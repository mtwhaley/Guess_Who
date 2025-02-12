using Characters;
using Conversation;

namespace Guess_Who {
    class GamePlay {

        private static Character secretCharacter;
        private static List<Character> guessableCharacters;
        private static Interaction interaction;

        private static bool _difficult;

        

        public static void GameLoop() {
            interaction = new();

            while (guessableCharacters.Count>0) {
                
                ComputerTurn();

                string guess = PlayerTurn();
                if (guess!=null) {
                    SystemUtil.Util.Write("Is my person "+guess+"?\n");
                    if (secretCharacter.Name.ToLower() == guess.ToLower()) {
                        SystemUtil.Util.Write("Yes!!\n\n\n");
                        PlayerWin();
                        break;
                    }
                    else {
                        SystemUtil.Util.Write("No!!\n\n\n");
                    }

                }
                if (guessableCharacters.Count == 1) {
                    SystemUtil.Util.Write("Your person is "+guessableCharacters[0].Name+"!!\n\n\n");
                    SystemUtil.Util.Write("My person was "+secretCharacter.Name+"\n");
                    break;

                }
            }
        }

        private static void PlayerWin() {
            SystemUtil.Util.Write("You win! Congratulations!\n");
        }

        private static string PlayerTurn() {

            SystemUtil.Util.Write("Your turn!\n\t");

            string userQuestion = Console.ReadLine();
            string? nameGuess = Interpreter.NameGuess(userQuestion);
            if (nameGuess!=null) return nameGuess;
            else {
                try {
                    Interpreter.interpretUserInput(userQuestion);
                    AnswerQuestion();
                } catch (GameExceptions.InvalidInputException e) {
                    SystemUtil.Util.Write(e.Message+"\n");
                    PlayerTurn();
                }
            }
            return null;
        }

        private static void AnswerQuestion() {
            
            object characterTrait = secretCharacter.GetCharacteristic(Conversation.Interpreter.focusCharacteristic);
            if (characterTrait is bool) {
                bool heldTrait = (bool)characterTrait;
                if (Conversation.Interpreter.focusNegation != heldTrait) SystemUtil.Util.Write("Yes!!\n\n\n");
                else SystemUtil.Util.Write("No!!\n\n\n");
            }
            else {
                string heldTrait = (string)characterTrait;
                string questionedValue = Conversation.Interpreter.focusSubcharacteristic;
                bool faceValue = object.Equals(heldTrait, questionedValue);
                if (Conversation.Interpreter.focusNegation != faceValue) SystemUtil.Util.Write("Yes!!\n\n\n");
                else SystemUtil.Util.Write("No!!\n\n\n");
            }
        }

        private static void ComputerTurn() {
            interaction.AskAQuestion(guessableCharacters, _difficult);
            string response = SystemUtil.Util.ReadYesOrNo();
            guessableCharacters = interaction.evaluateResponse(guessableCharacters, response);
        }

        public static void SetUp(bool difficult = false) {
            List<Character> all = CharacterRepository.GetAll();
            secretCharacter = GameUtil.PickRandomCharacter(all);
            guessableCharacters = all;
            _difficult = difficult;
            return;
        }
    }
}