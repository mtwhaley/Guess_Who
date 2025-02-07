
// todo: add readme
// todo: add difficulty setting
// todo: have image open

namespace Guess_Who {

    class Program {

        static void Main(string[] args) {

            SystemUtil.Util.ConfigureConsole();
            
            bool begin = GameEnter.GameBeginPrompt();

            while (begin) {
                GamePlay.SetUp();
                GamePlay.GameLoop();
                begin = GameEnter.PlayAgainPrompt();
            }

            SystemUtil.Util.Write("welp, bye!\n");
            Console.ReadKey();
        }
    }
}