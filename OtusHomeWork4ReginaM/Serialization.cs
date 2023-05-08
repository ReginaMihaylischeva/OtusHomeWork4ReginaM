namespace OtusHomeWork4ReginaM
{
    class Serialization
    {
        private const string propertiesNameWord = "properties;";
        private const string fieldsNameWord = "fields;";
        private const string separate = ";";
        private const string classNameWord = "class";

        public string GetString<TClass>(TClass @class) where TClass : class
        {
            var type = @class.GetType();

            var result = $"{classNameWord} {type.Name}{separate}";
            var properties = type.GetProperties();
            var fields = type.GetFields();

            result += propertiesNameWord;
            foreach (var prop in properties)
            {
                result += prop.Name + separate;
                result += type.GetProperty(prop.Name).GetValue(@class, null) + separate;
            }

            result += fieldsNameWord;
            foreach (var field in fields)
            {
                result += field.Name + separate;
                result += type.GetField(field.Name).GetValue(@class) + separate;
            }

            return result;

        }
    }
}
