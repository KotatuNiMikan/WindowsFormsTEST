//-----------------------------------------------------------------------
// <copyright file="Triangle.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST.Models
{
    using OpenTK;
    using OpenTK.Graphics.OpenGL;
    using System.Collections.Generic;
    using System.Linq;
    using WindowsFormsTEST.Interfaces;

    /// <summary>
    /// 三角形です。
    /// </summary>
    internal class Triangle : IDrawable
    {
        /// <summary>
        /// <see cref="Triangle"/>の初期化です。
        /// </summary>
        /// <param name="vertex1">頂点１です。</param>
        /// <param name="vertex2">頂点２です。</param>
        /// <param name="vertex3">頂点３です。</param>
        public Triangle(Vector3d vertex1, Vector3d vertex2, Vector3d vertex3)
        {
            this.Vertexes = new List<Vector3d> { vertex1, vertex2, vertex3 };
        }

        /// <summary>
        /// 頂点の列挙です。
        /// </summary>
        private IEnumerable<Vector3d> Vertexes { get; set; }

        /// <inheritdoc/>
        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            this.Vertexes
                .ToList()
                .ForEach(vertex => 
                {
                    GL.Normal3(this.GetNormal());
                    GL.Vertex3(
                        vertex.X,
                        vertex.Y,
                        vertex.Z);
                });
            GL.End();
        }

        /// <summary>
        /// 法線を取得します。
        /// </summary>
        /// <returns>法線ベクトルです。</returns>
        private Vector3d GetNormal()
        {
            var vector1 = this.Vertexes.ElementAt(1) * this.Vertexes.ElementAt(0);
            var vector2 = this.Vertexes.ElementAt(2) * this.Vertexes.ElementAt(0);
            return vector1 * vector2;
        }
    }
}
