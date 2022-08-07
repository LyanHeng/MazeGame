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
    class TakeCommandTest
    {
        // test full description
        [Test()]
        public void TestFullDescription()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand putCmd = new TransferCommand();

            john.Location.Inventory.Put(gem);
            string mssg = "You have taken the red gem from home";

            Assert.AreEqual(putCmd.Execute(john, new string[] { "pickup", "gem" }), mssg);
        }

        // test take item
        [Test()]
        public void TestTakeItem()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            john.Location.Inventory.Put(gem);
            takeCmd.Execute(john, new string[] { "take", "gem" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), gem);
            Assert.AreEqual(john.Location.Inventory.Fetch("gem"), null);
        }

        // test not take item
        [Test()]
        public void TestNotTakeItem()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            john.Location.Inventory.Put(gem);
            takeCmd.Execute(john, new string[] { "take", "sword" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), null);
            Assert.AreEqual(john.Location.Inventory.Fetch("gem"), gem);
        }

        // test take item from bag
        [Test()]
        public void TestTakeItemFromBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag bag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            bag.Inventory.Put(gem);
            john.Inventory.Put(bag);
            takeCmd.Execute(john, new string[] { "take", "gem", "from", "bag" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), gem);
            Assert.AreEqual(bag.Inventory.Fetch("gem"), null);
        }

        // test take item from not bag
        [Test()]
        public void TestNotTakeItemFromBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag bag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            john.Inventory.Put(bag);
            john.Inventory.Put(gem);
            takeCmd.Execute(john, new string[] { "take", "gem", "from", "bag" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), gem);
            Assert.AreEqual(bag.Inventory.Fetch("gem"), null);
        }

        // test take item from a new location
        [Test()]
        public void TestTakeItemFromNewLocation()
        {
            Player john = new Player("John", "a courageous traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            gardenSouth.Inventory.Put(gem);
            john.Location = gardenSouth;
            takeCmd.Execute(john, new string[] { "take", "gem", "from", "garden" });

            Assert.AreEqual(john.Inventory.Fetch("gem"), gem);
            Assert.AreEqual(gardenSouth.Inventory.Fetch("gem"), null);
        }

        // test invalid takes
        [Test()]
        public void TestInvalidTakes()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "red gem", "a shiny odd gem");
            TransferCommand takeCmd = new TransferCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(takeCmd.Execute(john, new string[] { "take" }), "I can't do that!");
            Assert.AreEqual(takeCmd.Execute(john, new string[] { "take gem" }), "I can't do that!");
            Assert.AreEqual(takeCmd.Execute(john, new string[] { "pickup", "gem", "in", "garden" }), "I can't find garden");
            Assert.AreEqual(takeCmd.Execute(john, new string[] { "take", "gem", "from", "home", "please" }), "I can't do that!");
        }
    }
}
