using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Interfaces;

namespace Task6.Commands
{
    class CustomCommand : IMyCommand
    {
        public readonly Action Command;
        public CustomCommand(Action command)
        {
            Command = command;
        }
        public void Execute()
        {
            Command();
        }
    }
}
