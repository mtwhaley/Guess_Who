
namespace Conversation {
    class Question {
        private string _firstPart;
        private string _secondPart;

        public Question(string firstPart, string secondPart="") {
            _firstPart=firstPart;
            _secondPart=secondPart;
        }

        public string GetFullQuestion(object specifiedCharacteristic) {
            if (specifiedCharacteristic is string) return _firstPart + specifiedCharacteristic + _secondPart + "?";
            return _firstPart + _secondPart + "?";
        }
    }
}