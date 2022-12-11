using Ninject;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class PoligonPainter : IComboBoxCommand
    {
        public readonly List<Vector3> Vectors = new List<Vector3>();
        public string CommandName => "Полигон";
        public Color4 FillColor { get; set; } = Color4.White;
        public PoligonPainter() { }

        public void Execute()
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(FillColor);
            foreach(var vector in Vectors)
            {
                GL.Vertex3(vector);
            }
            GL.End();
        }

        public PoligonPainter WithVectors(Vector3[] vectors)
        {
            Vectors.Clear();
            Vectors.AddRange(vectors);

            return this;
        }
    }
}
