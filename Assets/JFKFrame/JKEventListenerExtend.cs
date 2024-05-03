using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;


public static class JKEventListenerExtend
{
    #region 工具函数
    private static JKEventListener GetOrAddJKEventListener(Component com)
    {
        JKEventListener list = com.GetComponent<JKEventListener>();
        if (list == null)
        {
            return com.gameObject.AddComponent<JKEventListener>();
        }
        return list;
    }


    public static void AddEventListener<T>(this Component com, JKEventType eventType, Action<T, object[]> action, params object[] args)
    {
        JKEventListener list = GetOrAddJKEventListener(com);
        list.AddListener(eventType, action, args);
    }

    public static void RemoveEventListener<T>(this Component com, JKEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
    {
        JKEventListener list = GetOrAddJKEventListener(com);
        list.RemoveListener(eventType, action, checkArgs, args);
    }

    public static void RemoveAllListener(this Component com, JKEventType eventType)
    {
        JKEventListener lis = GetOrAddJKEventListener(com);
        lis.RemoveAllListener(eventType);
    }

    public static void RemoveAllListener(this Component com)
    {
        JKEventListener lis = GetOrAddJKEventListener(com);
        lis.RemoveAllListener();
    }

    #endregion

    #region 鼠标

    #endregion


    #region 碰撞
    public static void OnCollisionEnter(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionEnter, action,  args);
    }

    public static void OnCollisionStay(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionStay, action,  args);
    }

    public static void OnCollisionExit(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionExit, action,  args);
    }

    public static void OnCollisionEnter2D(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionEnter2D, action,  args);
    }

    public static void OnCollisionStay2D(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionStay2D, action,  args);
    }

    public static void OnCollisionExit2D(this Component com, Action<Collision, object[]> action,  params object[] args)
    {
        com.AddEventListener(JKEventType.OnCollisionExit2D, action,  args);
    }

    public static void RemoveCollisionEnter(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionEnter, action, checkArgs, args);
    }

    public static void RemoveCollisionStay(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionStay, action, checkArgs, args);
    }

    public static void RemoveCollisionExit(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionExit, action, checkArgs, args);
    }

    public static void RemoveCollisionEnter2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionEnter2D, action, checkArgs, args);
    }

    public static void RemoveCollisionStay2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionStay2D, action, checkArgs, args);
    }

    public static void RemoveCollisionExit2D(this Component com, Action<Collision, object[]> action, bool checkArgs = false, params object[] args)
    {
        com.RemoveEventListener(JKEventType.OnCollisionExit2D, action, checkArgs, args);
    }



    #endregion
}
