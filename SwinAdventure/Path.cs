using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Path : GameObject
    {
        private Location _location;

        public Path(string[] ids, string name, string desc, Location loc) : base(ids, name, desc)
        {
            _location = loc;
        }

        public override string FullDescription
        {
            get => "You head " + FirstID + Environment.NewLine + base.FullDescription + Environment.NewLine + "You have arrived in " + Location.Name;
        }

        public Location Location
        {
            get => _location;
        }

        // move player to the path's location -- bug (path should not move path directly like that)
        public void MovePlayer(Player p, string id)
        {
            if (p.Location.PathExists(id))
                p.Location = Location;
        }
    }
}
