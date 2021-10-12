using System;
using System.Linq;
using System.Globalization;

namespace number_guesser
{
    class Program // main class
    {
        static string[] yes_arr = { "Y", "YES" };
        static string[] no_arr = { "N", "NO" };

        static void Main(string[] args) // program entry point
        {
            // Print initial application information. 
            print_app_info();
            greet_user();

            // INITIAL SETUP ----------------------------------------

            Int16 play_again; // if user wants to play again {0 -> false, 1 -> true, -1 -> error}
            Int16 change_bounds = 1; // if user wants to change bounds; set to "true" by default {0 -> false, 1 -> true, -1 -> error}
            bool first_pass = true; // if user has made a pass through the game already; set to "true" by default

            Int32 guess_lower_bound = -1;
            Int32 guess_upper_bound = 0;

            do // restart whole game loop
            {
                if (!first_pass) { // only ask to change bounds after the first game
                    change_bounds = yes_or_no_question("Would you like to change the bounds?: ", yes_str: "Clearing previous bounds...", no_str: "Keeping current bounds...");
                }

                if (change_bounds == 1) {
                    do // <choose_bound> loop
                    {
                        // Let user pick lower bound.
                        guess_lower_bound = choose_bound("lower");

                        // Let the user pick upper bound.
                        guess_upper_bound = choose_bound("upper");

                        // Bounds must have correct cardinal order.
                        if (guess_upper_bound <= guess_lower_bound)
                            print_msg("Upper bound cannot be less than or equal to the lower bound. Choose valid values.", status: "ERROR");

                    } while (guess_upper_bound <= guess_lower_bound);
                }

                // START GAME LOGIC ----------------------------------------

                Random random = new Random(); // initialize new instance of <Random> class
                int correct_number = random.Next(guess_lower_bound, guess_upper_bound + 1); // generate random correct number ; "Next()" upper bound is non-inclusive
                int guess = int.MaxValue; // initialize <guess> to be wrong and out of bounds

                Console.WriteLine("Guess a number between {0} and {1}, inclusive:", guess_lower_bound, guess_upper_bound);

                while(true) {
                    string guess_str = Console.ReadLine(); // console inputs are strings by default
                    if (!Int32.TryParse(guess_str, out guess)) {
                        print_msg("Please enter a signed, 32-bit, integer value.", status: "error", newline: false);
                        continue;
                    }
                    guess = Int32.Parse(guess_str); // cast guess string datatype into a 32-bit integer datatype

                    if (guess < guess_lower_bound || guess > guess_upper_bound) {
                        print_msg("Your guess is out of bounds.", status: "error", newline: false);
                        continue;
                    }

                    if (guess != correct_number) {
                        print_msg("Your guess is incorrect. Please try again...", status: "error", newline: false);
                        continue;
                    }

                    if (guess == correct_number) {
                        print_msg("You guessed the correct number, good job!", status: "success"); // game success
                        break;
                    }
                }

                // PLAY AGAIN GAME LOGIC ----------------------------------------

                play_again = yes_or_no_question("Would you like to play again?: ", yes_str: "Restarting the game...", no_str: "Exiting the game...");
                
                first_pass = false; // user has already made it through the game once

            }while (play_again == 1); // when user actually wants to play again

            // END OF GAME ----------------------------------------

            print_msg("Thank you so much for playing! Please come again!");
            
            Console.ResetColor(); // reset console colors for final console output information
        }

        private static void print_app_info(bool color_reset = true)
        {
            /*
             * Description: Prints the name, version, and author of this application.
             * Input(s):
             *   (1) Boolean: <color_reset> (default: true) - Enables/disables the internal call to the "Console.ResetColor()" function.
             * Output(s): N/A. This is a void function.
            */

            // Setup Application Variables
            Console.ForegroundColor = ConsoleColor.Yellow; // initialize app info text color
            string app_name = "NumberGuesser";
            string app_version = "1.0.0";
            string app_author = "Alex Gibson";
            Console.WriteLine($"{app_name}: Version {app_version} by {app_author}.\n"); // write app info
            if (color_reset) {
                Console.ResetColor();
            }
        }

        private static void print_msg(string str, ConsoleColor f_color = ConsoleColor.Gray, ConsoleColor b_color = ConsoleColor.Black, bool newline = true, bool color_reset = true, string status = "NONE")
        {
            /*
             * Description: Prints a string with options for setting/modifying background colors, foreground colors, and newlines.
             * Input(s):
             *   (1) String: <str> - The string to be printed.
             *   (2) ConsoleColor: <f_color> (default: Gray) - The foreground color of the printed text.
             *   (3) ConsoleColor: <b_color> (default: Black) - The background color of the printed text.
             *   (4) Boolean: <newline> (default: true) - Enables/disables appending an additional newline character to the end of the printed text.
             *   (5) Boolean: <color_reset> (default: true) - Enables/disables the internal call to the "Console.ResetColor()" function.
             *   (6) String: <status> (default: "NONE") - Can pick foreground color presets from the following. NOTE: Setting <status> to anything other than "NONE" will override <f_color>.
             *     {"SUCCESS" -> Green, "WARN" -> Yellow, "ERROR" -> Red, "DEFAULT" -> White}
             * Output(s): N/A. This is a void function.
            */

            // Do error checking on input parameters
            validate_str_param(str);

            // The <status> parameter takes precedence for the foreground color over the <f_color> parameter
            status = status.ToUpper(new CultureInfo("en-US", false));

            // Detect print status for foreground color
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
            Console.WriteLine(str); // print string with or without the newline appended

            if (color_reset) {
                Console.ResetColor();
            }
        }

