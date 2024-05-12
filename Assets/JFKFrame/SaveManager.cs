using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using System;


public class SaveItem
{
    public int SaveId
    {
        get;
        private set;
    }

    public DateTime LastUpdateTime
    {
        get;
        private set;
    }

    public SaveItem(int saveId, DateTime lastUpdateTime)
    {
        SaveId = saveId;
        LastUpdateTime = lastUpdateTime;
    }

    public void UpdateTime(DateTime lastUpdateTime)
    {
        LastUpdateTime = lastUpdateTime;
    }
}


public static class SaveManager
{

    private class SaveManagerData
    {
        public int currId = 0;
        public List<SaveItem> saveItemList = new List<SaveItem>();
    }

    private static SaveManagerData saveManagerData;

    private const string saveDirName = "saveData";
    private const string settingDirName = "settingData";
    private static readonly string saveDirPath;
    private static readonly string settingDirPath;

    // 存档对象的缓存字典
    // <存档ID, <文件名称，实际对象>>
    private static Dictionary<int, Dictionary<string, object>> cacheDic =
        new Dictionary<int, Dictionary<string, object>>();

    static SaveManager()
    {
        saveDirPath = Application.persistentDataPath + "/" + saveDirName;
        settingDirPath = Application.persistentDataPath + "/" + settingDirName;

        if (Directory.Exists(saveDirPath) == false)
        {
            Directory.CreateDirectory(saveDirPath);
        }

        if (Directory.Exists(settingDirPath) == false)
        {
            Directory.CreateDirectory(settingDirPath);
        }

        saveManagerData = new SaveManagerData();
    }

    private static BinaryFormatter binaryFormatter = new BinaryFormatter();

    private static void SaveFile(object saveObject, string path)
    {
        FileStream f = new FileStream(path, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(f, saveObject);
        f.Dispose();
    }


    private static T LoadFile<T>(string path) where T : class
    {
        if (!File.Exists(path))
        {
            return null;
        }

        FileStream f = new FileStream(path, FileMode.Open);
        T obj = (T)binaryFormatter.Deserialize(f);
        f.Dispose();
        return obj;
    }


    // 获取某个存档的路径.
    private static string GetSavePath(int saveId, bool createDir = true)
    {
        // TODO
        string saveIndexDir = saveDirPath + "/" + saveId;
        if (Directory.Exists(saveIndexDir) == false)
        {
            if (createDir)
            {
                Directory.CreateDirectory(saveIndexDir);
                return saveIndexDir;
            }
            else
            {
                return null;
            }

        }

        return saveIndexDir;
    }

    public static void SaveObject(object saveObject, string saveFileName, int saveId = 0)
    {
        string dirPath = GetSavePath(saveId, true);
        string savePath = dirPath + "/" + saveFileName;
        SaveFile(saveObject, savePath);
        GetSaveItem(saveId).UpdateTime(DateTime.Now);
        SetCache(saveId, saveFileName, saveObject);
    }

    public static void SaveObject(object saveObject, int saveId = 0)
    {
        SaveObject(saveObject, saveObject.GetType().Name, saveId);
    }

    public static void SaveObject(object saveObject, SaveItem saveItem)
    {
        SaveObject(saveObject, saveObject.GetType().Name, saveItem.SaveId);
    }

    public static T LoadObject<T>(string saveFileName, int saveId = 0) where T : class
    {
        T obj = GetCache<T>(saveId, saveFileName);
        if (obj != null)
        {
            return obj;
        }

        string dirPath = GetSavePath(saveId, false);
        if (dirPath == null)
        {
            return null;
        }

        string savePath = dirPath + "/" + saveFileName;
        return LoadFile<T>(savePath);
    }

    public static T LoadObject<T>(int saveId = 0) where T : class {
        return LoadObject<T>(typeof(T).Name, saveId);
    }

    public static T LoadObject<T>(string saveFileName, SaveItem saveItem) where T : class {
        return LoadObject<T>(typeof(T).Name, saveItem.SaveId);
    }

    public static T LoadObject<T>(SaveItem saveItem) where T : class {
        return LoadObject<T>(typeof(T).Name, saveItem.SaveId);
    }

    #region 缓存
    private static void SetCache(int saveId, string fileName, object saveObject)
    {
        if (cacheDic.ContainsKey(saveId))
        {
            if (cacheDic[saveId].ContainsKey(fileName))
            {
                cacheDic[saveId][fileName] = saveObject;
            }
            else
            {
                cacheDic[saveId].Add(fileName, saveObject);
            }
        }
        else
        {
            cacheDic.Add(saveId, new Dictionary<string, object>() { { fileName, saveObject } });
        }
    }

    private static T GetCache<T>(int saveId, string fileName) where T : class
    {
        if (cacheDic.ContainsKey(saveId))
        {
            if (cacheDic[saveId].ContainsKey(fileName))
            {
                return cacheDic[saveId][fileName] as T;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 关于存档

    public static SaveItem GetSaveItem(int id)
    {
        for (int i = 0; i < saveManagerData.saveItemList.Count; i++)
        {
            if (saveManagerData.saveItemList[i].SaveId == id)
            {
                return saveManagerData.saveItemList[i];
            }
        }
        return null;
    }

    public static SaveItem CreateSaveItem()
    {
        SaveItem saveItem = new SaveItem(saveManagerData.currId, DateTime.Now);
        saveManagerData.saveItemList.Add(saveItem);
        saveManagerData.currId += 1;
        // todo update save to disk
        return saveItem;
    }

    public static void DeleteSaveItem(int saveId)
    {
        string itemDir = GetSavePath(saveId, false);
        if (itemDir != null)
        {
            Directory.Delete(itemDir, true);
        }

        saveManagerData.saveItemList.Remove(GetSaveItem(saveId));
    }


    public static void DeleteSaveItem(SaveItem saveItem)
    {
        DeleteSaveItem(saveItem.SaveId);
    }


    #endregion

}
