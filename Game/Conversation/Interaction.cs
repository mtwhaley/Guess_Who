using Characters;

namespace Conversation {
    class Interaction {

        private Guess_Who.Deductions deductions;
        private string focusCharacteristic;
        private string focusDetails;

        public Interaction() {
            deductions=new();
        }


        public void AskAQuestion(List<Character> possibleCharacters, bool hard=true) {

            deductions.ReloadPossibleCharacteristics(possibleCharacters);
            Random random = new Random();
            
            object specifiedCharacteristic;
            if (hard) {
                focusCharacteristic = deductions.getIdealQuestionCharacteristic(possibleCharacters);
                specifiedCharacteristic = deductions.getIdealSpecifics(possibleCharacters, focusCharacteristic);
                if (specifiedCharacteristic is string) {
                    focusDetails = (string) specifiedCharacteristic;
                }
                else focusDetails="";

            }
            else {
                
            List<string> questionTopics = deductions.GetUnknownCharacteristics();
                focusCharacteristic= questionTopics[random.Next(0, questionTopics.Count - 1)];
                Character selectedCharacter = possibleCharacters[random.Next(0, possibleCharacters.Count -1)];
                specifiedCharacteristic = selectedCharacter.GetCharacteristic(focusCharacteristic);

                if (specifiedCharacteristic is string) focusDetails = (string)specifiedCharacteristic;
                else focusDetails = "";
            }

            Question question = questionTemplates[focusCharacteristic];
            string questionToAsk = question.GetFullQuestion(focusDetails);
            SystemUtil.Util.Write(questionToAsk+"\n");
        }

        public List<Character> evaluateResponse(List<Character> possibleCharacters, string response) {
            return deductions.filterPossibleCharacters(possibleCharacters, focusCharacteristic, focusDetails, SystemUtil.Util.IsAcceptance(response));

        }

        private Dictionary<string, Question> questionTemplates = new Dictionary<string, Question>{
            {"Name", new Question("Is your person ")},
            {"Gender", new Question("Is your person a ")},
            {"EyeColor", new Question("Does your person have ", " eyes")},
            {"HairColor", new Question("Does your person have ", " hair")},
            {"Beard", new Question("Does your person have a beard")},
            {"Mustache", new Question("Does your person have a mustache")},
            {"BigNose", new Question("Does your person have a big nose")},
            {"Glasses", new Question("Is your person wearing glasses")},
            {"Hat", new Question("Is your person wearing a hat")},
            {"RedCheeks", new Question("Does your person have red cheeks")},
            {"Bald", new Question("Is your person bald")}
        };
    }
}