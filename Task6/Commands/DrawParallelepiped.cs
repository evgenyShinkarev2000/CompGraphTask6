using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;
using Task6.Parameters;

namespace Task6.Commands
{
    public class DrawParallelepiped : IDrawFigureCommand
    {
        public string Name => "Параллелепипед";
        public Vector3 Sides = new Vector3(1, 1, 1);
        public PrimitiveType primitiveType = PrimitiveType.Polygon;
        private readonly FillColorParams fillColorParams;

        public DrawParallelepiped(FillColorParams fillColorParams) 
        {
            this.fillColorParams = fillColorParams;
        }

        public void Execute()
        {
            GL.Translate(-Sides / 2);
            //Верх и низ
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach(var vector in RectangleWithFixedZ(Sides.X, Sides.Y, 0))
            {
                GL.Vertex3(vector);
            }
            GL.End();
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach (var vector in RectangleWithFixedZ(Sides.X, Sides.Y, 1))
            {
                GL.Vertex3(vector);
            }
            GL.End();
            // левая правая стороны
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach (var vector in RectangleWithFixedX(0, Sides.Y, Sides.Z))
            {
                GL.Vertex3(vector);
            }
            GL.End();
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach (var vector in RectangleWithFixedX(1, Sides.Y, Sides.Z))
            {
                GL.Vertex3(vector);
            }
            GL.End();
            // ближняя и дальняя сторона
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach (var vector in RectangleWithFixedY(Sides.X, 0, Sides.Z))
            {
                GL.Vertex3(vector);
            }
            GL.End();
            GL.Color4(fillColorParams.GetNextColor());
            GL.Begin(this.primitiveType);
            foreach (var vector in RectangleWithFixedY(Sides.X, 1, Sides.Y))
            {
                GL.Vertex3(vector);
            }
            GL.End();
        }

        private Vector3[] RectangleWithFixedX(float x, float y, float z)
        {
            return new[] { new Vector3(x, 0, 0), new Vector3(x, 0, z), new Vector3(x, y, z), new Vector3(x, y, 0) };
        }
        private Vector3[] RectangleWithFixedY(float x, float y, float z)
        {
            return new[] { new Vector3(0, y, 0), new Vector3(x, y, 0), new Vector3(x, y, z), new Vector3(0, y, z) };
        }
        private Vector3[] RectangleWithFixedZ(float x, float y, float z)
        {
            return new[] { new Vector3(0, 0, z), new Vector3(x, 0, z), new Vector3(x, y, z), new Vector3(0, y, z) };
        }
    }
}
