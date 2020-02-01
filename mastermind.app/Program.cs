using System;
using System.Linq;
using System.Collections.Generic;
using mastermind.app.models;
using mastermind.app.bll;

namespace mastermind.app
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int minNumber = 1, maxNumber = 7;
            Console.Write("\n*********************************************");
            Console.Write("\n************* MasterMind ********************");
            Console.Write("\n*********************************************");
            Console.Write("\n\n********************************************");

            Console.Write("\nWelcome to Mastermind.");
            Console.Write($"\nYou have 10 tries to guess a 4 digit number using digits from {minNumber} to {maxNumber - 1}.");
            Console.Write("\nKeep in mind that the 4 digit number may have duplicate numbers.");
            Console.Write("\nA (-) symbol will appear for every digit that is correct, but in the wrong position.");
            Console.Write("\nA (+) symbol will appear for every digit that is correct and in the correct position.");
            Console.Write("\nNothing will be printed for incorrect digits.");
            Console.Write("\nLet's begin. ");
            Console.Write("\n********************************************\n\n");
            bool exitgame = false;
            ConsoleKeyInfo keypressed;
            Random rnd = new Random();

            while (!exitgame)
            {
                int attempts = 0;
                string currentstring = "";
                bool numberguessed = false;
                List<MasterNumber> numberToGuess = new List<MasterNumber>()
                {
                    new MasterNumber{ Position = 0, Value = rnd.Next(minNumber,maxNumber) },
                    new MasterNumber{ Position = 1, Value = rnd.Next(minNumber,maxNumber) },
                    new MasterNumber{ Position = 2, Value = rnd.Next(minNumber,maxNumber) },
                    new MasterNumber{ Position = 3, Value = rnd.Next(minNumber,maxNumber) }
                };

                Console.Write("\nEnter your guess number : ");
                while (attempts < 10 && !numberguessed)
                {
                    keypressed = Console.ReadKey(true);
                    currentstring = CustomValidator.SetInputValue(currentstring, keypressed);
                    if (CustomValidator.InputReadyToCheck(currentstring, keypressed))
                    {
                        attempts++;
                        numberguessed = CustomValidator.GuessedNumber(currentstring, numberToGuess);
                        if (!numberguessed)
                        {
                            currentstring = "";
                            if (attempts < 10)
                            {
                                Console.Write($"\nAttempt number {attempts + 1} : ");
                            }
                        }
                    }
                }
                if (!numberguessed)
                {
                    Console.Write($"\nSorry you did not guess the number:  {String.Join("", numberToGuess.Select(x => x.Value))}");
                }
                exitgame = CustomValidator.ContinueGame();
            }
        }

    }
}
