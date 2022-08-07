using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head" })
        { }

        // execute moving player to path
        public override string Execute(Player p, string[] text)
        {
            // check all the error "move" conditions
            if (text.Length != 2)
                return "I don't know how to move like that";
            else if (text[0] != "move" && text[0] != "go" && text[0] != "head")
                return "move/go/head where?";

            // identify path and move player if path is found
            Path pathContainer = FetchPath(p, text[1]);
            if (pathContainer != null)
            {
                MovePlayer(p, pathContainer, text[1]);
                return pathContainer.FullDescription;
            }
            else
                return text[1] + " does not exist!";
        }

        // return the path requested in the player's location
        private Path FetchPath(Player p, string pathId)
        {
            if (p.Location.PathExists(pathId))
                return p.Location.LocatePath(pathId);
            else
                return null;
        }

        // move player to the new location
        private void MovePlayer(Player p, Path path, string pathId)
        {
            path.MovePlayer(p, pathId);
        }
    }
}
