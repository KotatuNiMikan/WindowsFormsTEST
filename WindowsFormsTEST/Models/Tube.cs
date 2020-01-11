//-----------------------------------------------------------------------
// <copyright file="Tube.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace WindowsFormsTEST.Models
{
    using OpenTK.Graphics;
    using OpenTK.Graphics.OpenGL;
    using System;
    using WindowsFormsTEST.Interfaces;

    /// <summary>
    /// 円柱です。
    /// </summary>
    internal class Tube : IDrawable
    {
        /// <summary>
        /// 長さです。
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// 上底の半径です。
        /// </summary>
        public float Radius1 { get; set; }

        /// <summary>
        /// 下底の半径です。
        /// </summary>
        public float Radius2 { get; set; }

        /// <inheritdoc/>
        public void Draw()
        {
            GL.Color4(Color4.Violet);
            GL.Begin(PrimitiveType.TriangleStrip);
            for (int deg = 0; deg <= 360; deg += 3)
            {
                var rx = (float)Math.Cos((float)Math.PI * deg / 180);
                var ry = (float)Math.Sin((float)Math.PI * deg / 180);

                GL.Normal3(rx, 0, ry);
                GL.Vertex3(rx * this.Radius1, -this.Length / 2, ry * this.Radius1);
                GL.Vertex3(rx * this.Radius2, this.Length / 2, ry * this.Radius2);
            }

            GL.LoadIdentity();
            GL.End();
        }
    }
}
