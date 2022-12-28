using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Parameters
{
    public class FillColorParams : IParametersProvider<FillColorParams>
    {
        public readonly Color4 FirstColor;
        public FillColorParams(Color4 color)
        {
            FirstColor = color;
        }
        
        public virtual Color4 GetNextColor()
        {
            return FirstColor;
        }

        public FillColorParams GetParameters()
        {
            return this;
        }
    }
}
