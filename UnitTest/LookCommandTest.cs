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
    class LookCommandTest
    {
        // return player's description
        [Test()]
        public void TestLookAtMe()
        {
            Player john = new Player("John", "a courageous traveller");
            LookCommand lookCmd = new LookCommand();

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "inventory" }),john.FullDescription);
        }

        // return gem's description when looking at gems
        [Test()]
        public void TestLookAtGem()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            LookCommand lookCmd = new LookCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem" }), gem.FullDescription);
        }

        // return "i can't find the gem"
        public void TestLookAtUnk()
        {
            Player john = new Player("John", "a courageous traveller");
            LookCommand lookCmd = new LookCommand();

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem" }), "I can't find the gem");
        }

        // return gem's description when looking in player's inventory
        [Test()]
        public void TestLookAtGemInMe()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            LookCommand lookCmd = new LookCommand();

            john.Inventory.Put(gem);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem", "in", "inventory" }), gem.FullDescription);
        }

        // return gem's description when looking in player's bag that is in player's inventory
        [Test()]
        public void TestLookAtGemInBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag mainBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            LookCommand lookCmd = new LookCommand();

            john.Inventory.Put(mainBag);
            mainBag.Inventory.Put(gem);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem", "in", "bag" }), gem.FullDescription);
        }

        // return "I can't find the bag" if bag is not found
        [Test()]
        public void TestLookAtGemInNoBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            LookCommand lookCmd = new LookCommand();
            
            john.Inventory.Put(gem);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem", "in", "bag" }), "I can't find the bag");
        }

        // return "I can't find the gem in bag" if the gem is not found in bag
        [Test()]
        public void TestLookAtNoGemInBag()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag mainBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            LookCommand lookCmd = new LookCommand();

            john.Inventory.Put(mainBag);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem", "in", "bag" }), "I can't find the gem in level 1 bag");
        }

        // check all errors in loop commands
        [Test()]
        public void TestInvalidLook()
        {
            Player john = new Player("John", "a courageous traveller");
            LookCommand lookCmd = new LookCommand();

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "around" }), "I don't know how to look like that");
            Assert.AreEqual(lookCmd.Execute(john, new string[] { "wassupppppp" }), "Error in look input");
            Assert.AreEqual(lookCmd.Execute(john, new string[] { "peak", "at", "bag" }), "Error in look input");
            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "into", "bag" }), "What do you want to look at?");
            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look", "at", "gem", "at", "bag" }), "What do you want to look in?");
        }

        // test "look"
        [Test()]
        public void TestLook()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag mainBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Location home = new Location(new string[] { "home", "safeplace" }, "home", "Home Sweet Home!");
            LookCommand lookCmd = new LookCommand();

            john.Location = home;
            home.Inventory.Put(mainBag);

            Assert.AreEqual(lookCmd.Execute(john, new string[] { "look" }), home.FullDescription);
        }
    }
}
