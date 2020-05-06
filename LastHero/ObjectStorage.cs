using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEditor;
namespace LastHero
{
    public class ObjectStorage
    {
        public static void SerializationCharacter(Character person)
        {
            string path = @".\ProjectData\person\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream($@"{path}{person.MainName}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, person);
                    Console.WriteLine($"Успешная сериализация: {path}{person.MainName}.xml");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка сериализации: " + e);
                }
            }
        }
        public static Character DeserializationCharacter(string name)
        {
            string path = @".\ProjectData\person\";
            XmlSerializer formatter = new XmlSerializer(typeof(Character));
            using (FileStream fs = new FileStream($@"{path}{name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Character newPerson = (Character)formatter.Deserialize(fs);
                    Console.WriteLine($"Успешная десериализация: {path}{newPerson.MainName}.xml");
                    newPerson.UpdateAll();
                    return newPerson;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка десериализации: " + e);
                    return null;
                }
            }
        }

        public static void SerizlizationItem(Item item)
        {
            string path = @".\ProjectData\items\xml\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            item.d_path = $@"{path}\{item.Name}.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(Item));
                using (FileStream fs = new FileStream($@"{path}{item.Name}.xml", FileMode.Create))
                {
                    try
                    {
                        formatter.Serialize(fs, item);
                    Console.WriteLine($"Успешная сериализация: {path}{item.Name}.xml");
                }
                    catch (Exception e)
                    {
                        Console.WriteLine("Ошибка сериализации: " + e);
                    }
                }
        }
        public static Item DeserializationItem(string name)
        {
            string path = @".\ProjectData\items\xml\";
            XmlSerializer formatter = new XmlSerializer(typeof(Item));
            using (FileStream fs = new FileStream($@"{path}{name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Item newitem = (Item)formatter.Deserialize(fs);
                    Console.WriteLine($"Успешная десериализация: {path}{newitem.Name}.xml");
                    return newitem;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка десериализации: " + e);
                    return null;
                }
            }
        }

        public static void SerizlizationArmor(Armor item)
        {
            string path = @".\ProjectData\armor\xml\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            item.d_path = $@"{path}\{item.Name}.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(Armor));
            using (FileStream fs = new FileStream($@"{path}{item.Name}.xml", FileMode.Create))
            {
                try
                {
                    formatter.Serialize(fs, item);
                    Console.WriteLine($"Успешная сериализация: {path}{item.Name}.xml");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка сериализации: " + e);
                }
            }
        }
        public static Armor DeserializationArmor(string name)
        {
            string path = @".\ProjectData\armor\xml\";
            XmlSerializer formatter = new XmlSerializer(typeof(Armor));
            using (FileStream fs = new FileStream($@"{path}{name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Armor newitem = (Armor)formatter.Deserialize(fs);
                    Console.WriteLine($"Успешная десериализация: {path}{newitem.Name}.xml");
                    return newitem;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка десериализации: " + e);
                    return null;
                }
            }

        }

        public static void SerizlizationWeapon(Weapon item)
        {
            string path = @".\ProjectData\weapon\xml\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            item.d_path = $@"{path}{item.Name}.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(Weapon));
            using (FileStream fs = new FileStream($@"{path}{item.Name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    formatter.Serialize(fs, item);
                    Console.WriteLine($"Успешная сериализация: {path}{item.Name}.xml");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка сериализации: " + e);
                }
            }
        }
        public static Weapon DeserializationWeapon(string name)
        {
            string path = @".\ProjectData\weapon\xml\";
            XmlSerializer formatter = new XmlSerializer(typeof(Weapon));
            using (FileStream fs = new FileStream($@"{path}{name}.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Weapon newitem = (Weapon)formatter.Deserialize(fs);
                    Console.WriteLine($"Успешная десериализация: {path}{newitem.Name}.xml");
                    return newitem;
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
