using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace LastHero
{
    public class ObjectStorage
    {
        public static void SerializationCharacter(Character person)
        {
            if (!Directory.Exists(@".\ProjectData\XML\"))
                Directory.CreateDirectory(@".\ProjectData\XML\");
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream($@".\ProjectData\XML\{person.MainName}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, person);
                    Console.WriteLine("Успешная сериализация!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка сериализации: " + e);
                }
            }
        }

        public static Character DeserializationCharacter(string name)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream($@".\ProjectData\XML\{name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Character newPerson = (Character)formatter.Deserialize(fs);
                    newPerson.UpdateAllValues();
                    Console.WriteLine("Объект десериализован");
                    return newPerson;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка десериализации: " + e);
                    return null;
                }
            }
        }
    }
}
