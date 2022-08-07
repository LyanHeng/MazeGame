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
    class CommandProcessorTest
    {
        // test register and search command
        [Test()]
        public void TestRegisterAndSearchCommand()
        {
            CommandProcessor cmd = new CommandProcessor();
            MoveCommand mC = new MoveCommand();
            cmd.RegisterCommand(mC);

            LookCommand lookCmd = new LookCommand();
            cmd.RegisterCommand(lookCmd);

            Assert.AreEqual(cmd.SearchCommand("move"), mC);
            Assert.AreEqual(cmd.SearchCommand("look"), lookCmd);
        }

        // test move command
        [Test()]
        public void TestMoveCommand()
        {
            Player john = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            CommandProcessor cmd = new CommandProcessor();

            MoveCommand mC = new MoveCommand();
            cmd.RegisterCommand(mC);

            john.Location.AddPath(paSouth);
            cmd.Execute(john, new string[] { "move", "south" });

            Assert.AreEqual(john.Location, gardenSouth);
        }

        // test look command
        [Test()]
        public void TestLookCommand()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag mainBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            CommandProcessor cmd = new CommandProcessor();

            LookCommand lookCmd = new LookCommand();
            cmd.RegisterCommand(lookCmd);

            john.Inventory.Put(mainBag);
            mainBag.Inventory.Put(gem);

            Assert.AreEqual(cmd.Execute(john, new string[] { "look", "at", "gem", "in", "bag" }), gem.FullDescription);
        }

        // test not registered command
        [Test()]
        public void TestNotRegisteredCommand()
        {
            Player john = new Player("John", "a courageous traveller");
            Bag mainBag = new Bag(new string[] { "bag", "carriage" }, "level 1 bag", "It's a bag, what else did you expect?");
            Item gem = new Item(new string[] { "gem" }, "a gem", "a shiny odd gem");
            CommandProcessor cmd = new CommandProcessor();

            LookCommand lookCmd = new LookCommand();

            john.Inventory.Put(mainBag);
            mainBag.Inventory.Put(gem);

            Assert.AreEqual(cmd.Execute(john, new string[] { "look", "at", "gem", "in", "bag" }), "look command not recognized");
        }

        // test invalid look command
        [Test()]
        public void TestInvalidLookCommand()
        {
            Player john = new Player("John", "a courageous traveller");
            CommandProcessor cmd = new CommandProcessor();

            LookCommand lookCmd = new LookCommand();
            cmd.RegisterCommand(lookCmd);

            Assert.AreEqual(cmd.Execute(john, new string[] { "look", "for", "inventory" }), "What do you want to look at?");
        }

        // test invalid move command
        [Test()]
        public void TestInvalidMove()
        {
            Player john = new Player("john", "a mighty traveller");
            Location gardenSouth = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path paSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", gardenSouth);
            CommandProcessor cmd = new CommandProcessor();

            MoveCommand mC = new MoveCommand();
            cmd.RegisterCommand(mC);

            john.Location.AddPath(paSouth);

            Assert.AreEqual(cmd.Execute(john, new string[] { "move", "quick" }), "quick does not exist!");
        }

        // test unidentified command
        [Test()]
        public void TestUnidentifiedCommand()
        {
            Player john = new Player("John", "a courageous traveller");
            CommandProcessor cmd = new CommandProcessor();

            Assert.AreEqual(cmd.Execute(john, new string[] { "hold", "sword" }), "hold command not recognized");
        }

        // test no command
        [Test()]
        public void TestNoCommand()
        {
            Player john = new Player("John", "a courageous traveller");
            CommandProcessor cmd = new CommandProcessor();

            Assert.AreEqual(cmd.Execute(john, new string[] { }), "command not found");
        }
    }
}
