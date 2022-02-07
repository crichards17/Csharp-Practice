using System;

namespace GuessingGame 
{
    class GuessingGame 
    {
        static void Main(string[] args) 
        {
            Random r = new Random();
            int num = r.Next(0, 100);
            bool win = false;

            do {
                Console.Write("Guess a number between 0 and 100: ");
                string s = Console.ReadLine();
                try {
                    int i = int.Parse(s);
                    if (i > num) {
                        Console.WriteLine("Guess lower.");
                    } else if (i < num) {
                        Console.WriteLine("Guess Higher");
                    } else {
                        Console.WriteLine("Correct!");
                        win = true;
                    }
                } catch (System.FormatException) {
                    Console.WriteLine("Guess must be an integer.");
                }

            } while (win == false);

            Console.WriteLine();
            Console.WriteLine("You win!");
        }
    }
}
