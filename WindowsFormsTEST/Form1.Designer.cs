//-----------------------------------------------------------------------
// <copyright file="Form1.Designer.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST
{
    /// <summary>
    /// メインのフォームです。
    /// </summary>
    public partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.GlControl1 = new OpenTK.GLControl();
            this.TrackBarX = new System.Windows.Forms.TrackBar();
            this.TrackBarY = new System.Windows.Forms.TrackBar();
            this.TrackBarZ = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarZ)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "現在時刻";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(18, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 320);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "モデル";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(927, 648);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(149, 48);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // GlControl1
            // 
            this.GlControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GlControl1.BackColor = System.Drawing.Color.Black;
            this.GlControl1.Location = new System.Drawing.Point(299, 124);
            this.GlControl1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.GlControl1.Name = "GlControl1";
            this.GlControl1.Size = new System.Drawing.Size(777, 517);
            this.GlControl1.TabIndex = 4;
            this.GlControl1.VSync = false;
            this.GlControl1.Load += new System.EventHandler(this.GlControl1_Load);
            this.GlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.GlControl1_Paint);
            this.GlControl1.Resize += new System.EventHandler(this.GlControl1_Resize);
            this.GlControl1.MouseDown += this.GlControl1_MouseDown;
            this.GlControl1.MouseUp += this.GlControl1_MouseUp;
            this.GlControl1.MouseMove += this.GlControl1_MouseMove;
            // 
            // TrackBarX
            // 
            this.TrackBarX.Location = new System.Drawing.Point(18, 451);
            this.TrackBarX.Maximum = 100;
            this.TrackBarX.Name = "TrackBarX";
            this.TrackBarX.Size = new System.Drawing.Size(273, 69);
            this.TrackBarX.TabIndex = 5;
            // 
            // TrackBarY
            // 
            this.TrackBarY.Location = new System.Drawing.Point(18, 526);
            this.TrackBarY.Maximum = 100;
            this.TrackBarY.Name = "TrackBarY";
            this.TrackBarY.Size = new System.Drawing.Size(273, 69);
            this.TrackBarY.TabIndex = 6;
            this.TrackBarY.Value = 100;
            // 
            // TrackBarZ
            // 
            this.TrackBarZ.Location = new System.Drawing.Point(18, 601);
            this.TrackBarZ.Maximum = 100;
            this.TrackBarZ.Name = "TrackBarZ";
            this.TrackBarZ.Size = new System.Drawing.Size(273, 69);
            this.TrackBarZ.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 708);
            this.Controls.Add(this.TrackBarZ);
            this.Controls.Add(this.TrackBarY);
            this.Controls.Add(this.TrackBarX);
            this.Controls.Add(this.GlControl1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CloseButton;
        private OpenTK.GLControl GlControl1;
        private System.Windows.Forms.TrackBar TrackBarX;
        private System.Windows.Forms.TrackBar TrackBarY;
        private System.Windows.Forms.TrackBar TrackBarZ;
    }
}