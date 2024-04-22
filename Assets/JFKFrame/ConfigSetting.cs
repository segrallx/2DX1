using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName ="ConfigSetting" , menuName ="JKFrame/ConfigSetting")]
public class ConfigSetting : ConfigBase
{
    [DictionaryDrawerSettings(KeyLabel = "类型", ValueLabel = "列表")]
    public Dictionary<string, Dictionary<int, ConfigBase>> configDic;

    public T GetConfig<T>(string configTypeName, int id) where T : ConfigBase
    {
        if(!configDic.ContainsKey(configTypeName))
        {
            throw new System.Exception("JK:config error:"+configTypeName);
        }

        if (configDic[configTypeName].ContainsKey(id))
        {
            throw new System.Exception($"JK:config error {configTypeName} key {id}");
        }

        return configDic[configTypeName][id] as T;
    }
}
