using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName ="GameSetting", menuName ="JKFrame/Config/GameSetting")]
public class GameSetting : ConfigBase
{
#if UNITY_EDITOR

    [Button(Name = "初始化游戏配置", ButtonHeight =50)]
    [GUIColor(0,1,0)]
    private void Init()
    {
        Debug.Log("GameSetting 初始化");
    }


    [InitializeOnLoadMethod]
    private static void LoadForEditor()
    {
        GameObject.Find("GameRoot").GetComponent<GameRoot>().GameSetting.Init();
    }

#endif
}
