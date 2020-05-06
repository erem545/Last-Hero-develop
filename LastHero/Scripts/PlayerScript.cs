using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using LastHero;

public class PlayerScript : MonoBehaviour
{
    GameObject gameObjectPlayer;
	public Character player;
    public string Name;
    public UpdateGUIFromPlayer gui;
    private void Start()
    {
        SetPlayer(ObjectStorage.DeserializationCharacter(Name));
        gameObjectPlayer = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.ok == false)
            Destroy(gameObjectPlayer);
    }

    public void SetPlayer(Character _player)
    {
        player = _player;
        player.UpdateAll();
    }
}
