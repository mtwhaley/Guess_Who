using System.Reflection;

namespace Characters {
    class CharacterConverter {
        static Dictionary<string, string> entityAttributeMappings;

        public CharacterConverter() {
            entityAttributeMappings = new Dictionary<string, string>
            {
                {"character_id", "CharacterId"},
                { "name", "Name" },
                { "gender", "Gender" },
                { "eyes", "EyeColor" },
                { "hair", "HairColor" },
                { "beard", "Beard" },
                { "mustache", "Mustache" },
                { "big_nose", "BigNose" },
                { "glasses", "Glasses" },
                { "hat", "Hat" },
                { "red_cheeks", "RedCheeks" },
                { "bald", "Bald" }
            };
        }

        public Character convertDatabaseEntryToEntity(Dictionary<string, object> data) {
            Character entity = new();
            
            data.Keys.ToList().ForEach(key=>{
                string entityAttribute = entityAttributeMappings[key];
                entity.SetCharacteristic(entityAttribute, data[key]);
            });

            return entity;

        }
    }
}