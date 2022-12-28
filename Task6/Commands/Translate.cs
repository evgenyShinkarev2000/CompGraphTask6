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
    public class Translate : IMyCommand
    {
        public string CommandName => "Переместить";
        public TranslateParams TranslateParams { get; }

        public Translate(TranslateParams translateParams)
        {
            TranslateParams = translateParams;
        }

        public void Execute()
        {
            GL.Translate(TranslateParams.Translate);
        }
    }
}
