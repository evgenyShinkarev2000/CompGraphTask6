using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class DrawRegularPoligon : IDrawFigureCommand
    {
        public string Name => "Правильный многоугольник";
        public float Radius { get; set; }
        public int SidesCount
        {
            get => sidesCount; set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                sidesCount = value;
            }
        }

        private int sidesCount;
        public void Execute()
        {
            var currentPoint = new Vector2(0, Radius);
            var angle = Math.PI * 2.0 * (1.0 / SidesCount);
            GL.Begin(PrimitiveType.Polygon);
            foreach(var i in Enumerable.Range(0, sidesCount))
            {
                GL.Vertex2(currentPoint);
                currentPoint = RollVectorRight(angle, currentPoint);
            }
            GL.End();
        }

        public IEnumerable<IMyCommand> Init(CommandInitializer visitor)
            => visitor.Visit(this);

        private Vector2 RollVectorRight(double angle, Vector2 vector)
        {
            return new Vector2(
                (float)(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle)),
                (float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle)));
        }
    }
}
