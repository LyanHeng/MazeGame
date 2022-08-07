using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        private Inventory _inventory;
        private Location _location;
        
        // return inventory items' short description
        public Inventory Inventory
        {
            get => _inventory;
        }

        public Location Location 
        { 
            get => _location; 
            set => _location = value;
        }

        // return player's description and inventory descriptions
        public override string FullDescription
        {
            get
            {
                string desc = "You are " + Name + " " + base.FullDescription + Environment.NewLine + "You are carrying" + Environment.NewLine;
                desc += Inventory.ItemList;
                return desc;
            }
        }

        public Player(string name, string desc) : base (new string[] {"me", "inventory"}, name, desc)
        {
            _inventory = new Inventory();
            _location = new Location(new string[] { "home", "spawn" }, "home", "Home sweet home!");
        }

        // find any item in the inventory & the player itself
        public GameObject Locate(string id)
        {
            if (AreYou(id))
                return this;
            else if (Inventory.HasItem(id))
                return Inventory.Fetch(id);
            else
                return Location.Locate(id);
        }
    }
}
