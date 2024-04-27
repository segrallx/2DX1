using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;


[CreateAssetMenu(fileName ="GameSetting", menuName ="JKFrame/Config/GameSetting")]
public class GameSetting : ConfigBase
{

    [LabelText("对象池设置")]
    [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "缓存")]
    public Dictionary<Type, bool> wantCacheDic = new Dictionary<Type, bool>();


#if UNITY_EDITOR

    [Button(Name = "初始化游戏配置", ButtonHeight =50)]
    [GUIColor(0,1,0)]
    private void Init()
    {
        Debug.Log("GameSetting 初始化");
        PoolAttributeOnEditor();
    }


    [InitializeOnLoadMethod]
    private static void LoadForEditor()
    {
        if(GameObject.Find("GameRoot")!=null)
        {
            GameObject.Find("GameRoot").GetComponent<GameRoot>().GameSetting.Init();
        }
    }

    // 将带有poll特性的类型加入对象池.
    private void PoolAttributeOnEditor()
    {
        wantCacheDic.Clear();
        // 获取所有程序集
        System.Reflection.Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

        // 遍历程序集下的每一个类型.
        foreach (System.Reflection.Assembly assembly in asms)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typ in types)
            {
                PoolAttribute pool = typ.GetCustomAttribute<PoolAttribute>();
                if(pool!=null)
                {
                    wantCacheDic.Add(typ, true);
                }
            }
        }
    }

#endif
}
