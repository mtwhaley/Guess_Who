using System.Reflection;

namespace Characters {
    class CharacterUtil {

        public static readonly Dictionary<string, List<object>> allPossibleCharacteristics = new Dictionary<string, List<object>> {
            {"HairColor", ["brown", "blonde", "black", "red", "white"]},
            {"EyeColor", ["brown", "blue"]},
            {"Gender", ["male", "female"]},
            {"Beard", [true, false]},
            {"Mustache", [true, false]},
            {"BigNose", [true, false]},
            {"Glasses", [true, false]},
            {"Hat", [true, false]},
            {"RedCheeks", [true, false]},
            {"Bald", [true, false]},
        };

        

        public static object GetCharacteristic(Character character, string characteristicName) {
            Type characterType = typeof(Character);
            PropertyInfo propertyInfo = characterType.GetProperty(characteristicName);
            return propertyInfo.GetValue(character);
        }

    }
}