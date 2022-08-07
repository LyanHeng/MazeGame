using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Location : GameObject, IHaveInventory
    {
        private Inventory _locItems = new Inventory();
        private List<Path> _paths = new List<Path>();

        public Location(string[] id, string name, string description) : base(id, name, description)
        { }

        public Inventory Inventory
        {
            get => _locItems;
        }

        public override string FullDescription
        {
            get
            {
                string message = "You are in " + Name + Environment.NewLine + base.FullDescription + Environment.NewLine;
                if (_paths.Count == 0)
                    message += "There are no paths" + Environment.NewLine;
                else
                {
                    message += "There are exits to the ";
                    if (_paths.Count == 1)
                        message += _paths[0].Name + Environment.NewLine;
                    else
                    {
                        foreach (Path path in _paths)
                        {
                            if (path == _paths[_paths.Count() - 1])
                                message += "and " + path.Name + Environment.NewLine;
                            else
                                message += path.Name + ", ";
                        }
                    }
                }
                message += "In this room you can see: " + Environment.NewLine + Inventory.ItemList;

                return message;
            }
        }

        // locate object in location or itself
        public GameObject Locate(string id)
        {
            if (AreYou(id))
                return this;
            else
                return Inventory.Fetch(id);
        }

        // determines if a path exists in the location
        public bool PathExists(string pathId)
        {
            return _paths.Exists(path => path.AreYou(pathId));
        }

        // returns a path in the location
        public Path LocatePath(string pathId)
        {
            return _paths.Find(path => path.AreYou(pathId));
        }

        // add new path
        public void AddPath(Path p)
        {
            // if path already exists, do not overwrite
            if (!(PathExists(p.FirstID)))
                _paths.Add(p);
        }
    }
}
