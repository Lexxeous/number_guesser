using System;
using System.Linq;
using System.Globalization;

namespace number_guesser
{
    class Program // main class
    {


        static void Main(string[] args) // program entry point
        {
            print_app_info();
            greet_user();

            // INITIAL SETUP ----------------------------------------

            int play_again = 0;

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

                Console.WriteLine("Guess a number between {0} and {1}, inclusive:", guess_lower_bound, guess_upper_bound);

                while (guess != correct_number)
                {
                    string guess_str = Console.ReadLine(); // console inputs are strings by default
                    if (!int.TryParse(guess_str, out guess)) {
                        print_msg("Please enter a numeric value.", status: "error");
                        continue;
                    }
                    guess = Int32.Parse(guess_str); // cast guess string datatype into a 32-bit integer datatype

                    if (guess < guess_lower_bound || guess > guess_upper_bound) {
                        print_msg("Your guess is out of bounds.", status: "error", newline: false);
                    }

                    if (guess != correct_number) {
                        print_msg("Your guess is incorrect. Please try again...", status: "error", newline: false);
                    } 
                } // guessed correct number, end of guessing while loop

                if (guess == correct_number) { // confirm that guess is equivalent to the correct random number
                    print_msg("You guessed the correct number, good job!", status: "success"); // game success
                }

                // PLAY AGAIN GAME LOGIC ----------------------------------------

                do
                { 
                    print_msg("Would you like to play again?: ", newline: false);
                    string play_input = Console.ReadLine();
                    string play_str = play_input.ToUpper(new CultureInfo("en-US", false)); // convert string input to all uppercase letters

                    if (yes_arr.Contains(play_str))
                    {
                        play_again = 1;
                        print_msg("Restarting the game...", newline: false);
                        print_msg("Generating a new random number...");
                    }
                    else if (no_arr.Contains(play_str))
                    {
                        play_again = 0;
                        print_msg("Exiting the game...");
                    }
                    else
                    {
                        print_msg("Input is invalid. Please enter (n|no) or (y|yes).", status: "error");
                        play_again = -1;
                    }
                } while (play_again == -1); // error checking for proper <play_again> input

            }while (play_again == 1); // when user actually wants to play again

            // END OF GAME REMARKS ----------------------------------------

            print_msg("Thank you so much for playing! Please come again!");

            // END OF GAME LOGIC ----------------------------------------

            // Reset console colors for final console output information
            Console.ResetColor();

        }

        static void print_app_info()
        { 
            // Setup Application Variables
            Console.ForegroundColor = ConsoleColor.Yellow; // initialize app info text color
            string app_name = "NumberGuesser";
            string app_version = "1.0.0";
            string app_author = "Alex Gibson";
            Console.WriteLine("{0}: Version {1} by {2}.\n", app_name, app_version, app_author); // write app info
            Console.ResetColor();
        }

        static void print_msg(string str, ConsoleColor f_color = ConsoleColor.Gray, ConsoleColor b_color = ConsoleColor.Black, bool newline = true, bool color_reset = true, string status = "NONE")
        {
            // The <status> parameter takes precedence for the foreground color over the <f_color> parameter
            status = status.ToUpper(new CultureInfo("en-US", false));

            // detect print status for foreground color
            if (status == "SUCCESS")
                f_color = ConsoleColor.Green;
            else if (status == "WARN")
                f_color = ConsoleColor.Yellow;
            else if (status == "ERROR")
                f_color = ConsoleColor.Red;
            else if (status == "DEFAULT")
                f_color = ConsoleColor.White;

            Console.ForegroundColor = f_color;
            Console.BackgroundColor = b_color;
            if (newline) {
                str = str + "\n";
            }
            Console.WriteLine(str);
            if (color_reset) {
                Console.ResetColor();
            }
        }

        static void greet_user()
        {
            print_msg("What is your name?:", newline: false);
            string user_name = Console.ReadLine();
            print_msg("Hello " + user_name + " let's play a game...");
        }
    }
}
