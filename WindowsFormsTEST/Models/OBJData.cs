//-----------------------------------------------------------------------
// <copyright file="OBJData.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST.Models
{
    using OpenTK;
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using WindowsFormsTEST.Interfaces;

    /// <summary>
    /// .objファイルのデータです。
    /// </summary>
    internal class OBJData : IDrawable
    {        
        /// <summary>
        /// <see cref="OBJData"/>を初期化します。
        /// </summary>
        /// <param name="data">バイト配列です。</param>
        public OBJData(byte[] data)
        {
            var objText = Encoding.UTF8.GetString(data);
            objText = objText.Replace("\r\n", "\n");
            var lineData = objText.Split('\n');
            var vertexes = lineData
                .Where(line => line.StartsWith("v"))
                .Select(line =>
                {
                    var splitStrings = line.Split(' ');
                    return new Vector3d
                    {
                        X = float.Parse(splitStrings[1]),
                        Y = float.Parse(splitStrings[2]),
                        Z = float.Parse(splitStrings[3]),
                    };
                })
                .ToList();

            var xCordinations = vertexes.Select(vertex => vertex.X);
            var yCordinations = vertexes.Select(vertex => vertex.Y);
            var zCordinations = vertexes.Select(vertex => vertex.Z);

            var xLength = (float)(xCordinations.Max() - xCordinations.Min());
            var yLength = (float)(yCordinations.Max() - yCordinations.Min());
            var zLength = (float)(zCordinations.Max() - zCordinations.Min());

            this.ScalingVal = xLength > yLength ?
                xLength :
                yLength;
            this.ScalingVal = zLength > this.ScalingVal ?
                zLength :
                this.ScalingVal;

            this.Triangles = lineData
                .Where(line => line.StartsWith("f"))
                .Select(line =>
                {
                    var splitStrings = line.Split(' ');
                    return new Triangle(
                        vertexes.ElementAt(int.Parse(splitStrings[1]) - 1),
                        vertexes.ElementAt(int.Parse(splitStrings[2]) - 1),
                        vertexes.ElementAt(int.Parse(splitStrings[3]) - 1));
                })
                .ToList();
        }

        /// <summary>
        /// 三角形の列挙です。
        /// </summary>
        private IEnumerable<Triangle> Triangles { get; set; }

        /// <summary>
        /// モデルの大きさです。
        /// </summary>
        private float ScalingVal { get; set; }

        /// <inheritdoc/>
        public void Draw()
        {
            var scale = 1 / this.ScalingVal;
            GL.Translate(0.0f, -0.5f, 0.0f);
            GL.Scale(scale, scale, scale);
            GL.Color4(Color4.Lime);
            this.Triangles
                .ToList()
                .ForEach(triagle => triagle.Draw());
            GL.LoadIdentity();
        }
    }
}
