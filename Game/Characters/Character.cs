using System.Reflection;
using Org.BouncyCastle.Pqc.Crypto.Bike;

namespace Characters {

    
    class Character {
        private int _characterId;
        private string _name;
        private string _gender;
        private string _eyeColor;
        private string _hairColor;
        private bool _beard;
        private bool _mustache;
        private bool _bigNose;
        private bool _glasses;
        private bool _hat;
        private bool _redCheeks;
        private bool _bald;

        public Character() {
            _characterId = 0; 
            _name = ""; 
            _gender = ""; 
            _eyeColor = ""; 
            _hairColor = ""; 
            _beard = false; 
            _mustache = false; 
            _bigNose = false; 
            _glasses = false; 
            _hat = false; 
            _redCheeks = false; 
            _bald = false; 
        }

        public Character(string name, string gender, string eyeColor, string hairColor, bool beard, bool mustache, bool bigNose, bool glasses, bool hat, bool redCheeks, bool bald ) {
            _name = name;
            _gender = gender;
            _eyeColor = eyeColor;
            _hairColor = hairColor;
            _beard = beard;
            _mustache = mustache;
            _bigNose = bigNose;
            _glasses = glasses;
            _hat = hat;
            _redCheeks = redCheeks;
            _bald = bald;
        }

        public int CharacterId {
        get { return _characterId; }
        set { _characterId = value; }
        }

        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        public string Gender {
            get { return _gender; }
            set { _gender = value; }
        }

        public string EyeColor {
            get { return _eyeColor; }
            set { _eyeColor = value; }
        }

        public string HairColor {
            get { return _hairColor; }
            set { _hairColor = value; }
        }

        public bool Beard {
            get { return _beard; }
            set { _beard = value; }
        }

        public bool Mustache {
            get { return _mustache; }
            set { _mustache = value; }
        }

        public bool BigNose {
            get { return _bigNose; }
            set { _bigNose = value; }
        }

        public bool Glasses {
            get { return _glasses; }
            set { _glasses = value; }
        }

        public bool Hat {
            get { return _hat; }
            set { _hat = value; }
        }

        public bool RedCheeks {
            get { return _redCheeks; }
            set { _redCheeks = value; }
        }

        public bool Bald {
            get { return _bald; }
            set { _bald = value; }
        }

        public object GetCharacteristic(string characteristicName) {
            if (string.IsNullOrEmpty(characteristicName))
                throw new ArgumentException("Characteristic name cannot be null or empty.", nameof(characteristicName));

            PropertyInfo propertyInfo = this.GetType().GetProperty(characteristicName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Property '{characteristicName}' does not exist on Character.", nameof(characteristicName));

            try
            {
                return propertyInfo.GetValue(this);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving property '{characteristicName}' value.", ex);
            }
        }

        public void SetCharacteristic(string characteristicName, object value) {
            if (string.IsNullOrEmpty(characteristicName)) {
                throw new ArgumentException("Characteristic name cannot be null or empty.", nameof(characteristicName));
            }

            Type characterType = typeof(Character);
            PropertyInfo propertyInfo = characterType.GetProperty(characteristicName);

            if (propertyInfo == null) {
                throw new ArgumentException($"Property '{characteristicName}' does not exist on type '{characterType.Name}'.", nameof(characteristicName));
            }

            if (!propertyInfo.CanWrite) {
                throw new InvalidOperationException($"Property '{characteristicName}' is read-only and cannot be modified.");
            }

            // Ensure the value is of the correct type for the property
            if (value != null && !propertyInfo.PropertyType.IsAssignableFrom(value.GetType())) {
                throw new ArgumentException($"Value type '{value.GetType().Name}' is not assignable to property '{characteristicName}' of type '{propertyInfo.PropertyType.Name}'.");
            }

            propertyInfo.SetValue(this, value);
        }


    }

        
}