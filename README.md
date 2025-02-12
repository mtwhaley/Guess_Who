# Guess Who Console Application (C#)

## Description

The "Guess Who" game is a simple turn-based console game written in C# where a player and the computer must guess each other's character based on a series of yes/no questions. The application randomly selects a character and the player needs to ask questions that lead to narrowing down the options until they can guess the correct character or the computer figures out the player's choice.

## Features

- **Character Selection**: The game selects a random character from a predefined list.
- **Question and Answer Logic**: The player can ask yes/no questions to narrow down the character.
- **Game Loop**: The game continues until either the player or computer correctly guesses the other character.
- **Replay Option**: The user has the option to play again after finishing the game.

## How to Run

1. Clone this repository to your local machine.
2. Open the project in an IDE or text editor (e.g., Visual Studio or Visual Studio Code).
3. Build and run the `Guess Who` project.
4. Follow the on-screen instructions to play the game.

### Prerequisites

- .NET Core SDK (for C#)
- A compatible IDE or text editor (e.g., Visual Studio)
- MySQL database with a table 'characters', populated with character information corresponding to the physical characteristics of the roster, with the following fields:
  - character_id (int)
  - name (varchar)
  - gender (varchar, 'male' or 'female')
  - eyes (varchar, 'blue' or 'brown')
  - hair (varchar, 'black', 'blonde', 'brown', 'red', or 'white')
  - beard (bool)
  - mustache (bool)
  - big_nose (bool)
  - glasses (bool)
  - hat (bool)
  - red_cheeks (bool)
  - bald (bool)

## How to Play

1. The game will prompt the user if they want to play.
2. If the user opts to play a game, an image file containing the roster will open and the user will be prompted for difficulty
3. The game will select a character from the roster.
4. The computer and user will take turns asking yes/no questions about the physical characteristics of the characters.
5. If the computer narrows the list down to only one candidate, it will announce the character and declare a victory. If you narrow down the computer's character before the computer figures it out, make a guess and the game will tell you if you're correct.
6. The computer will ask the user if they want to play again. If the user agrees, steps 3-6 repeat. If the user declines, the game exits.

## Acceptable questions

The game is configured to ask and answer question regarding a set of pre-defined characteristics. The characteristics are as follows:

- name
- gender
  - male (also 'man' or 'boy')
  - female (also 'woman' or 'girl')
- eye color
  - blue
  - brown
- hair color
  - black
  - blonde
  - brown
  - red
  - white
- beard
- mustache
- glasses
- hat
- big nose
- bald
- red cheeks
