using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        private List<Item> _items;

        // return list of items' short description
        public string ItemList
        {
            get
            {
                string myList = "";
                foreach (Item item in _items)
                {
                    if (item == _items[_items.Count() - 1])
                        myList += "\t" + item.ShortDescription;
                    else
                        myList += "\t" + item.ShortDescription + Environment.NewLine;
                }
                return myList;
            }
        }

        public Inventory()
        {
            _items = new List<Item>();
        }
        
        // check item in inventory
        public bool HasItem(string id)
        {
            return _items.Exists(item => item.AreYou(id));
        }

        // add new item in inventory
        public void Put(Item itm)
        {
            _items.Add(itm);
        }

        // return item and delete it from inventory
        public Item Take(string id)
        {
            Item takenItem = _items.Find(item => item.AreYou(id));
            _items.Remove(takenItem);
            return takenItem;
        }

        // return item with no deletion
        public Item Fetch(string id)
        {
            return _items.Find(item => item.AreYou(id));
        }
    }
}
