using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class QuitCommand : Command
    {
        public QuitCommand() : base(new string[] { "quit" })
        { }

        public override string Execute(Player p, string[] text)
        {
            return "Bye.";
        }
    }
}
