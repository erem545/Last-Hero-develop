using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using LastHero;

public class UpdateGUIFromPlayer : MonoBehaviour
{
    GUIStyle mystyle;
    GUIStyle btnStyle;
    GUIStyle boxStyle;
    GUIStyle statuspart;

    public Texture2D texture_gui;
    public Texture2D img_mainmenu;
    public PlayerScript PlayerImport;
    public MouseInfoPointer MouseInfo;
    Character player;
   // public WorldTime time;
    public Texture health_img;
    public Texture endurance_img;

    internal int window;
    private bool dev;
    private int devMode;
    internal bool detalsInfo;
    internal PlayerScript selectedPlayer;

    void Start()
    {
        mystyle = new GUIStyle();
        btnStyle = new GUIStyle();
        boxStyle = new GUIStyle();
        player = PlayerImport.player;
        window = 1;
        detalsInfo = false;
        dev = false;
        devMode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (player == null)
            return;
        mystyle = GUI.skin.GetStyle("label"); // Стиль для основного профиля
        mystyle.fontStyle = FontStyle.Bold;
        mystyle.normal.textColor = Color.white;
        mystyle.alignment = TextAnchor.UpperLeft;
        mystyle.fontSize = 17;

        btnStyle = GUI.skin.GetStyle("button");
        btnStyle.fontStyle = FontStyle.Bold;
        btnStyle.normal.textColor = Color.white;
        btnStyle.alignment = TextAnchor.MiddleCenter;
        btnStyle.fontSize = 20;

        boxStyle = GUI.skin.GetStyle("box");
        boxStyle.fontStyle = FontStyle.Bold;
        boxStyle.normal.textColor = Color.white;
        boxStyle.alignment = TextAnchor.UpperCenter;
        boxStyle.fontSize = 22;

        statuspart = GUI.skin.GetStyle("label");
        statuspart.fontStyle = FontStyle.Bold;
        statuspart.normal.textColor = Color.white;
        statuspart.alignment = TextAnchor.UpperCenter;
        statuspart.fontSize = 20;

        GUIContent healthContent1 = 
            new GUIContent(Math.Round(player.Health, 1) + "\t/ " + Math.Round(player.MaxHealth, 1), health_img);
        GUIContent enduranceContent1 = 
            new GUIContent(Math.Round(player.Endurance, 1) + "\t/ " + Math.Round(player.MaxEndurance, 1), endurance_img);



        GUIContent infoContent = new GUIContent(
            "Сила:       "+ player.Strength + "(+ "+ player.bodyNode.SumStrength +")\n" +
            "Ловкость:  "+ player.Agility + " (+ " + player.bodyNode.SumAgility +")\n" +
            "Интеллект: "+ player.Intelligance+" (+ "+ player.bodyNode.SumIntelligance + ")\n" +
            "Оружие: " + player.weaponNode.ToString() + "\n\n" +
            "Точность:  " + player.Accuaracy + "\n" +
            "Защита:    "+ player.ArmorValue);

        GUIContent allInfoContent = new GUIContent();
        if (selectedPlayer != null)
        {
            allInfoContent = new GUIContent(
                    "Здоровье: " + selectedPlayer.player.Health + " / " + selectedPlayer.player.MaxHealth+ "\n" +
                    "Выносливость: " + selectedPlayer.player.Endurance + " / " + selectedPlayer.player.MaxEndurance+ "\n" +
                    "Сила: " + selectedPlayer.player.Strength+ "\n" +
                    "Ловкость: " + selectedPlayer.player.Agility+ "\n" +
                    "Интеллект: " + selectedPlayer.player.Intelligance+ "\n" +
                    "Точность: " + selectedPlayer.player.Accuaracy+ "\n" +
                    "Защита: " + selectedPlayer.player.ArmorValue+ "\n" +
                    "Лидерство: " + selectedPlayer.player.Leadership+ "\n");
        }


        GUIContent infoAboutWeapons = new GUIContent(
            player.weaponNode.ToString()
            );

        GUIContent infoAboutArmor = new GUIContent(
            player.bodyNode.ToString()
            );
        GUIContent expContent = new GUIContent(
            "Уровень: " + player.Level + "\n" +
            "Опыт: " + player.XP);



        switch (window) 
        {
            case 1:
                {
                    const float height_percent_box1 = 0.25f; // Процент нижней панели от максимальной высоты
                    const int factor_1 = 10; // Чем больше фактор, тем больше отступ текстуры от границ нижней панели 

                    // Верхняя панель
                    GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height  * 0.05f));
                    GUI.Box(new Rect(1, 1, Screen.width - 1, Screen.height * 0.05f - 1), "", boxStyle);
                    if (GUI.Button(new Rect(0, 0, 160, 50), "Меню", btnStyle))
                    {
                        Time.timeScale = 0;
                        player.paused = true;
                        window = 2;
                    }
                    if (GUI.Button(new Rect(Screen.width * 0.5f, 0, 250, 50), "Develop", btnStyle))
                        dev = true;
                    GUI.EndGroup();

                    // Состояние игрока в GUI
                    GUI.BeginGroup(new Rect(0, Screen.height - Screen.height * height_percent_box1, Screen.width * 0.25f, Screen.height * height_percent_box1));
                    GUI.Box(new Rect(0, 0, Screen.width * 0.25f - 2, Screen.height * height_percent_box1), "Состояние", boxStyle);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.04f, Screen.width * 0.24f, 25), player.bodyNode.head.ToString(), statuspart);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.07f, Screen.width * 0.24f, 25), player.bodyNode.body.ToString(), statuspart);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.10f, Screen.width * 0.24f, 25), player.bodyNode.lhand.ToString(), statuspart);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.13f, Screen.width * 0.24f, 25), player.bodyNode.rhand.ToString(), statuspart);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.16f, Screen.width * 0.24f, 25), player.bodyNode.lfoot.ToString(), statuspart);
                    GUI.Label(new Rect(factor_1 + 2, Screen.height * 0.19f, Screen.width * 0.24f, 25), player.bodyNode.rfoot.ToString(), statuspart);
                    GUI.EndGroup();

                     // Профиль игрока в GUI
                    GUI.BeginGroup(new Rect(Screen.width * 0.25f, Screen.height - Screen.height * height_percent_box1, Screen.width * 0.25f, Screen.height * height_percent_box1));
                    GUI.Box(new Rect (0, 0, Screen.width * 0.25f - 2, Screen.height * height_percent_box1), player.MainName, boxStyle);
                    GUI.DrawTexture(new Rect(factor_1, Screen.height * factor_1 / 1000 + 25, Screen.width * 0.0937f - factor_1, Screen.height * 0.20f), Texture2D.whiteTexture);
                    GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / Screen.height * 40, Screen.width * 0.2f, Screen.height * 0.06f), healthContent1, mystyle);
                    GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / Screen.height * 80, Screen.width * 0.2f, Screen.height * 0.06f), enduranceContent1, mystyle);
                    GUI.Label(new Rect(Screen.width * 0.1f, Screen.height / Screen.height * 120, Screen.width * 0.2f, Screen.height * 0.1f), expContent, mystyle);
                    GUI.EndGroup();

                    // Характеристики игрока в GUI
                    GUI.BeginGroup(new Rect(Screen.width * 0.5f, Screen.height - Screen.height * height_percent_box1, Screen.width * 0.25f, Screen.height * height_percent_box1));
                    GUI.Box(new Rect(0, 0, Screen.width * 0.25f - 2, Screen.height * height_percent_box1), "Характеристики", boxStyle);
                    GUI.Label(new Rect(factor_1 * 2, factor_1 * 3, Screen.width * 0.24f, Screen.height - Screen.height * height_percent_box1), infoContent, mystyle);
                    GUI.EndGroup();

                    // Характеристики игрока в GUI
                    GUI.BeginGroup(new Rect(Screen.width * 0.75f, Screen.height - Screen.height * height_percent_box1, Screen.width * 0.25f, Screen.height * height_percent_box1));
                    GUI.Box(new Rect(0, 0, Screen.width * 0.25f - 2, Screen.height * height_percent_box1), "Характеристики", boxStyle);
                    GUI.Label(new Rect(factor_1 * 2, factor_1 * 3, Screen.width * 0.24f, Screen.height - Screen.height * height_percent_box1), MouseInfo.character.ToString(), mystyle);
                    GUI.EndGroup();

                    break;
                }
            case 2: // Меню-Пауза
                {
                    GUI.BeginGroup(new Rect(Screen.width * 0.4f, Screen.height * 0.4f, Screen.width * 0.25f, Screen.height * 0.25f));
                    GUI.Box(new Rect(0, 0, Screen.width * 0.25f - 2, Screen.height * 0.25f - 2), "Меню", boxStyle);
                    if (GUI.Button(new Rect((Screen.width / 2) * 0.15f, (Screen.height / 2) * 0.25f, 160, 50), "Продолжить", btnStyle))
                    {
                        Time.timeScale = 1;
                        player.paused = false;
                        window = 1;
                    }
                    GUIContent imgMainMenu = new GUIContent(img_mainmenu);
                    GUI.Box(new Rect(20, 30, Screen.width * 0.25f - 2, Screen.height * 0.25f - 2), imgMainMenu, mystyle);
                    GUI.EndGroup();
                    break;

                }
            //case 3:
            //    {
            //        GUI.BeginGroup(new Rect(10, 10, 200, 250));
            //        GUI.Box(new Rect(5, 5, 210, 160), "Информация"); mystyle.alignment = TextAnchor.UpperCenter;
            //        GUI.Label(new Rect(20, 30, 150, 20), $"{player.MainName}", mystyle); mystyle.alignment = TextAnchor.UpperLeft;
            //        GUI.Label(new Rect(30, 50, 80, 20), healthContent1, mystyle); GUI.Label(new Rect(120, 50, 70, 20), healthContent2, mystyle);
            //        GUI.Label(new Rect(30, 80, 80, 20), enduranceContent1, mystyle); GUI.Label(new Rect(120, 80, 70, 20), enduranceContent2, mystyle);
            //        if (GUI.Button(new Rect(50, 110, 80, 20), "Скрыть"))
            //            window = 1;
            //        GUI.EndGroup();

            //        GUI.BeginGroup(new Rect(Screen.width * 0.8f, 50, Screen.width * 0.2f, Screen.height * 0.35f));
            //        GUI.Box(new Rect(0, 0, 200, 300), "Подробная информация");
            //        GUI.Label(new Rect(25, 30, 270, 250), bodyPart, inventoryStyle);
            //        GUI.EndGroup();

            //        GUI.BeginGroup(new Rect(Screen.width * 0.3f, 50, Screen.width * 0.6f, Screen.height * 0.7f));
            //        GUI.Box(new Rect(0, 0, 500, 250), "Аммуниция");
            //        GUI.Label(new Rect(25, 30, 450, 250), infoAboutArmor, inventoryStyle);
            //        GUI.Box(new Rect(0, 250, 500, 150), "Оружие");
            //        GUI.Label(new Rect(25, 290, 500, 150), infoAboutWeapons, inventoryStyle);
            //        GUI.EndGroup();
            //        break;
            //    }

        }

        if ((detalsInfo) && (window < 2))
        {
            GUI.BeginGroup(new Rect(Screen.width - Screen.width * 0.3f, Screen.height * 0.4f, 500, 550));
            //GUI.Box(new Rect(0, 0, Screen.width * 0.29f, 240), "Информация");
            GUI.Label(new Rect(5, 5, 100, 20), selectedPlayer.player.MainName, mystyle);
            GUI.Label(new Rect(5, 35, 190, 200), allInfoContent, mystyle);
            GUI.EndGroup();
        }
        if (dev)
        {
            GUI.BeginGroup(new Rect(10, Screen.height * 0.1f, Screen.width * 0.2f, Screen.height * 0.2f));
            GUI.Box(new Rect(0, 0, Screen.width * 0.2f, Screen.height * 0.2f), "Режим разработчика", boxStyle);
            if (GUI.Button(new Rect(5, 35, 100, 20), "Хил +10"))
                devMode = 1;
            if (GUI.Button(new Rect(5, 60, 100, 20), "Урон -10"))
                devMode = 2;

            if (GUI.Button(new Rect(120, 35, 100, 20), "Опыт +"))
                devMode = 3;
            if (GUI.Button(new Rect(235, 35, 100, 20), "Уров +"))
                devMode = 7;

            if (GUI.Button(new Rect(120, 60, 100, 20), "Сил +"))
                devMode = 4;
            if (GUI.Button(new Rect(235, 60, 100, 20), "Лов +"))
                devMode = 5;
            if (GUI.Button(new Rect(350, 60, 100, 20), "Инт +"))
                devMode = 6;

            GUI.EndGroup();

            switch (devMode)
            {
                case 1:
                    player.ToHeal(10);
                    break;
                case 2:
                    player.ToDamage(10);
                    break;
                case 3:
                    player.GetReward(100);
                    break;

                case 4:
                    player.StrengthStats++;
                    player.UpdateAll();
                    break;
                case 5:
                    player.AgilityStats++;
                    player.UpdateAll();
                    break;
                case 6:
                    player.IntelliganceStats++;
                    player.UpdateAll();
                    break;
                case 7:
                    player.LevelUP();
                    break;
            }
            devMode = 0;
        }

    }
}
