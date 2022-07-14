using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject")]

namespace RockPaperScissors
{

    public class Program
    {
        static void Main(string[] args)
        {
            // Welcome the Gamer
            Welcome();
            Gamer human = CreateUser();
            int gamePlayNumber = 1;
            bool endGame = false;
            List<Result> results = new List<Result>();
            while (!endGame)
            {
                Gamer computer = new Gamer(RandomGamerName(), GamerType.Computer);
                Console.WriteLine("Your openent is " + computer.getName());
                Gameplay gameplay = new Gameplay(human, computer, gamePlayNumber++);
                Result result = gameplay.startGameplay();

                if (result == Result.FirstWins)
                    Console.WriteLine("Congratulations " + human.getName() + "!");
                else if (result == Result.SecondWins)
                    Console.WriteLine("Congratulations " + computer.getName() + "!");
                else
                    Console.WriteLine("Draw! Nice try, both of you :)");
                // Store result
                results.Add(result);
                int wins = results.Where(r => r == Result.FirstWins).Count();
                int losses = results.Where(r => r == Result.SecondWins).Count();
                int draws = results.Where(r => r == Result.Draw).Count();
                // Show information about number of wins, losses and draws
                string msgTemplate = "The number of wins is: {0}. The number of losses is: {1}. The number of draws is {2}.";
                string message = string.Format(msgTemplate, wins, losses, draws);
                Console.WriteLine(message);
                // Ask if user want to quit the game
                Console.WriteLine("If you want to quit press Q, to continue press any other button");
                ConsoleKeyInfo info = Console.ReadKey(true);
                endGame = info.Key == ConsoleKey.Q;
            }
            Console.WriteLine("Thanks for playing " + human.getName() + "!");
        }
        static void Welcome()
        {
            Console.WriteLine("Welcome to the game!");
        }
        internal static Gamer CreateUser()
        {
            string userName = "";
            while (string.IsNullOrEmpty(userName))
            {
                Console.WriteLine("Please provide your name:");
                userName = Console.ReadLine();
                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("The empty name provide. Please try again to type your name");
                }
                else if (userName.Length > 20)
                {
                    Console.WriteLine("The name is too long. Please provide a name with less than 20 characters. Please try again to type your name");
                    userName = "";
                }
                else if (userName.Any(char.IsDigit))
                {
                    Console.WriteLine("The name contains a number. Please try again to type your name without a number");
                    userName = "";
                }
            }

            Gamer human = new Gamer(userName, GamerType.Human);
            // Welcome new human gamer
            Console.WriteLine("Good luck " + userName + "!");

            return human;
        }  
        internal static string RandomGamerName()
        {
            List<string> names = new List<string>() { "Adam", "Robert", "Pierre", "Lucile", "Eva", "Kate" };
            var random = new Random();
            int index = random.Next(names.Count);
            return names[index];
        }
    }

    
}
