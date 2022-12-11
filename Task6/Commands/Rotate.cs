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
    public class Rotate : IInitializableCommand
    {
        public double angle;
        public double x;
        public double y;
        public double z;
        public string CommandName => "Повернуть";
        public void Execute()
        {
            GL.Rotate(angle, x, y, z);
        }

        public IEnumerable<IMyCommand> Init(CommandInitializer visitor)
            => visitor.Visit(this);
    }
}
