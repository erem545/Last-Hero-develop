using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingText : MonoBehaviour
{
    public GameObject player;
    public string TextString;
    TextMesh txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = gameObject.GetComponent<TextMesh>();
        txt.fontSize = 12;
        txt.color = Color.white;
        txt.alignment = TextAlignment.Center;
        txt.text = TextString;
    }

    // Update is called once per frame
    void Update()
    {
        txt.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z-2); 
    }

}
