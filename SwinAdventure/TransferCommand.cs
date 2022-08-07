using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class TransferCommand : Command
    {
        public TransferCommand() : base(new string[] { "put", "drop", "take", "pickup" })
        { }

        public override string Execute(Player p, string[] text)
        {
            // check all the error conditions
            if (text.Length != 2 && text.Length != 4)
                return "I can't do that!";
            else if (!AreYou(text[0]))
                return "Error in " + text[0] + " input";
            else if (text.Length == 4 && ((text[2] != "in") == (text[2] != "from")))
                return text[0] + " " + text[1] + " in/from what/where?";

            IHaveInventory container;
            // identify and verify the container
            if (text.Length == 2)
                container = p.Location;
            else if (FetchContainer(p, text[3]) != null)
                container = FetchContainer(p, text[3]);
            else
                return "I can't find " + text[3];

            // assign transferring container and receiveing container
            IHaveInventory receiveCont, transferCont;
            if (text[0] == "put" || text[0] == "drop")
            {
                transferCont = p;
                receiveCont = container;
            }
            else
            {
                transferCont = container;
                receiveCont = p;
            }

            // transfer the item from one container to the other
            return PerformTransfer(transferCont, text[1], receiveCont, text[0]);
        }

        // return the container
        private IHaveInventory FetchContainer(Player p, string containerId)
        {
            return p.Locate(containerId) as IHaveInventory;
        }

        // transform item from one container to another
        private string PerformTransfer(IHaveInventory transferCont, string thingId, IHaveInventory receiverCont, string keyWord)
        {
            // remove item from player's inventory and put in the container
            Item cont = transferCont.Inventory.Take(thingId);
            if (cont != null)
            {
                receiverCont.Inventory.Put(cont);
                if (keyWord == "put" || keyWord == "drop")
                    return "You have put the " + cont.Name + " in " + receiverCont.Name;
                else
                    return "You have taken the " + cont.Name + " from " + transferCont.Name;
            }
            else
                return "I can't find " + thingId + " in " + receiverCont.Name;
        }
    }
}
