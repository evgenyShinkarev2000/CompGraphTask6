using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task6.Interfaces;

namespace Task6
{
    internal static class DIContainer
    {
        public static readonly StandardKernel standartKernel = new StandardKernel();
        static DIContainer()
        {
        }
    }
}
