using SystemUtil;

namespace Guess_Who {
    class GameEnter {

        public static bool difficultPrompt() {
            Util.Write("Should I go easy on you?\n");

            string response = Util.ReadYesOrNo("Do you want me to go easy?");

            if (Util.IsAcceptance(response)) return false;
            return true;
        }

        public static bool PlayAgainPrompt() {
            Util.Write("That was fun!!\nDo you want to play again?!\n");

            string response = Util.ReadYesOrNo("... So do you want to play Guess Who again?");

            if (Util.IsAcceptance(response)) {return true;}
            return false;
        }
        public static bool GameBeginPrompt() {
            Util.Write("Care to play a game of Guess Who?!\n");

            string response = Util.ReadYesOrNo("... So do you want to play Guess Who?");


            if (Util.IsAcceptance(response)) {return true;}
            return false;
        }
    }
}