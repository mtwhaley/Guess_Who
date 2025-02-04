namespace Characters {
    class CharacterRepository {
        
        private readonly static string _connectionString = MySqlDatabase.ConnectionString.Get();

        private static List<Character> allCharacters;

        private static void Initialize() {
            allCharacters = new List<Character>();
            Fill();
        }

        private static void Fill() {
            CharacterConverter databaseInterpreter = new CharacterConverter();
            string query = "SELECT * FROM characters;";
            var db = new MySqlDatabase.Database(_connectionString);
            List<Dictionary<string, object>> results = db.ExecuteQuery(query);
            results.ForEach(result=>{
                Character entity = databaseInterpreter.convertDatabaseEntryToEntity(result);
                allCharacters.Add(entity);
            });
            
        }

        public static List <Character> GetAll() {
            if (allCharacters==null) Initialize();
            return allCharacters;
        }


    }
}