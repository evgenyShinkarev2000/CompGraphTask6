using Ninject;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Commands;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class DrawArbitaryPoligon : IDrawFigureCommand
    {
        public readonly List<Vector3> Vectors = new List<Vector3>();
        public string Name => "Произвольный полигон";
        public DrawArbitaryPoligon() { }

        public void Execute()
        {
            GL.Begin(PrimitiveType.Polygon);
            foreach(var vector in Vectors)
            {
                GL.Vertex3(vector);
            }
            GL.End();
        }
        public DrawArbitaryPoligon WithVectors(IEnumerable<Vector3> vectors)
        {
            Vectors.Clear();
            Vectors.AddRange(vectors);

            return this;
        }
    }
}
