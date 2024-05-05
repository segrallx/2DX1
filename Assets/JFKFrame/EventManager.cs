using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{

    private interface IEventInfo
    {
    }


    private class EventInfo: IEventInfo
    {
        public Action action;
        public void Init(Action act)
        {
            this.action += act;
        }
    }

    private class EventInfo<T> : IEventInfo
    {
        public Action<T> action;
        public void Init(Action<T> act)
        {
            this.action += act;
        }
    }

    private class EventInfo<T,K> : IEventInfo
    {
        public Action<T,K> action; 
        public void Init(Action<T,K> act)
        {
            this.action += act;
        }
    }

    private class EventInfo<T, K, L> : IEventInfo
    {
        public Action<T, K, L> action;
        public void Init(Action<T, K, L> act)
        {
            this.action += act;
        }
    }

    private static Dictionary<string, IEventInfo> eventInfoDic = new Dictionary<string, IEventInfo>();

    #region 添加事件的监听
    public static void AddEventListener(string eventName, Action act)
    {
        if(eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo).action += act;
        }
        else
        {
            EventInfo eventInfo = PoolManager.Instance.GetObject<EventInfo>();
            eventInfo.Init(act);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    public static void AddEventListener<T>(string eventName, Action<T> act)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T>).action += act;
        }
        else
        {
            EventInfo<T> eventInfo = PoolManager.Instance.GetObject<EventInfo<T>>();
            eventInfo.Init(act);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    public static void AddEventListener<T, K>(string eventName, Action<T,K> act)
    {
        if (eventInfoDic.ContainsKey(eventName))
        {
            (eventInfoDic[eventName] as EventInfo<T,K>).action += act;
        }
        else
        {
            EventInfo<T,K> eventInfo = PoolManager.Instance.GetObject<EventInfo<T,K>>();
            eventInfo.Init(act);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }

    public static void AddEventListener<T, K, L>(string eventName, Action<T, K, L> act)
    {
        if (eventInfoDic.ContainsKey(eventName)) 
        {
            (eventInfoDic[eventName] as EventInfo<T, K, L>).action += act;
        }
        else
        {
            EventInfo<T, K, L> eventInfo = PoolManager.Instance.GetObject<EventInfo<T, K, L>>();
            eventInfo.Init(act);
            eventInfoDic.Add(eventName, eventInfo);
        }
    }
    #endregion

    
    
    #region 触发
    public static void EventTrigger(string eventName)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo).action?.Invoke();
    }

    public static void EventTrigger<T>(string eventName, T arg1)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T>).action?.Invoke(arg1);
    }

    public static void EventTrigger<T, K>(string eventName, T arg1, K arg2)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T,K>).action?.Invoke(arg1,arg2);
    }

    public static void EventTrigger<T, K, L>(string eventName, T arg1, K arg2, L arg3)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T, K, L>).action?.Invoke(arg1, arg2, arg3);
    }

    #endregion

    #region 删除事件

    public static void RemoveEventListener(string eventName, Action act)
    {
        if(!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo).action -= act;
    }

    public static void RemoveEventListener<T>(string eventName, Action<T> act)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T>).action -= act;
    }

    public static void RemoveEventListener<T, K>(string eventName, Action<T,K> act)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T, K>).action -= act;
    }

    public static void RemoveEventListener<T, K, L>(string eventName, Action<T, K, L> act)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        (eventInfoDic[eventName] as EventInfo<T, K, L>).action -= act;
    }

    // remove
    public static void RemoveEventListener(string eventName)
    {
        if (!eventInfoDic.ContainsKey(eventName))
        {
            return;
        }

        eventInfoDic[eventName].JKObjectPushPool();
        eventInfoDic.Remove(eventName);
    }

    public static void Clear()
    {
        foreach(string eventName in eventInfoDic.Keys)
        {
            RemoveEventListener(eventName);
        }
    }

    #endregion

}
