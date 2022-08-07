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
    class BagTest
    {
        // items can be added to bag, locate returns item without deletion
        [Test()]
        public void TestLocateBag()
        {
            Bag myBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myBag.Inventory.Put(mySword);
            Assert.AreEqual(myBag.Locate("sword"), mySword);
            Assert.AreEqual(myBag.Inventory.HasItem("sword"), true);
        }

        // bag locates itself
        [Test()]
        public void TestLocateItself()
        {
            Bag myBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Assert.AreEqual(myBag.Locate("bag"), myBag);
            Assert.AreEqual(myBag.Locate("carriage"), myBag);
        }

        // bag returns nil if asked something it doesn't have
        [Test()]
        public void TestLocateNothing()
        {
            Bag myBag = new Bag(new string[] { "bag" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myBag.Inventory.Put(mySword);
            Assert.AreEqual(myBag.Locate("shovel"), null);
        }

        // bag returns full description
        [Test()]
        public void TestFullDescription()
        {
            Bag myBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            Item myShovel = new Item(new string[] { "shovel" }, "a dusty shovel", "Once a golden shovel, now a dusty one");
            Item mySpear = new Item(new string[] { "spear" }, "an ancient spear", "A spear forged in ancient Rome");
            myBag.Inventory.Put(mySword);
            myBag.Inventory.Put(myShovel);
            myBag.Inventory.Put(mySpear);
            string fullDesc = "In the level 1 bag you can see:\n";
            fullDesc += myBag.Inventory.ItemList;
            Assert.AreEqual(myBag.FullDescription, fullDesc);
        }

        // bag can locate another bag but not access it, while has its own items
        [Test()]
        public void TestBagInBag()
        {
            Bag myBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Bag miniBag = new Bag(new string[] { "minibag" }, "mini-bag", "Keep all your money safe.");
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            Item coins = new Item(new string[] { "coins", "gold" }, "the game finance", "what are you going to do without money?");
            miniBag.Inventory.Put(coins);
            myBag.Inventory.Put(miniBag);
            myBag.Inventory.Put(mySword);
            Assert.AreEqual(myBag.Locate("minibag"), miniBag);
            Assert.AreEqual(myBag.Locate("sword"), mySword);
            Assert.AreEqual(myBag.Locate("coins"), null);
        }
    }
}
