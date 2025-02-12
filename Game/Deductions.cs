using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Characters;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Tls;

namespace Guess_Who {
    class Deductions {
        private Dictionary<string, List<object>> possibleCharacteristics;

        public Deductions() {
            possibleCharacteristics = new();
        }

        public string getIdealQuestionCharacteristic(List<Character> possibleCharacters) {
            Dictionary<string, object> counter = new();
            List<string> questionTopics = GetUnknownCharacteristics();
            int numCharacters = possibleCharacters.Count;
            

            questionTopics.ForEach(topic=>{
                counter.Add(topic,0);
                if (possibleCharacters[0].GetCharacteristic(topic) is string) {
                    if (possibleCharacteristics[topic].Count > 2) {
                        Dictionary<string, int> subcounter = new();

                        possibleCharacteristics[topic].ForEach(subcharacteristic=>{
                            subcounter[(string)subcharacteristic] = 0;
                            possibleCharacters.ForEach(character=>{
                                if (object.Equals(character.GetCharacteristic(topic), subcharacteristic)) {
                                    subcounter[(string)subcharacteristic] += 1;
                                }
                            });
                    });
                    counter[topic] = subcounter;
                    }
                    
                    else {
                        string randomValue = (string)possibleCharacters[0].GetCharacteristic(topic);
                        possibleCharacters.ForEach(character=>{
                        if (object.Equals(character.GetCharacteristic(topic), randomValue)) {
                            counter[topic] = (int)counter[topic] + 1;
                        }
                    });
                    }

                }
                else {
                    possibleCharacters.ForEach(character=>{
                        if (object.Equals(character.GetCharacteristic(topic),true)) {
                            counter[topic] = (int) counter[topic] + 1;
                        }
                    });
                }
            });


            string midMost = null;
            int midMostCount = 0;
            int halfway = numCharacters/2;
            counter.Keys.ToList().ForEach(key=>{
                if (counter[key] is Dictionary<string, int> dictionary ) {
                    dictionary.Keys.ToList().ForEach(subkey=> {
                        if (Math.Abs(halfway - dictionary[subkey])< Math.Abs(halfway-midMostCount)) {
                            midMost = key;
                            midMostCount = dictionary[subkey];
                        }
                        
                    });
                }
                else {
                    if (Math.Abs(halfway - (int)counter[key]) < Math.Abs(halfway - midMostCount)) {
                        midMost = key;
                        midMostCount = (int)counter[key];
                    }
                    
                }
            });

            return midMost;
         
        }
        public object getIdealSpecifics(List<Character> possibleCharacters, string focusCharacteristic)  {
            
            if (possibleCharacters[0].GetCharacteristic(focusCharacteristic) is bool) return true;
            if (focusCharacteristic.ToLower() == "gender") return "male";

            Dictionary <string, int> optionCounter = new();

            possibleCharacters.ForEach((character)=>{
                string subcharacteristic = (string)character.GetCharacteristic(focusCharacteristic);
                if (!optionCounter.ContainsKey(subcharacteristic)) optionCounter.Add(subcharacteristic, 1);
                else optionCounter[subcharacteristic]++;
            });
            
            int midWay = possibleCharacters.Count/2;
            string midMost = null;
            optionCounter.Keys.ToList().ForEach(key=>{
                if (midMost == null) midMost = key;
                else {
                    if ( Math.Abs(midWay - optionCounter[key] ) < Math.Abs(midWay - optionCounter[midMost])) midMost = key;
                }
            });

            

            return midMost;
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