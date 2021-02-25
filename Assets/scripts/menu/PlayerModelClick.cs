using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerModel
{
    Basic, Knight, Skeleton, Troll, Golemn
}

public class PlayerModelClick : MonoBehaviour
{
    public PlayerModel targetModel;
    public void Click()
    {
        GameObject.FindGameObjectWithTag("canvas")
            .GetComponent<CharacterSkinsManager>()
            .SwapPlayerModel(targetModel);
    }
}
