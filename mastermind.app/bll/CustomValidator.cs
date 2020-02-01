using mastermind.app.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mastermind.app.bll
{
    public static class CustomValidator
    {
        private static readonly int Maxdigits = 4;
        public static string SetInputValue(string currentstring, ConsoleKeyInfo keypressed)
        {
            if (keypressed.Key != ConsoleKey.Backspace && currentstring.Length + 1 <= Maxdigits)
            {
                var _x = double.TryParse(keypressed.KeyChar.ToString(), out double val);
                if (val >= 1 && val <= 6 && _x)
                {
                    currentstring += keypressed.KeyChar;
                    Console.Write(keypressed.KeyChar);
                }
            }
            else
            {
                if (keypressed.Key == ConsoleKey.Backspace && currentstring.Length > 0)
                {
                    currentstring = currentstring.Substring(0, (currentstring.Length - 1));
                    Console.Write("\b \b");
                }
                else if (keypressed.Key == ConsoleKey.Enter && Maxdigits < currentstring.Length)
                {
                    Console.Write($"\nPlease write a number of {Maxdigits} digits");
                    currentstring = "";
                }
            }

            return currentstring;
        }

        public static bool InputReadyToCheck(string currentstring, ConsoleKeyInfo keypressed)
        {
            return keypressed.Key == ConsoleKey.Enter && Maxdigits == currentstring.Length;
        }
        public static bool GuessedNumber(string numberinserted, List<MasterNumber> numberToGuess)
        {
            var guessednumber = numberinserted.Select((n, i) => new MasterNumber { Position = i, Value = int.Parse(n.ToString()) }).ToList();
            var correctValue = string.Concat(Enumerable.Repeat("+", numberToGuess.Count(x => guessednumber.Any(y => y.Position == x.Position && y.Value == x.Value))));
            numberToGuess.ForEach(x => guessednumber.Where(y => y.Position != x.Position && y.Value == x.Value));
            var wrongValue = string.Concat(Enumerable.Repeat("-", guessednumber.Where(x => numberToGuess.Any(y => y.Position != x.Position && y.Value == x.Value)).GroupBy(x=> x.Value).Select(x=> x.First()).Count()));
            var result = string.Concat(correctValue, wrongValue);
            if (correctValue.Length == Maxdigits)
            {
                Console.Write($"\nCongratulations! You got the number {numberinserted}");
                return true;
            }
            if (result.Length > 0)
            {
                Console.Write($"\nGetting close:  {result}");
            }
            return false;
        }

        public static bool ContinueGame()
        {
            Console.Write("\nDo you want to start a new game? (y/n)");
            var keypressed = Console.ReadKey(true);
            var validresponse = false;
            while (!validresponse)
            {

                if (keypressed.KeyChar.ToString().ToUpper() == "Y")
                {
                    Console.Clear();
                    return false;
                }
                else if (keypressed.KeyChar.ToString().ToUpper() == "N")
                {
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.Write("\nPlease type a valid answer");
                    Console.Write("\nDo you want to start a new game? (y/n)");
                    keypressed = Console.ReadKey(true);
                }
            }
            return true;
        }
    }


}
