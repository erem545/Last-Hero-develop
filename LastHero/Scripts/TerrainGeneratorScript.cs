using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LastHero_GameEngine;

public class TerrainGeneratorScript : MonoBehaviour
{
    public Texture2D TextureStart;
    TerrainClass.Generator CreatorTerrain;
    void Start()
    {
        TerrainClass.MotherTexture = TextureStart;
        GetComponent<Renderer>().material.mainTexture = CreatorTerrain.TerrainGenerator();
    }
    void Update()
    {
        
    }
}
