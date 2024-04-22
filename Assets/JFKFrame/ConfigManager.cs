using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : ManageBase<ConfigManager>
{
    [SerializeField]
    private ConfigSetting configSetting;

    public T GetConfig<T>(string configTypeName, int id) where T : ConfigBase
    {
        return configSetting.GetConfig<T>(configTypeName, id);
    }


}
