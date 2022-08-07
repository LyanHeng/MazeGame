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
    class LocationTest
    {
        // test locate itself
        [Test()]
        public void TestLocateItself()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");

            Assert.AreEqual(newLocation.Locate("home"), newLocation);
        }

        // test locate items the location has
        [Test()]
        public void TestLocateItems()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Item table = new Item(new string[] { "table" }, "table", "Wooden table");

            newLocation.Inventory.Put(table);

            Assert.AreEqual(newLocation.Locate("table"), table);
        }

        // test not locate item it does not have
        [Test()]
        public void TestNotLocateItems()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Item table = new Item(new string[] { "table" }, "table", "Wooden table");
            Item palmTree = new Item(new string[] { "palm", "tree" }, "palmtree", "Good old palm tree!");

            newLocation.Inventory.Put(table);

            Assert.AreEqual(newLocation.Locate("palmtree"), null);
        }

        // test player locating items in the location
        [Test()]
        public void TestPlayerLocateItem()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Player newPlayer = new Player("John", "A mighty explorer");
            Item table = new Item(new string[] { "table" }, "table", "Wooden table");

            // do we have to add the person to the location? do we consider them as gameobjects?
            newLocation.Inventory.Put(table);
            newPlayer.Location = newLocation;

            Assert.AreEqual(newPlayer.Locate("table"), table);
        }

        // test player not locating items in the location
        [Test()]
        public void TestPlayerNotLocateItem()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Player newPlayer = new Player("John", "A mighty explorer");

            Item palmTree = new Item(new string[] { "palm", "tree" }, "palmtree", "Good old palm tree!");
            Item table = new Item(new string[] { "table" }, "table", "Wooden table");
            newPlayer.Location = newLocation;
            newLocation.Inventory.Put(table);

            Assert.AreEqual(newPlayer.Locate("palmTree"), null);
        }

        // test locate existing item in a non-existing location
        [Test()]
        public void TestLocateItemInNotLocation()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Location otherLocation = new Location(new string[] { "beach", "sunny" }, "beach", "A sunny beach!");
            Player newPlayer = new Player("John", "A mighty explorer");

            Item table = new Item(new string[] { "table" }, "table", "Wooden table");
            newPlayer.Location = newLocation;
            newLocation.Inventory.Put(table);

            Assert.AreEqual(otherLocation.Locate("table"), null);
        }

        // test full description
        [Test()]
        public void TestFullDescription()
        {
            Location newLocation = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            Player newPlayer = new Player("John", "A mighty explorer");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);

            Item table = new Item(new string[] { "table" }, "table", "Wooden table");
            newPlayer.Location = newLocation;
            newLocation.Inventory.Put(table);
            newPlayer.Location.AddPath(paSouth);

            string mssg = "You are in home\nHome Sweet Home!\nThere are exits to the south\nIn this room you can see: \n\ttable (table)";

            Assert.AreEqual(newLocation.FullDescription, mssg);
        }
    }
}
