using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class CommandProcessor : Command
    {
        private List<Command> _commands = new List<Command>();

        public CommandProcessor() : base(new string[] { "processor" }) { }

        // allocate the right command when a command is given
        public override string Execute(Player p, string[] text)
        {
            // check for error
            if (text.Length == 0)
                return "command not found";

            // find command
            if (CommandExists(text[0]))
                return SearchCommand(text[0]).Execute(p, text);
            else
                return text[0] + " command not recognized";
        }

        // command exists
        public bool CommandExists(string cmdId)
        {
            return _commands.Exists(command => command.AreYou(cmdId));
        }

        // search for the right command
        public Command SearchCommand(string cmdId)
        {
            return _commands.Find(command => command.AreYou(cmdId));
        }

        // add a new command to the command list
        public void RegisterCommand(Command cmd)
        {
            _commands.Add(cmd);
        }
    }
}