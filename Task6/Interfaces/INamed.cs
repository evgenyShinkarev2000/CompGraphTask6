using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.Interfaces
{
    public interface INamed : IMyCommand
    {
        public string Name { get; }
    }
}
