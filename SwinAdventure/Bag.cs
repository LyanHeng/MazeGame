using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Bag : Item, IHaveInventory
    {
        private Inventory _inventory;

        // bag's inventory
        public Inventory Inventory
        {
            get => _inventory;
        }

        // full description of bag
        public override string FullDescription
        {
            get
            {
                string desc = "In the " + Name + " you can see:" + Environment.NewLine;
                desc += Inventory.ItemList;
                return desc;
            }
        }

        public Bag(string[] ids, string name, string desc) : base(ids, name, desc)
        {
            _inventory = new Inventory();
        }

        // locate items in bag or bag itself
        public GameObject Locate(string id)
        {
            if (AreYou(id))
                return this;
            else
                return Inventory.Fetch(id);
        }
    }
}
