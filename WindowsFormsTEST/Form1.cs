//-----------------------------------------------------------------------
// <copyright file="Form1.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;
    using WindowsFormsTEST.Interfaces;
    using WindowsFormsTEST.Models;
    using WindowsFormsTEST.Properties;

    /// <summary>
    /// メインのフォームです。
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// モデルの回転行列です。
        /// </summary>
        private Matrix4 rotate = Matrix4.Identity;

        /// <summary>
        /// <see cref="Form1"/>を初期化します。
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            var radioIndex = 0;
            this.ModelRadioButtons
                .ToList()
                .ForEach(item => 
                {
                    item.Checked = radioIndex == 0;
                    item.Location = new System.Drawing.Point(7, 25 + (25 * radioIndex++));
                });

            this.groupBox1.Controls.AddRange(this.ModelRadioButtons.ToArray());
            this.Timer.Tick += new EventHandler(this.PeriodicProcessing);
        }

        /// <summary>
        /// 環境光の強さです。
        /// </summary>
        private static float AmbientVal { get; set; } = 0.2f;

        /// <summary>
        /// 拡散光の強さです。
        /// </summary>
        private static float DiffuseVal { get; set; } = 0.7f;

        /// <summary>
        /// 鏡面光の強さです。
        /// </summary>
        private static float SpecularVal { get; set; } = 1.0f;

        /// <summary>
        /// ロケール選択ラジオボタンの列挙です。
        /// </summary>
        private IEnumerable<RadioButton> ModelRadioButtons { get; set; } = new List<RadioButton>
        {
            new RadioButton
            {
                Text = nameof(Tube),
                Tag = new Tube
                {
                    Length = 1.0f,
                    Radius1 = 0.5f,
                    Radius2 = 0.5f
                }
            },
            new RadioButton
            {
                Text = nameof(Quads),
                Tag = new Quads
                {
                    Length = 1.0f,
                }
            },
            new RadioButton
            {
                Text = "StanfordBunny",
                Tag = new OBJData(Resources.bunny)
            },
        };

        /// <summary>
        /// 一定時間間隔の処理を制御するタイマーです。
        /// </summary>
        private Timer Timer { get; set; } = new Timer
        {
            Interval = 10,
            Enabled = true,
        };

        /// <summary>
        /// カメラを回転させるフラグです。
        /// </summary>
        private bool IsCameraRotating { get; set; }

        /// <summary>
        /// 現在のマウス位置です。
        /// </summary>
        private Vector2 Current { get; set; }

        /// <summary>
        /// 光源の位置です。
        /// </summary>
        private Vector4 Light0Position { get; set; } = new Vector4(200.0f, 150.0f, 500.0f, 0.0f);

        /// <summary>
        /// 環境光成分です。
        /// </summary>
        private Color4 LightAmbient { get; set; } = new Color4(AmbientVal, AmbientVal, AmbientVal, 1.0f);
        
        /// <summary>
        /// 拡散光成分です。
        /// </summary>
        private Color4 LightDiffuse { get; set; } = new Color4(DiffuseVal, DiffuseVal, DiffuseVal, 1.0f);

        /// <summary>
        /// 鏡面光成分です。
        /// </summary>
        private Color4 LightSpecular { get; set; } = new Color4(SpecularVal, SpecularVal, SpecularVal, 1.0f);

        /// <summary>
        /// 光源の一定減衰率です。
        /// </summary>
        private float LightConstantAttenuation { get; set; } = 1.0f;

        /// <summary>
        /// 線形減衰率です。
        /// </summary>
        private float LightLinearAttenuation { get; set; } = 0.3f;

        /// <summary>
        /// 二次減衰率です。
        /// </summary>
        private float LightQuadraticAttenuation { get; set; } = 0.3f;

        /// <summary>
        /// 環境光素材です。
        /// </summary>
        private Color4 MaterialAmbient { get; set; } = new Color4(0.24725f, 0.1995f, 0.0225f, 1.0f);

        /// <summary>
        /// 拡散光素材です。
        /// </summary>
        private Color4 MaterialDiffuse { get; set; } = new Color4(0.75164f, 0.60648f, 0.22648f, 1.0f);

        /// <summary>
        /// 鏡面光素材です。
        /// </summary>
        private Color4 MaterialSpecular { get; set; } = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

        /// <summary>
        /// 素材の光源？
        /// </summary>
        private float MaterialShiness { get; set; } = 51.4f;

        /// <summary>
        /// ボタン押下処理です。
        /// </summary>
        /// <param name="sender">イベントのソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            this.rotate = Matrix4.Identity;
        }

        /// <summary>
        /// 一定間隔での処理です。
        /// </summary>
        /// <param name="sender">イベントのソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void PeriodicProcessing(object sender, EventArgs e)
        {
            var culture = this.ModelRadioButtons
                .First(item => item.Checked)
                .Tag as CultureInfo;
            this.label1.Text = DateTime.Now.ToString("F", culture) + $" アスペクト比： {(float)GlControl1.Size.Width / (float)GlControl1.Size.Height}";

            this.Render();
        }

        /// <summary>
        /// 閉じるボタン押下処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "アプリケーションを終了しますか？",
                "確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// GLコントロールのロード処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

            //// 裏面削除、反時計回りが表でカリング
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            
            //// ライティングON Light0を有効化
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            //// 法線の正規化
            GL.Enable(EnableCap.Normalize);

            //// 色を材質に変換
            GL.Enable(EnableCap.ColorMaterial);
            GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.Diffuse);

            this.Light();
        }

        /// <summary>
        /// GLコントロールのリサイズ処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, GlControl1.Size.Width, GlControl1.Size.Height);
            GL.MatrixMode(MatrixMode.Projection);
            var projection = Matrix4.CreatePerspectiveFieldOfView(
                (float)Math.PI / 4,
                (float)GlControl1.Size.Width / (float)GlControl1.Size.Height,
                1.0f,
                64.0f);
            GL.LoadMatrix(ref projection);
        }

        /// <summary>
        /// 描画処理です。
        /// </summary>
        private void Render()
        {
            this.GlControl1.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.MatrixMode(MatrixMode.Modelview);
            var modelview = Matrix4.LookAt(new Vector3(0.0f, 0.0f, 4.0f), Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref modelview);

            var model = this.ModelRadioButtons
                .Where(radio => radio.Checked)
                .FirstOrDefault()
                .Tag as IDrawable;
            
            GL.MultMatrix(ref this.rotate);
            GL.PushMatrix();
            model?.Draw();
            GL.PopMatrix();

            this.GlControl1.SwapBuffers();
        }

        /// <summary>
        /// 光源の設定です。
        /// </summary>
        private void Light()
        {
            GL.Light(LightName.Light0, LightParameter.Position, this.Light0Position);
            GL.Light(LightName.Light0, LightParameter.Ambient, this.LightAmbient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, this.LightDiffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, this.LightSpecular);
            GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, this.LightConstantAttenuation);
            GL.Light(LightName.Light0, LightParameter.LinearAttenuation, this.LightLinearAttenuation);
            GL.Light(LightName.Light0, LightParameter.QuadraticAttenuation, this.LightQuadraticAttenuation);
            
            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, this.MaterialAmbient);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, this.MaterialDiffuse);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, this.MaterialSpecular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, this.MaterialShiness);
        }

        /// <summary>
        /// GLコントロールの描画処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_Paint(object sender, EventArgs e)
        {
            this.Render();
        }

        /// <summary>
        /// GLコントロールでのマウスキーダウン処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsCameraRotating = MouseButtons.Left == e.Button;
            if (this.IsCameraRotating)
            {
                this.Current = new Vector2(e.X, e.Y);
            }
        }

        /// <summary>
        /// GLコントロールのマウスキーアップ処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                this.IsCameraRotating = false;
                this.Current = Vector2.Zero;
            }
        }

        /// <summary>
        /// GLコントロールのマウス移動処理です。
        /// </summary>
        /// <param name="sender">イベントソースです。</param>
        /// <param name="e">イベント引数です。</param>
        private void GlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsCameraRotating)
            {
                var previous = this.Current;
                this.Current = new Vector2(e.X, e.Y);
                var delta = this.Current - previous;
                delta /= (float)Math.Sqrt(this.GlControl1.Width ^ 2 + this.GlControl1.Height ^ 2);
                if (delta.Length > 0.0f)
                {
                    var rad = delta.Length * Math.PI;
                    var theta = (float)Math.Sin(rad) / delta.Length;
                    var after = new Quaternion(delta.Y, delta.X, 0.0f, theta);
                    this.rotate *= Matrix4.CreateFromQuaternion(after);
                }
            }
        }
    }
}