using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace OtusHomeWork4ReginaM
{
    class Program
    {
        static void Main(string[] args)
        {
            var serialize = new Serialization();

            string customSerialization = "";
            string json = "";

           var testClass = new F() {
                i1 = 1,
                i2 = 2,
                i3 = 3,
                i4 = 4,
                i5 = 5            
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                customSerialization = serialize.GetString<F>(testClass);
            }

            stopwatch.Stop();   
            Console.WriteLine("собственная сериализация заняла: " + stopwatch.ElapsedMilliseconds + " миллисекунд");
            stopwatch.Reset();

            for (int i = 0; i < 1000; i++)
            {
                json = JsonConvert.SerializeObject(testClass);
            }

            Console.WriteLine("стандартная сериализация заняла: " + stopwatch.ElapsedMilliseconds + " миллисекунд");

            var deserialize = new Deserialize();

            stopwatch.Start();

            var newClass = deserialize.GetClass(customSerialization);

            stopwatch.Stop();            
            Console.WriteLine("собственная десериализация заняла: " + stopwatch.ElapsedMilliseconds + " миллисекунд");
            stopwatch.Reset();

            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                JsonConvert.DeserializeObject(json);
            }

            stopwatch.Stop();       
            Console.WriteLine("стандартная десериализация заняла: " + stopwatch.ElapsedMilliseconds + " миллисекунд");
            stopwatch.Reset();
            Console.ReadKey();

        }
    }
}
