using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Interfaces
{
    internal interface IParametersProvider<out T>
    {
        T GetParameters();
    }
}
