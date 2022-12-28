using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Commands
{
    public class DrawCoordinates : IMyCommand
    {
        public void Execute()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.LineWidth(5);
            GL.Color4(Color4.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Color4(Color4.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.Color4(Color4.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 1);
            GL.End();
        }
    }
}
