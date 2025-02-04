using Characters;

namespace Guess_Who {
    class GameUtil {
        public static Character PickRandomCharacter(List<Character> charactersToChooseFrom) {
            Random random = new();
            int index = random.Next(1, charactersToChooseFrom.Count);
            return charactersToChooseFrom[index];
        }
    }
}