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
    public class SetFillColor : IInitializableCommand
    {
        public string CommandName => "Изменить основной цвет";
        public Color4 Color { get; set; }

        public void Execute()
        {
            GL.Color4(Color);
        }

        public IEnumerable<IMyCommand> Init(CommandInitializer visitor)
            => visitor.Visit(this);
    }
}
