using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;
using Task6.Parameters;

namespace Task6.Painters
{
    public class Rotate : IMyCommand
    {
        public RotateParams RotateParams { get; }
        public string CommandName => "Повернуть";
        public Rotate(RotateParams rotateParams)
        {
            RotateParams = rotateParams;
        }

        public void Execute()
        {
            GL.Rotate(RotateParams.Angle, RotateParams.Direction);
        }
    }
}
