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
    class PutCommandTest
    {
        // test put's description
        [Test()]
        public void TestFullDescription()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);
            string mssg = "You have put the red gem in home";

            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "gem" }), mssg);
        }

        // test put item
        [Test()]
        public void TestPutItem()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);
            putCmd.Execute(john, new string[] { "put", "gem" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), null);
            Assert.AreEqual(john.Location.Inventory.Fetch("gem"), gem);
        }

        // test not put item
        [Test()]
        public void TestNotPutItems()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "sword" }), "I can't find sword in home");
            Assert.AreEqual(john.Location.Inventory.Fetch("gem"), null);
        }

        // test put item in bag
        [Test()]
        public void TestPutItemInBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag bag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);
            john.Inventory.Put(bag);
            putCmd.Execute(john, new string[] { "put", "gem", "in", "bag" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), null);
            Assert.AreEqual(bag.Inventory.Fetch("gem"), gem);
        }

        // test put item in not bag
        [Test()]
        public void TestPutItemInNotBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag bag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Location.Inventory.Put(gem);

            Assert.AreEqual(john.Inventory.Fetch("gem"), null);
            Assert.AreEqual(bag.Inventory.Fetch("gem"), null);
        }

        // test put item in a new location
        [Test()]
        public void TestPutItemInNewLocation()
        {
            Player john = new Player("John", "a courageous traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);
            // would not use this in program. This is only done to make things less complex.
            john.Location = gardenSouth;
            putCmd.Execute(john, new string[] { "put", "gem" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), null);
            Assert.AreEqual(gardenSouth.Inventory.Fetch("gem"), gem);
        }

        // test put item in not location
        [Test()]
        public void TestPutItemInNotLocation()
        {
            Player john = new Player("John", "a courageous traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);
            putCmd.Execute(john, new string[] { "put", "gem", "in", "garden" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), gem);
            Assert.AreEqual(john.Location.Inventory.Fetch("gem"), null);
            Assert.AreEqual(gardenSouth.Inventory.Fetch("gem"), null);
        }

        // test container not found
        [Test()]
        public void TestContainerNotFound()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "gem", "in", "bag" }), "I can't find bag");
            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "gem", "in", "garden" }), "I can't find garden");
        }

        // test invalid puts 
        [Test()]
        public void TestInvalidPuts()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(putCmd.Execute(john, new string[] { "put" }), "I can't do that!");
            Assert.AreEqual(putCmd.Execute(john, new string[] { "put gem" }), "I can't do that!");
            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "gem", "to", "garden" }), "put gem in/from what/where?");
            Assert.AreEqual(putCmd.Execute(john, new string[] { "put", "gem", "in", "home", "please" }), "I can't do that!");
        }
    }
}
