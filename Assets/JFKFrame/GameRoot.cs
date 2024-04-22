using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : SingletonMono<GameRoot>
{

    [SerializeField]
    private GameSetting gameSetting;
    public GameSetting GameSetting
    {
        get { return gameSetting; }
    }

    protected override void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        DontDestroyOnLoad(gameObject);
        InitManagers();
    }

    private void InitManagers()
    {
        ManageBase[] managers = GetComponents<ManageBase>();
        Debug.LogFormat("init managers {0}", managers.Length);
        for(int i=0; i< managers.Length; i++)
        {
            managers[i].Init();
        }

        //ManageBase X = GetComponent<PoolManager>() as ManageBase;
        //X.Init();
    }

}
