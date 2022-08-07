using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SwinAdventure;

namespace UnitTest
{
    [TestFixture()]
    class InventoryTest
    {
        // item exists in the inventory
        [Test()]
        public void TestFindItem()
        {
            Inventory myInventory = new Inventory();
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myInventory.Put(mySword);
            Assert.AreEqual(myInventory.HasItem("sword"), true);
        }

        // item doesn't exist in the inventory
        [Test()]
        public void TestItemNotFound()
        {
            Inventory myInventory = new Inventory();
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myInventory.Put(mySword);
            Assert.AreEqual(myInventory.HasItem("spear"), false);
        }

        // returns item and it remains in inventory
        [Test()]
        public void TestFetchItem()
        {
            Inventory myInventory = new Inventory();
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myInventory.Put(mySword);
            Assert.AreEqual(myInventory.Fetch("sword"), mySword);
            Assert.AreEqual(myInventory.HasItem("sword"), true);
        }

        // returns item and it is no longer in inventory
        [Test()]
        public void TestTakeItem()
        {
            Inventory myInventory = new Inventory();
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myInventory.Put(mySword);
            Assert.AreEqual(myInventory.Take("sword"), mySword);
            Assert.AreEqual(myInventory.HasItem("sword"), false);
        }

        [Test()]
        // returns items in rows with its short description
        public void TestItemList()
        {
            Inventory myInventory = new Inventory();
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            Item myShovel = new Item(new string[] { "shovel" }, "a dusty shovel", "Once a golden shovel, now a dusty one");
            Item mySpear = new Item(new string[] { "spear" }, "an ancient spear", "A spear forged in ancient Rome");
            myInventory.Put(mySword);
            myInventory.Put(myShovel);
            myInventory.Put(mySpear);
            string myList = "\ta mighty sword (sword)\n\ta dusty shovel (shovel)\n\tan ancient spear (spear)";
            Assert.AreEqual(myInventory.ItemList, myList);
        }
    }
}