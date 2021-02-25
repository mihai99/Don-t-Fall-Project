using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapSkinType
{
    Basic, LightWood, DarkWood, Metal, Hippie, Hearts
}
public class MapSkinClick : MonoBehaviour
{
    public MapSkinType targetModel;
    public void Click()
    {
        GameObject.FindGameObjectWithTag("canvas")
            .GetComponent<MapSkinManager>()
            .SwapMapSkin(targetModel);
    }
}
