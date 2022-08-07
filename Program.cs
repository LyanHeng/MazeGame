using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwinAdventure;

namespace MazeGame
{
    public class Program
    {
        static void Main(string[] arg)
        {
            // register all commands the player can use
            CommandProcessor cmd = new CommandProcessor();
            LookCommand lookCmd = new LookCommand();
            MoveCommand moveCmd = new MoveCommand();
            PutCommand putCmd = new PutCommand();
            TakeCommand takeCmd = new TakeCommand();
            QuitCommand quitCmd = new QuitCommand();
            cmd.RegisterCommand(lookCmd);
            cmd.RegisterCommand(moveCmd);
            cmd.RegisterCommand(putCmd);
            cmd.RegisterCommand(takeCmd);
            cmd.RegisterCommand(quitCmd);

            // welcome title
            Console.WriteLine("Welcome to Swin Adventure!");
            Console.WriteLine("Enter player's name and descriptions to begin!");

            // player's info from user
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();

            // create player object
            Player newPlayer = new Player(name, desc);
            Item table = new Item(new string[] { "table" }, "table", "Wooden table");
            Item chestBox = new Item(new string[] { "chest", "box" }, "chest", "A golden chest box, what lies inside?");
            newPlayer.Location.Inventory.Put(table);
            newPlayer.Location.Inventory.Put(chestBox);

            // create two items add it into player's inventory
            Item goldCoin = new Item(new string[] { "gold", "coin" }, "a golden coin", "A shiny golden coin!");
            Item shovel = new Item(new string[] { "shovel" }, "a dusty shovel", "Dust covered old shovel.");
            newPlayer.Inventory.Put(goldCoin);
            newPlayer.Inventory.Put(shovel);

            // create a bag and add to player's inventory
            Bag newBag = new Bag(new string[] { "bag", "level 1" }, "level 1 bag", "It's a bag, what else did you expect?");
            newPlayer.Inventory.Put(newBag);

            // create another item and add to bag
            Item newSword = new Item(new string[] { "sword" }, "a mighty sword", "A finely crafted sword, forged to perfection");
            newBag.Inventory.Put(newSword);

            // create items for shop
            Item chair = new Item(new string[] { "chair" }, "a wooden chair", "A sturdy looking chair");
            Item shopShovel = new Item(new string[] { "shovel", "shop" }, "a dusty shovel", "Dust covered old shovel.");

            // create paths (from home)
            Location garden = new Location(new string[] { "garden" }, "a garden", "Beautiful garden!");
            Path homeSouth = new Path(new string[] { "south", "s" }, "south", "You go through a door", garden);
            Location shop = new Location(new string[] { "shop", "local" }, "a local shop", "Where you can get everything!");
            Path homeNorth = new Path(new string[] { "north", "n" }, "north", "You enter the local shop", shop);
            Location restaurant = new Location(new string[] { "restaurant" }, "a burger restaurant", "The smell of burger roams the place");
            Path shopEast = new Path(new string[] { "east", "e" }, "east", "You opened the door and oh my, you'd love a burger", restaurant);
            Location gym = new Location(new string[] { "gym" }, "a gym", "An open modern gym");
            Path shopNorth = new Path(new string[] { "north", "n" }, "north", "You're ready to lift some weight", gym);

            // set paths
            Location home = newPlayer.Location;
            home.AddPath(homeNorth);
            home.AddPath(homeSouth);
            shop.AddPath(shopEast);
            shop.AddPath(shopNorth);

            // add item to shop
            shop.Inventory.Put(chair);
            shop.Inventory.Put(shopShovel);

            // loop reading commands from user
            while (true)
            {
                Console.Write("\n");
                Console.Write("Command -> ");
                string[] command = Console.ReadLine().ToLower().Split();

                // execute the command through the command processor
                Console.WriteLine(cmd.Execute(newPlayer, command));
            }
        }
    }
}