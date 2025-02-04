using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Characters;

namespace Guess_Who {
    class Deductions {
        private Dictionary<string, List<object>> possibleCharacteristics;

        public Deductions() {
            possibleCharacteristics = new();
        }

        public List<Character> filterPossibleCharacters(List<Character> possibleCharacters, string focusCharacteristic, string focusDetails, bool affirm) {
            if (possibleCharacters[0].GetCharacteristic(focusCharacteristic) is bool) {
                return possibleCharacters.Where(character=> (bool)character.GetCharacteristic(focusCharacteristic) == affirm).ToList();
            }
            if (affirm) {
                return possibleCharacters.Where(character => (string)character.GetCharacteristic(focusCharacteristic) == focusDetails).ToList();
            }
            else {
                return possibleCharacters.Where(character => (string)character.GetCharacteristic(focusCharacteristic) != focusDetails).ToList();

            }
        }

        
        public void ReloadPossibleCharacteristics(List<Character> possibleCharacters) {
            possibleCharacteristics.Clear();
            List<string> allCharacteristics = Characters.CharacterUtil.allPossibleCharacteristics.Keys.ToList();
            possibleCharacters.ForEach(character=>{
                allCharacteristics.ForEach(characteristic=>{
                    if (!possibleCharacteristics.ContainsKey(characteristic)) {
                        possibleCharacteristics[characteristic] = new();
                    }
                    if (!possibleCharacteristics[characteristic].Contains(character.GetCharacteristic(characteristic))) {
                        possibleCharacteristics[characteristic].Add(character.GetCharacteristic(characteristic));
                    }

                });
            });
        }

        public List<string> GetUnknownCharacteristics() {
            List<string> unknownCharacteristics = new();
            possibleCharacteristics.Keys.ToList().ForEach(characteristic=>{
                if (possibleCharacteristics[characteristic].Count>1) {
                    unknownCharacteristics.Add(characteristic);
                }
            });

            return unknownCharacteristics;
        }

        
        
        public string CharacteristicToAsk(List<Character> guessableCharacters) {
            if (guessableCharacters.Count ==1 ) return "Name";
            return "";
        }
    }
}