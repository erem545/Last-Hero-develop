using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Serialization : MonoBehaviour
{

    public LastHero.Character c1;
    // Start is called before the first frame update
    void Start()
    {
        LastHero.ObjectStorage.SerializationCharacter(c1);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
