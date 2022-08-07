using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SwinAdventure;

namespace UnitTest
{
    [TestFixture()]
    class PlayerTest
    {
        // player has default id of me and inventory
        [Test()]
        public void TestPlayerIdentifiable()
        {
            Player myPlayer = new Player("John", "A wise adventurer");
            Assert.AreEqual(myPlayer.AreYou("me"),true);
            Assert.AreEqual(myPlayer.AreYou("inventory"), true);
        }

        // player able to locate item in inventory
        [Test()]
        public void TestLocateItems()
        {
            Player myPlayer = new Player("John", "A wise adventurer");
            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            myPlayer.Inventory.Put(mySword);
            Assert.AreEqual(myPlayer.Locate("sword"), mySword);
            Assert.AreEqual(myPlayer.Inventory.HasItem("sword"), true);
        }

        // player can locate itself
        [Test()]
        public void TestPlayerLocateItself()
        {
            Player myPlayer = new Player("John", "A wise adventurer");
            Assert.AreEqual(myPlayer.Locate("me"), myPlayer);
            Assert.AreEqual(myPlayer.Locate("inventory"), myPlayer);
        }

        // player locates nothing - return null
        [Test()]
        public void TestPlayerLocateNothing()
        {
            Player myPlayer = new Player("John", "A wise adventurer");
            Assert.AreEqual(myPlayer.Locate("gun"), null);
        }

        // player return full description
        [Test()]
        public void TestFullDescription()
        {
            Player myPlayer = new Player("John", "A wise adventurer");

            Item mySword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            Item myShovel = new Item(new string[] { "shovel" }, "a dusty shovel", "Once a golden shovel, now a dusty one");
            Item mySpear = new Item(new string[] { "spear" }, "an ancient spear", "A spear forged in ancient Rome");
            myPlayer.Inventory.Put(mySword);
            myPlayer.Inventory.Put(myShovel);
            myPlayer.Inventory.Put(mySpear);

            string myFullDesc = "You are John A wise adventurer\nYou are carrying\n";
            myFullDesc += myPlayer.Inventory.ItemList;

            Assert.AreEqual(myPlayer.FullDescription, myFullDesc);
        }
    }
}
