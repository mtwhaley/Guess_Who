using Characters;
using GameExceptions;
using Guess_Who;

namespace Conversation {
    class Interpreter {

        private static readonly Dictionary<string, string> FAMILIAR_TRANSLATIONS = new Dictionary<string, string> {
            {"HairColor", "hair"},
            {"EyeColor", "eyes"},
            {"Gender", "gender"},
            {"Beard", "beard"},
            {"Mustache", "mustache"},
            {"BigNose", "big nose"},
            {"Glasses", "glasses"},
            {"Hat", "hat"},
            {"RedCheeks", "red cheeks"},
            {"Bald", "bald"},
        };

        private static readonly Dictionary<string, List<string>> SUBCHARACTERISTIC_SYNONYMS = new Dictionary<string, List<string>>{
            {"male", ["man", "boy"]},
            {"female", ["woman", "girl"]}
        };

        public static string focusCharacteristic;
        public static string focusSubcharacteristic;
        public static bool focusNegation;

        

        public static void interpretUserInput(string input) {
            focusCharacteristic=null;
            focusSubcharacteristic=null;
            focusNegation = SystemUtil.Util.HasNegation(input);
            
            FindKeyWords(input);
            if (focusCharacteristic == null) return;
            
            return;
        }



        public static string NameGuess(string userQuestion) {
            List<Character> allCharacters = CharacterRepository.GetAll();
            List<string> allNames= allCharacters.Select(character=>character.Name).ToList();
            foreach (string name in allNames) {
                if (userQuestion.ToLower().IndexOf(name.ToLower())!=-1) {
                    return name;
                }
            }
            return null;
        }

        public static void RestateUserInput(string characteristic, string subcharacteristic) {
            if (subcharacteristic.Length>0) SystemUtil.Util.Write(subcharacteristic+" ");
            SystemUtil.Util.Write(FAMILIAR_TRANSLATIONS[characteristic]+"?\n");

        }

        private static string GetCharacteristicBySubCharacteristic(string subcharacteristic) {
            foreach (string characteristic in CharacterUtil.allPossibleCharacteristics.Keys.ToList()) {
                if (CharacterUtil.allPossibleCharacteristics[characteristic].IndexOf(subcharacteristic)!=-1) {
                    return characteristic;
                }
            };
            return null;
        }

        private static void FindKeyWords(string input) {
            List<string> allCharacteristics = CharacterUtil.allPossibleCharacteristics.Keys.ToList();
            List<string> possibleKeywords = new();
            List<string> characteristicsMentioned = new();
            List<string> subcharacteristicsMentioned = new();
            int attributesMentioned = 0;

            allCharacteristics.ForEach(characteristic=>{
                bool attributeMentionedByName = false;
                if (input.ToLower().IndexOf((FAMILIAR_TRANSLATIONS[characteristic]).ToLower()) != -1) {
                    attributesMentioned+=1;
                    characteristicsMentioned.Add(characteristic);
                    attributeMentionedByName = true;
                }
                if (CharacterUtil.allPossibleCharacteristics[characteristic][0] is string) {
                    CharacterUtil.allPossibleCharacteristics[characteristic].ForEach(subcharacteristic=>{
                        if (input.ToLower().IndexOf(((string)subcharacteristic).ToLower()) != -1) {
                            if (!SystemUtil.Util.IsColor((string)subcharacteristic) || attributeMentionedByName) {
                                subcharacteristicsMentioned.Add((string)subcharacteristic);
                                if (!attributeMentionedByName) {
                                    attributesMentioned+=1;
                                    characteristicsMentioned.Add(characteristic);
                                }
                            }
                        }
                    });
                }

            });

            if (characteristicsMentioned.Count == 0 && subcharacteristicsMentioned.Count == 0) {
                SUBCHARACTERISTIC_SYNONYMS.Keys.ToList().ForEach(subcharWithSynonyms=>{
                    SUBCHARACTERISTIC_SYNONYMS[subcharWithSynonyms].ForEach(synonym=>{
                        if (input.ToLower().IndexOf(synonym)!=-1) {
                            string subcharacteristic = subcharWithSynonyms;
                            subcharacteristicsMentioned.Add((string)subcharacteristic);
                            attributesMentioned+=1;
                            characteristicsMentioned.Add(GetCharacteristicBySubCharacteristic(subcharacteristic));
                            
                            
                        }
                    });
                });
            }

            if ((characteristicsMentioned.Count == 1) && (subcharacteristicsMentioned.Count<2)) {
                RestateUserInput(characteristicsMentioned[0], subcharacteristicsMentioned.Count >0 ? subcharacteristicsMentioned[0]: "");
                focusCharacteristic = characteristicsMentioned[0];
                if (subcharacteristicsMentioned.Count >0) focusSubcharacteristic=subcharacteristicsMentioned[0];
            }

            if (characteristicsMentioned.Count == 0) {
                throw new InvalidInputException();
            }

        }
    }
}