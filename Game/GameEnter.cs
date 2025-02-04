namespace Guess_Who {
    class GameEnter {

        public static bool PlayAgainPrompt() {
            SystemUtil.Util.Write("That was fun!!\nDo you want to play again?!\n");

            string response = SystemUtil.Util.ReadYesOrNo("... So do you want to play Guess Who again?");

            if (SystemUtil.Util.IsAcceptance(response)) {return true;}
            return false;
        }
        public static bool GameBeginPrompt() {
            SystemUtil.Util.Write("Care to play a game of Guess Who?!\n");

            string response = SystemUtil.Util.ReadYesOrNo("... So do you want to play Guess Who?");


            if (SystemUtil.Util.IsAcceptance(response)) {return true;}
            return false;
        }
    }
}