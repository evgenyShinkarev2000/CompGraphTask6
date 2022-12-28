using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;
using Task6.Parameters;

namespace Task6.Painters
{
    public class DrawRegularPoligon : IDrawFigureCommand
    {
        public string Name => "Правильный многоугольник";
        public SidesCountParams SidesCountParams { get; }
        public RadiusParams RadiusParams { get; }
        public FillColorParams FillColorParams { get; }
        public DrawRegularPoligon(SidesCountParams sidesCountParams, RadiusParams radiusParams, FillColorParams fillColorParams)
        {
            SidesCountParams = sidesCountParams;
            RadiusParams = radiusParams;
            FillColorParams = fillColorParams;
        }

        public void Execute()
        {
            var currentPoint = new Vector2(0, RadiusParams.Radius);
            var angle = Math.PI * 2.0 * (1.0 / SidesCountParams.SidesCount);
            GL.Begin(PrimitiveType.Polygon);
            GL.Color4(FillColorParams.GetNextColor());
            foreach(var i in Enumerable.Range(0, SidesCountParams.SidesCount))
            {
                GL.Vertex2(currentPoint);
                currentPoint = RollVectorRight(angle, currentPoint);
            }
            GL.End();
        }
        private Vector2 RollVectorRight(double angle, Vector2 vector)
        {
            return new Vector2(
                (float)(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle)),
                (float)(vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle)));
        }
    }
}
