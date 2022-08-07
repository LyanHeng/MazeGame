using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;

namespace UnitTest
{
    [TestFixture()]
    class ItemTest
    {
        // item is identifiable
        [Test()]
        public void TestItemIsIdentifiable()
        {
            Item myItem = new Item(new string[] {"sword", "spear", "shovel"}, "a mighty weapon", "A finely crafted weapon, forged to perfection");
            Assert.AreEqual(myItem.AreYou("sword"), true);
            Assert.AreEqual(myItem.AreYou("spear"), true);
            Assert.AreEqual(myItem.AreYou("shovel"), true);
        }

        // print item's short description
        [Test()]
        public void TestShortDescription()
        {
            Item myItem = new Item(new string[] { "sword", "spear", "shovel" }, "a mighty weapon", "A finely crafted weapon, forged to perfection");
            Assert.AreEqual(myItem.ShortDescription, "a mighty weapon (sword)");
        }

        // print item's full description
        [Test()]
        public void TestFullDescription()
        {
            Item myItem = new Item(new string[] { "sword", "spear", "shovel" }, "a mighty weapon", "A finely crafted weapon, forged to perfection");
            Assert.AreEqual(myItem.FullDescription, "A finely crafted weapon, forged to perfection");
        }
    }
}
