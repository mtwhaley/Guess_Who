﻿
// todo: add readme
// todo: add difficulty setting
// todo: have image open

using SystemUtil;

namespace Guess_Who {

    class Program {

        static void Main(string[] args) {

            Util.ConfigureConsole();

            
            bool begin = GameEnter.GameBeginPrompt();
            if (begin) Util.OpenPicture();

            while (begin) {
                GamePlay.SetUp();
                GamePlay.GameLoop();
                begin = GameEnter.PlayAgainPrompt();
            }

            Util.Write("welp, bye!\n");
            Console.ReadKey();
        }
        
    }

    
}