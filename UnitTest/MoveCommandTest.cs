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
    class MoveCommandTest
    {
        // test full description
        [Test()]
        public void TestFullDescription()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            MoveCommand mC = new MoveCommand();

            p.Location.AddPath(paSouth);
            string result = "You head south\nYou go through a door\nYou have arrived in a garden";

            Assert.AreEqual(mC.Execute(p, new string[] { "move", "south" }), result);
        }

        // test move player to a new location given valid identifier
        [Test()]
        public void TestMovePlayer()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            MoveCommand mC = new MoveCommand();

            p.Location.AddPath(paSouth);
            mC.Execute(p, new string[] { "move", "south" });

            Assert.AreEqual(p.Location, gardenSouth);
        }

        // test move twice
        [Test()]
        public void TestMoveTwice()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            Location toiletEast = new Location(new string[] { "toilet" }, "a toilet", "Simply a toilet!");
            Path paEast = new Path(new string[] { "east", "e" }, "east", "You open the toilet door", toiletEast);
            MoveCommand mC = new MoveCommand();

            // move south then east
            p.Location.AddPath(paSouth);
            mC.Execute(p, new string[] { "move", "south" });
            p.Location.AddPath(paEast);
            mC.Execute(p, new string[] { "move", "east" });

            Assert.AreEqual(p.Location, toiletEast);
        }

        // test move back to the first location
        [Test()]
        public void TestReturn()
        {
            // declare and add path to 
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            MoveCommand mC = new MoveCommand();
            
            // record home as current location
            Location home = p.Location;
            
            // set garden - south, relative to home
            p.Location.AddPath(paSouth);

            // move south
            mC.Execute(p, new string[] { "move", "south" });

            // set home as north of current location (garden)
            Path paHome = new Path(new string[] { "north", "n" }, "north", "You go back home", home);
            p.Location.AddPath(paHome);

            // go back home (go north)
            mC.Execute(p, new string[] { "move", "n" });

            Assert.AreEqual(p.Location, home);
        }

        // test player remains in the same location given invalid identifier
        [Test()]
        public void TestStayInTheSameLocation()
        {
            Player p = new Player("john", "a mighty traveller");
            Location toiletNorth = new Location(new string[] { "toilet" }, "a toilet", "Simply a toilet!");
            Path paNorth = new Path(new string[] { "north", "n" }, "north", "You open the toilet door", toiletNorth);
            MoveCommand mC = new MoveCommand();

            // home
            Location home = p.Location;

            // no path was added, therefore player cannot move north
            mC.Execute(p, new string[] { "move", "north" });

            Assert.AreEqual(p.Location, home);
        }

        // test invalid moves
        [Test()]
        public void TestInvalidMoves()
        {
            Player p = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            MoveCommand mC = new MoveCommand();

            p.Location.AddPath(paSouth);

            Assert.AreEqual(mC.Execute(p, new string[] { "wassuppp", "south" }), "move/go/head where?");
            Assert.AreEqual(mC.Execute(p, new string[] { "move" }), "I don't know how to move like that");
            Assert.AreEqual(mC.Execute(p, new string[] { "move", "quick" }), "quick does not exist!");
            Assert.AreEqual(mC.Execute(p, new string[] { "move", "west" }), "west does not exist!");
        }
    }
}
