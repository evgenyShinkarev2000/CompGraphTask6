using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Parameters
{
    public class TranslateParams
    {
        public readonly Vector3 Translate;
        public TranslateParams(Vector3 translate)
        {
            Translate = translate;
        }
        public TranslateParams GetParameters()
        {
            return this;
        }
    }
}
