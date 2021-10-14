<!-- README.md -->

# <img src=".pics/lexx_headshot_clear.png" width="100px"/> Lexxeous's .NET Number Guesser: <img src=".pics/guess_num_logo.png" width="100px"/>

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Windows](https://svgshare.com/i/ZhY.svg)](https://svgshare.com/i/ZhY.svg)
[![Generic badge](https://img.shields.io/badge/working-yes-bright_green_.svg)](https://shields.io/)

### Summary:

This is a **Windows** Console Application written with `C#` and built with **Microsoft's** software development framework, **.NET**.

> Partial credit (inspiration) goes to *Brad Traversy* for his [Build a C# .NET Application in 60 Minutes](https://www.youtube.com/watch?v=GcFJjpMFJvI) tutorial video on **YouTube**.

### Description:

To start the game, the user is greeted with a prompt to enter thier name.

The object of the game is to guess a randomly generated integer between a lower and an upper bound, determined by the user. The user can play the game again, as many times as they want. After the first game, the user also has the option to keep or change the guessing bounds. Error messages will inform the user when thier input is invalid. When the user guesses the correct number, they will be awarded with a success message.

### Game Constraints:

  * User defined bounds must be eligible as a signed, 32-bit integer.
    - The minimum allowed lower bound value is `-2,147,483,648`.
    - The maximum allowed upper bound value is `2,147,483,647`.
  * The upper bound value must be strictly greater than the lower bound value.
  * The upper bound, the lower bound, and the user's guess must be a signed, 32-bit, integer value.
  * The user's name must only contain alpha characters.