using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Painters
{
    public class DrawRegularPoligon : IDrawFigureCommand
    {
        public string Name => "Правильный многоугольник";
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMyCommand> Init(CommandInitializer visitor)
            => visitor.Visit(this);
    }
}
