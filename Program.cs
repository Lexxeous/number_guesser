using System;
using System.Linq;
using System.Globalization;

namespace number_guesser
{
    class Program // main class
    {
        static void Main(string[] args) // program entry point
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // initialize app info text color

            // Setup Application Variables
            string app_name = "NumberGuesser";
            string app_version = "1.0.0";
            string app_author = "Alex Gibson";
            Console.WriteLine("{0}: Version {1} by {2}.\n", app_name, app_version, app_author); // write app info

            // INITIAL SETUP ----------------------------------------

            Console.ResetColor();

            Console.Write("What is your name?: ");
            string user_name = Console.ReadLine();

            Console.WriteLine("Hello {0}, let's play a game...",user_name);

            int play_again = 1; // enter <play_again == 1> while loop by default
            string[] yes_arr = {"Y", "YES"};
            string[] no_arr = { "N", "NO" };

            int guess_lower_bound = 1;
            int guess_upper_bound = 10;

            // START GAME LOGIC ----------------------------------------

            do
            {
                Random random = new Random(); // initialize new instance of Random class

                int correct_number = random.Next(guess_lower_bound, guess_upper_bound + 1); // generate random correct number ; "Next()" upper bound is non-inclusive
                int guess = 0; // initialize user's guess outside of guessing bounds

                Console.WriteLine("Guess a number between {0} and {1}, inclusive:\n", guess_lower_bound, guess_upper_bound);

                while (guess != correct_number)
                {
                    string guess_str = Console.ReadLine(); // console inputs are strings by default
                    if (!int.TryParse(guess_str, out guess))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a numeric value.");
                        Console.ResetColor();
                        continue;
                    }
                    guess = Int32.Parse(guess_str); // cast guess string datatype into a 32-bit integer datatype

                    if (guess < guess_lower_bound || guess > guess_upper_bound)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your guess is out of bounds.");
                        Console.ResetColor();
                    }

                    if (guess != correct_number)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Your guess is incorrect, please try again...");
                        Console.ResetColor();
                    } 
                } // guessed correct number, end of guessing while loop

                if (guess == correct_number) // confirm that guess is equivalent to the correct random number
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You guessed the correct number, good job!\n");
                    Console.ResetColor();
                }

                // PLAY AGAIN GAME LOGIC ----------------------------------------

                do
                { 
                    Console.Write("Would you like to play again?: ");
                    string play_input = Console.ReadLine();
                    string play_str = play_input.ToUpper(new CultureInfo("en-US", false)); // convert string input to all uppercase letters

                    if (yes_arr.Contains(play_str))
                    {
                        play_again = 1;
                        Console.WriteLine("Restarting the game...");
                        Console.WriteLine("Generating a new random number...");
                    }
                    else if (no_arr.Contains(play_str))
                    {
                        play_again = 0;
                        Console.WriteLine("Exiting the game...");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Input is invalid. Please enter (n|no) or (y|yes).");
                        play_again = -1;
                        Console.ResetColor();
                    }
                } while (play_again == -1); // error checking for proper <play_again> input

            } while (play_again == 1); // when user actually wants to play again

            // END OF GAME REMARKS ----------------------------------------

            Console.WriteLine("Thank you so much for playing! Please come again!");

            // END OF GAME LOGIC ----------------------------------------

            // Reset console colors for final console output information
            Console.ResetColor();

        }
    }
}
