using System;
using Microsoft.Extensions.DependencyInjection;
using Snap.Entities;
using Snap.Interfaces;

namespace Snap
{
    internal class Program
    {
        private static void Main()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IDeck, SnapDeck>();
            var serviceProvider = collection.BuildServiceProvider();
            var deck = serviceProvider.GetService<IDeck>();

            Console.WriteLine("Welcome to Snap Virtual! \nWhat is your name?");
            var name = Console.ReadLine();

            Console.WriteLine("Welcome {0} \nHow many virtual players would you like to play with?", name);
            var extraPlayers = Convert.ToInt32(Console.ReadLine());

            //Start game
            var game = new Game(name, extraPlayers, deck);

            Console.WriteLine(
                "Rules: " +
                "\n - The objective of this game is to win all the cards in play!" +
                "\n - Each player is dealt a pile of cards. " +
                "\n - On each turn, the game will automatically draw a card from your pile and display it on the screen" +
                "\n - This card will be added to a 'middle pile'" +
                "\n - Players should SNAP if the face of the last two cards played match! e.g. K Hearts and K of Clubs. " +
                "\n - To SNAP, press the 's' key." +
                "\n - Players have one second between each card to SNAP. " +
                "\n - If you are the first to SNAP correctly then you win all the cards in the 'middle pile'." +
                "\n - If you are the first to SNAP but you are incorrect (there's no match), then you have to give a card to each other player.");
            Console.WriteLine("When you're ready to start, press enter...");
            Console.ReadLine();
            Console.WriteLine("Starting...");
            game.PlayGame();
        }
    }
}