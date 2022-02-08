using System;

namespace DiceRoller
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
            Run();
            End();
        }

        //Intro text on startup
        private static void Start() {
            Console.WriteLine("\nWelcome to Dice Rolling! \nEnter any combination of dice using the syntax \"XdY+Z\".\nFor example: \"2d4+2\" will return a roll of 2 d4 and add 2.\nType \"help\" or \"h\" at any time to access the syntax guide.");

            Console.WriteLine("\nPress any key to begin.");
            //TODO: Implement syntax guide
            Console.ReadKey();
        }

        private static void Run() {
            bool end = false;
            
            while (!end) {
                Console.Write("\nEnter your roll: ");
                String entry = Console.ReadLine();
                if (entry.ToLower().Equals("end")) {
                    end = true;
                    break;
                }
                String[] args = entry.Split('d','+');

                int count = 0;
                int die = 0;
                int mod = 0;
                bool modIncluded = false;
                bool modValid = false;
                
                bool countValid = int.TryParse(args[0], out count);
                bool dieValid = int.TryParse(args[1], out die);
                if (args.Length > 2 && args[2] != null) {
                    modIncluded = true;
                    modValid = int.TryParse(args[2], out mod);
                }

                if (args.Length < 2 || args.Length > 3 || !countValid || !dieValid || (modIncluded && !modValid)) {
                    Console.WriteLine("Format not recognized.\nType \"Help\" or \"h\" to review the syntax guide.\nType \"End\" to end.");
                } else {
                    int roll = Roll(count, die, mod);
                    Console.WriteLine($"Roll result: {roll}");
                    Console.WriteLine($"Average roll:{(((Double)die + 1) / 2.0) * count + mod}");
                }
            }
        }

        private static int Roll(int count, int die, int mod) {
            int total = 0;
            var rand = new Random();
            for (int i = 0; i < count; i++) {
                total += rand.Next(1, die + 1);
            }
            return total += mod;
        }

        private static void End() {
            Console.WriteLine("\nThank you for rolling!");
        }
    }
}
