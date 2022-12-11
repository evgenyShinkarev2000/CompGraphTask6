using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class Rotator : INamedCommand
    {
        public double angle;
        public double x;
        public double y;
        public double z;
        public string CommandName => "Повернуть";

        public Rotator(double angle = 0, double x = 0, double y = 0, double z = 0)
        {
            this.angle = angle;
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Execute()
        {
            GL.Rotate(angle, x, y, z);
        }
    }
}
