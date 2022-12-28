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
    public class Translate : IInitializableCommand
    {
        public double x;
        public double y;
        public double z;
        public string CommandName => "Переместить";

        public void Execute()
        {
            GL.Translate(x, y, z);
        }
    }
}
