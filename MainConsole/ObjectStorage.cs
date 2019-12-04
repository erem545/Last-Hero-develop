using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MainConsole
{
    class ObjectStorage
    {
        public static void Seriaz(Item item)
        {
            // создаем объект BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream($"{item.Name}.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, item);
                Console.WriteLine("Объект сериализован");
            }

            // десериализация из файла people.dat
            using (FileStream fs = new FileStream($"{item.Name}.dat", FileMode.OpenOrCreate))
            {
                Item newItem = (Item)formatter.Deserialize(fs);
                Console.WriteLine("Объект десериализован");
                TextFactory.PrintOneArray(TextFactory.ItemInfo(newItem));
            }

            Console.ReadLine();
        }
    }
}
