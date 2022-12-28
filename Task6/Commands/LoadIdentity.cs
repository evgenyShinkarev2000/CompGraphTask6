using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Commands
{
    public class LoadIdentity : IMyCommand
    {
        public void Execute()
        {
            GL.LoadIdentity();
        }
    }
}
