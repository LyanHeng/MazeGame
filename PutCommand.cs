using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class PutCommand : Command
    {
        public PutCommand() : base(new string[] { "put", "drop" })
        { }

        public override string Execute(Player p, string[] text)
        {
            // check all the error "look" conditions
            if (text.Length != 2 && text.Length != 4)
                return "I can't put like that!";
            else if (!AreYou(text[0]))
                return "Error in put input";
            else if (text.Length == 4 && text[2] != "in")
                return text[0] + " " + text[1] + " in what/where?";

            IHaveInventory container;
            // identify and verify the container
            if (text.Length == 2)
                container = p.Location;
            else if (FetchContainer(p, text[3]) != null)
                container = FetchContainer(p, text[3]);
            else
                return "I can't find " + text[3];

            // put item in container
            return PutIn(p, text[1], container);
        }

        // return the container
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        // put item in the container
        private string PutIn(Player p, string thingId, IHaveInventory container)
        {
            // remove item from player's inventory and put in the container
            Item cont = p.Inventory.Take(thingId);
            if (cont != null)
            {
                container.Inventory.Put(cont);
                return "You have put the " + cont.Name + " in " + container.Name;
            }
            else
                return "I can't find " + thingId + " in " + container.Name;
        }
    }
}
