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

    [LabelText("���������")]
    [DictionaryDrawerSettings(KeyLabel = "����", ValueLabel = "����")]
    public Dictionary<Type, bool> wantCacheDic = new Dictionary<Type, bool>();


#if UNITY_EDITOR

    [Button(Name = "��ʼ����Ϸ����", ButtonHeight =50)]
    [GUIColor(0,1,0)]
    private void Init()
    {
        Debug.Log("GameSetting ��ʼ��");
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

    // ������poll���Ե����ͼ�������.
    private void PoolAttributeOnEditor()
    {
        wantCacheDic.Clear();
        // ��ȡ���г���
        System.Reflection.Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();

        // ���������µ�ÿһ������.
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
