using Ninject;
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
    public class Cleaner : IComboBoxCommand
    {
        public Color4 FillColor { get; set; } = Color4.DimGray;

        public string CommandName => "Заливка";

        public Cleaner() {}

        public void Execute()
        {
            GL.ClearColor(FillColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }

        public Cleaner WithClearColor(Color4? clearColor = null)
        {
            clearColor = clearColor ?? Color4.DimGray;
            FillColor = (Color4)clearColor;

            return this;
        }
    }
}
