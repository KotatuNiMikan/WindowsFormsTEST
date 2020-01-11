//-----------------------------------------------------------------------
// <copyright file="Quads.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST.Models
{
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using WindowsFormsTEST.Interfaces;

    /// <summary>
    /// 四角形です。
    /// </summary>
    internal class Quads : IDrawable
    {
        /// <summary>
        /// 辺の長さです。
        /// </summary>
        public float Length { get; set; }

        /// <inheritdoc/>
        public void Draw()
        {
            var hoge = this.Length / 2;
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0.0f, 0.0f, 1.0f);
            GL.Color4(Color4.White);
            GL.Vertex3(hoge, hoge, 0.0f);
            GL.Color4(Color4.Blue);
            GL.Vertex3(-hoge, hoge, 0.0f);
            GL.Color4(Color4.Lime);
            GL.Vertex3(-hoge, -hoge, 0.0f);
            GL.Color4(Color4.Red);
            GL.Vertex3(hoge, -hoge, 0.0f);

            GL.End();
        }
    }
}
