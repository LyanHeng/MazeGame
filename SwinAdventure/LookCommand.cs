using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" })
        { }

        // execute looking for the item
        public override string Execute(Player p, string[] text)
        {
            // check all the error "look" conditions
            if (text.Length != 1 && text.Length != 3 && text.Length != 5)
                return "I don't know how to look like that";
            else if (text[0] != "look")
                return "Error in look input";
            else if (text.Length == 3 && text[1] != "at")
                return "What do you want to look at?";
            else if (text.Length == 5 && text[3] != "in")
                return "What do you want to look in?";

            // identify container
            IHaveInventory container;
            if (text[0] == "look" && text.Length == 1)
                return p.Location.FullDescription;
            else if (text.Length == 3)
                container = p;
            else if (FetchContainer(p, text[4]) != null)
                container = FetchContainer(p, text[4]);
            else
                return "I can't find the " + text[text.Length - 1];

            // return the FullDescription of the item in the container
            return LookAtIn(text[2], container);
        }

        // return the container
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        // look for item in the container
        private string LookAtIn(string thingId, IHaveInventory container)
        {
            GameObject contDesc = container.Locate(thingId);
            if (contDesc != null)
                return contDesc.FullDescription;
            else
                return "I can't find the " + thingId + " in " + container.Name;
        }
    }
}
