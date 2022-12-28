using Ninject;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class Fill : IDrawFigureCommand
    {
        public string Name => "Заливка";

        public Fill() {}

        public void Execute()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }
}
