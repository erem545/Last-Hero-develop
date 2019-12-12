﻿using System;
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

        public static void DeserializationCharacter(Character person)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream($"{person.MainName}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Character newPerson = (Character)formatter.Deserialize(fs);
                    newPerson.UpdateAllValues();
                    Console.WriteLine("Объект десериализован");
                    Console.WriteLine(newPerson.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка десериализации: " + e);
                }
            }
        }
    }
}
