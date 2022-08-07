using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;
using NUnit.Framework;

namespace UnitTest
{
    [TestFixture()]
    class PathTest
    {
        // test move player to another location
        [Test()]
        public void TestMovePlayer()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path pa = new Path(new string[] { "south", "s" }, "south", "go through a door", gardenSouth);

            p.Location.AddPath(pa);
            pa.MovePlayer(p, "south");

            Assert.AreEqual(p.Location, gardenSouth);
        }

        // test get a path from a location from its identifier
        [Test()]
        public void TestGetPath()
        {
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path pa = new Path(new string[] { "south", "s" }, "south", "go through a door", gardenSouth);

            gardenSouth.AddPath(pa);

            Assert.AreEqual(gardenSouth.LocatePath("south"), pa);
        }

        // test player leave a location, given a valid path identifier
        [Test()]
        public void TestLeaveLocation()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path pa = new Path(new string[] { "south", "s" }, "south", "go through a door", gardenSouth);

            p.Location.AddPath(pa);
            pa.MovePlayer(p, "south");

            Assert.AreEqual(p.Location, gardenSouth);
        }

        // test player stay in a location, given an invalid path identifier
        [Test()]
        public void TestStayLocation()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path pa = new Path(new string[] { "south", "s" }, "south", "go through a door", gardenSouth);
            Location toiletNorth = new Location(new string[] { "toilet" }, "a toilet", "Simply a toilet!");
            Path paNorth = new Path(new string[] { "north", "n" }, "north", "opens the toilet door", toiletNorth);

            p.Location.AddPath(pa);
            pa.MovePlayer(p, "south");
            paNorth.MovePlayer(p, "north");

            Assert.AreEqual(p.Location, gardenSouth);
        }
    }
}
