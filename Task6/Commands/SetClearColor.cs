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
    public class SetClearColor : IInitializableCommand
    {
        public Color4 Color { get; set; }
        public void Execute()
        {
            GL.ClearColor(Color);
        }
    }
}
