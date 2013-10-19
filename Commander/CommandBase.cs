using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Utilities
{
    public abstract class CommandBase : ICommand
    {
        public abstract bool Validate();
        public abstract void Execute();
        
        public abstract String Id
        {
            get;
        }
        
        public virtual void OutputHelp()
        {
            this.Output(@"No help is available for this command.");
        }

        protected void Output(String message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}