        private static void greet_user()
        {
            /*
             * Description: Get user's name and welcome them to the game.
             * Input(s): N/A. No parameters.
             * Output(s): N/A. This is a void function.
            */

            print_msg("What is your name?:", newline: false, status: "DEFAULT");

            while (true)
            {
                string user_name = Console.ReadLine(); // console inputs are strings by default
                if (user_name.All(Char.IsLetter)) { // if <user_name> characters are all alpha
                    print_msg("Hello " + user_name + " let's play a game...", status: "DEFAULT");
                    break;
                }
                else { // if <user_name> characters are not all alpha
                    print_msg("All characters in your name must be alpha.", status: "error", newline: false);
                    continue;
                }
            }
        }

        private static Int32 choose_bound(string bound_str)
        {
            /*
             * Description: Set an upper or lower 32-bit integer bound, based on the <bound_str> input parameter.
             * Input(s):
             *   (1) String: <bound_str> - The string value that decides which bound to set. Value must be ("upper"|"UPPER") or ("lower"|"LOWER").
             * Output(s):
             *   (1) Int32: <guess_bound> - The 32-bit upper or lower bound that is to be set.
            */

            // Do error checking on input parameters
            validate_str_param(bound_str);

            // Initialize local variables
            Int32 guess_bound; // value to be returned
            string b_msg; // "lower" or "upper"
            string direction; // "minimum" or "maximum"
            Int32 val; // min or max Int32 value
            bool nl; // new line temporary variable

            // Check value of input parameter <bound_str> then set local variables accordingly
            bound_str = bound_str.ToUpper(new CultureInfo("en-US", false)); // convert <bound_str> to all uppercase
            if (bound_str == "UPPER") {
                b_msg = "upper";
                direction = "maximum";
                val = Int32.MaxValue;
                nl = true;
            }
            else if (bound_str == "LOWER") {
                b_msg = "lower";
                direction = "minimum";
                val =Int32.MinValue;
                nl = false;
            }
            else {
                throw new ArgumentException("<bound_str>", "must contain value (\"upper\"|\"UPPER\") or (\"lower\"|\"LOWER\")");
            }

            // Print variable bound choosing message(s) based on the <bound_str>
            print_msg("Choose " + b_msg + " bound for the number guessing game:", status: "DEFAULT", newline: false);
            print_msg("The " + direction + " value allowed is " + val.ToString("#,#", CultureInfo.InvariantCulture) + ".", status: "DEFAULT", newline: false);
            while (true)
            {
                string guess_bound_str = Console.ReadLine(); // console inputs are strings by default
                if (!Int32.TryParse(guess_bound_str, out guess_bound)) { // if the input cannot be parsed as a 32-bit integer
                    print_msg("Please enter a signed, 32-bit, integer value.", status: "error", newline: false);
                    continue;
                }
                else { // if the input can be parsed as a 32-bit integer
                    guess_bound = Int32.Parse(guess_bound_str);
                    print_msg("The " + b_msg + " bound is set to: " + guess_bound.ToString("#,#", CultureInfo.InvariantCulture), status: "DEFAULT", newline: nl);
                    break;
                }
            }

            return guess_bound;
        }

        private static void validate_str_param(string s)
        {
            /*
             * Description: Throws an exception if an input argument/parameter is not of type String, is empty, or is of type null.
             * Input(s): N/A. No parameters.
             * Output(s): N/A. This is a void function.
            */

            if (!(s is String) || String.IsNullOrEmpty(s)) {
                throw new ArgumentException("<s>", "must be of type String, cannot be empty, and cannot be of type null.");
            }
        }

        private static Int16 yes_or_no_question(string question, string yes_str, string no_str)
        {

            /*
             * Description: Ask a yes or no question and return the result.
             * Input(s):
             *   (1) String: <question> - The question to ask the user.
             *   (2) String: <yes_str> - The message to print if the user chooses "yes".
             *   (3) String: <no_str> - The message to print if the user chooses "no".
             * Output(s):
             *   (1) Int16: <result> - The answer to the question. 1 for "yes" and 0 for "no".
             *   NOTE: Inside the function, <result> can also hold a value of -1 if the user's input is invalid.
            */

            // Input validation
            validate_str_param(question);
            validate_str_param(yes_str);
            validate_str_param(no_str);

            Int16 result;

            do {
                print_msg(question, newline: false);
                string input = Console.ReadLine();
                string caps_str = input.ToUpper(new CultureInfo("en-US", false)); // convert string input to all uppercase letters

                if (yes_arr.Contains(caps_str)) {
                    result = 1;
                    print_msg(yes_str);
                }
                else if (no_arr.Contains(caps_str)) {
                    result = 0;
                    print_msg(no_str);
                }
                else {
                    print_msg("Input is invalid. Please enter (n|no) or (y|yes).", status: "error");
                    result = -1;
                }
            } while (result == -1); // error checking for proper <result> input

            return result;
        }
    }
}
