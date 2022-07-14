using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject")]

namespace RockPaperScissors
{
    enum Answer
    {
        Paper,
        Rock,
        Scissors,
        Unknown
    }

    enum Result
    {
        FirstWins,
        SecondWins,
        Draw
    }

    internal class Gameplay
    {
        int _gameplayNumber;
        Gamer _firstGamer;
        Gamer _secondGamer;
        public int getGameplayNumber() { return _gameplayNumber; }
        internal Answer getGamerAnswer(Gamer gamer)
        {
            Answer gamerAnswer = Answer.Unknown;
            if (gamer.getType() == GamerType.Computer)
                gamerAnswer = gamer.randomChoice();
            else
            {
                string userAnswer = "";
                while (string.IsNullOrEmpty(userAnswer))
                {
                    Console.WriteLine("Please provide your answer: (R)ock, (P)aper, (S)cissors:");
                    userAnswer = Console.ReadLine();
                    if (string.Equals(userAnswer, "R", StringComparison.OrdinalIgnoreCase))
                        gamerAnswer = Answer.Rock;
                    else if (string.Equals(userAnswer, "P", StringComparison.OrdinalIgnoreCase))
                        gamerAnswer = Answer.Paper;
                    else if (string.Equals(userAnswer, "S", StringComparison.OrdinalIgnoreCase))
                        gamerAnswer = Answer.Scissors;
                    else
                    {
                        Console.WriteLine("You can provide only values of R, P or S. Please try again.");
                        userAnswer = "";
                    }
                }
            }
            return gamerAnswer;
        }
        internal Result getResult(Answer firstAnswer, Answer secondAnswer)
        {
            // First wins
            if (firstAnswer == Answer.Rock && secondAnswer == Answer.Scissors)
                return Result.FirstWins;
            else if (firstAnswer == Answer.Paper && secondAnswer == Answer.Rock)
                return Result.FirstWins;
            else if (firstAnswer == Answer.Scissors && secondAnswer == Answer.Paper)
                return Result.FirstWins;
            // Draw
            else if (firstAnswer == secondAnswer)
                return Result.Draw;
            // Second wins
            else
                return Result.SecondWins;
        }
        public Result startGameplay()
        {
            Answer firstGamerAnswer = getGamerAnswer(_firstGamer);
            Answer secondGamerAnswer = getGamerAnswer(_secondGamer);
            return getResult(firstGamerAnswer, secondGamerAnswer);     
        }
    
        public Gameplay(Gamer firstGamer, Gamer secondGamer, int gameplayNumber)
        {
            _gameplayNumber = gameplayNumber;
            _firstGamer = firstGamer;
            _secondGamer = secondGamer;

            Console.WriteLine("The gameplay number " + gameplayNumber
               + " will soon begin. Good luck for " + _firstGamer.getName() + " and " + _secondGamer.getName());
        }
    }
}

