using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OtusHomeWork4ReginaM
{
    class Deserialize
    {
        private const int classNameWordLenght = 6;
        private const int propertiesNameWordLenght = 11;
        private const string propertiesNameWord = "properties";
        private const string fieldsNameWord = "fields";
        private const string separate = ";";
        public object GetClass(string csvString)
        {
            var propertiesNameArray = new List<string>();
            var propertiesValuesArray = new List<string>();
            var classNameIndex = csvString.IndexOf(separate);
            var classType = csvString.Substring(classNameWordLenght, classNameIndex - classNameWordLenght);

            var propertiesIndex = csvString.LastIndexOf(propertiesNameWord);

            var fieldsIndex = csvString.LastIndexOf(fieldsNameWord);

            if (propertiesIndex + propertiesNameWordLenght != fieldsIndex)
            {
                var properties = csvString.Substring(propertiesIndex + propertiesNameWordLenght, fieldsIndex - propertiesIndex - propertiesNameWordLenght);
                int startIndex = 0;

                for (int i = 0; i + startIndex < properties.Length; i++)
                {
                    var propStr = properties.Substring(i + startIndex);

                    var firstPropIndex = propStr.IndexOf(separate);
                    var firstPropName = propStr.Substring(0, firstPropIndex);
                    startIndex += firstPropIndex;

                    var firstPropValIndex = propStr.Substring(firstPropIndex + 1).IndexOf(separate);
                    var firstPropVal = propStr.Substring(firstPropIndex + 1, firstPropValIndex);

                    startIndex += firstPropValIndex;
                    propertiesNameArray.Add(firstPropName);
                    propertiesValuesArray.Add(firstPropVal);

                    startIndex += 1;
                }
            }

            var newClass = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Name == classType).FirstOrDefault();

            if (newClass == null) 
            {
                throw new Exception("Сериализуемый класс не найден в текущей сборке.");
            }

           var inst = Activator.CreateInstance(newClass);

            for (int i = 0; i <= propertiesNameArray.Count - 1; i++)
            {
                newClass.GetProperty(propertiesNameArray[i]).SetValue(inst, int.Parse(propertiesValuesArray[i]));
            }


            return inst;

        }
    }
}
