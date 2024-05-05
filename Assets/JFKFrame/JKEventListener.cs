using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IMouseEvent: IPointerEnterHandler, IPointerExitHandler, 
    IPointerClickHandler, IPointerUpHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    
}

//事件类型
public enum JKEventType
{
    OnMouseEnter,
    OnMouseExit,
    OnClick,
    OnClickDown,
    OnClickUp,
    OnDrag,
    OnBeginDrag,
    OnEndDrag,

    OnCollisionEnter,
    OnCollisionStay,
    OnCollisionExit,
    OnCollisionEnter2D,
    OnCollisionStay2D,
    OnCollisionExit2D,

    OnTriggerEnter,
    OnTriggerStay,
    OnTriggerExit,
    OnTriggerEnter2D,
    OnTriggerStay2D,
    OnTriggerExit2D,
}

// 事件工具
public class JKEventListener : MonoBehaviour, IMouseEvent
{
    #region 内部类和接口
    /// 一次事件
    private class JKEventListenerEventInfo<T>
    {
        public Action<T, object[]> action;
        public object[] args;

        public void Init(Action<T, object[]> action , object[] args)
        {
            this.action = action;
            this.args = args;
        }

        public void Destory()
        {
            this.action = null;
            this.args = null;
            this.JKObjectPushPool();
        }

        public void TriggerEvent(T eventData)
        {
            action?.Invoke(eventData, args);
        }
    }

    interface JKEventListenerEventInfosI {
        void RemoveAll();
    }

    // 一类事件
    private class JKEventListenerEventInfos<T>: JKEventListenerEventInfosI
    {
        private List<JKEventListenerEventInfo<T>> eventList = new List<JKEventListenerEventInfo<T>>();

        public void AddListener(Action<T, object[]> action, params object[] args)
        {
            JKEventListenerEventInfo<T> info = PoolManager.Instance.GetObject<JKEventListenerEventInfo<T>>();
            info.Init(action, args);
            eventList.Add(info);
        }

        public void RemoveListener(Action<T, object[]> action, bool checkArgs = false, params object[] args)
        {
            for(int i=0; i< eventList.Count; i++)
            {
                if (eventList[i].action.Equals(action))
                {
                    if(checkArgs && args.Length>0)
                    {
                        if (args.ArrayEquals(eventList[i].args))
                        {
                            //eventList[i].JKObjectPushPool();
                            eventList[i].Destory();
                            eventList.RemoveAt(i);
                            return;
                        }
                    }
                    else
                    {
                        //eventList[i].JKObjectPushPool();
                        eventList[i].Destory();
                        eventList.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public void RemoveAll()
        {
            for(int i=0;i<eventList.Count;i++)
            {
                //eventList[i].JKObjectPushPool();
                eventList[i].Destory();
            }
            this.JKObjectPushPool();
            eventList.Clear();
        }
        
        public void TriggerEvent(T eventData)
        {
            for(int i=0;i<eventList.Count;i++)
            {
                eventList[i].TriggerEvent(eventData);
            }
        }

    }

    #endregion

    private Dictionary<JKEventType, JKEventListenerEventInfosI> eventInfoDict = 
        new Dictionary<JKEventType, JKEventListener.JKEventListenerEventInfosI>();


    private void TriggerAction<T>(JKEventType eventType, T eventData)
    {
        if (eventInfoDict.ContainsKey(eventType))
        {
            (eventInfoDict[eventType] as JKEventListenerEventInfos<T>).TriggerEvent(eventData);
        }
    }

    #region 外部访问
    public void AddListener<T>(JKEventType eventType, Action<T, object[]> action, params object[] args) 
    { 
        if(eventInfoDict.ContainsKey(eventType))
        {
            (eventInfoDict[eventType] as JKEventListenerEventInfos<T>).AddListener(action, args);
        }
        else
        {
            JKEventListenerEventInfos<T> infos = PoolManager.Instance.GetObject<JKEventListenerEventInfos<T>>();
            infos.AddListener(action, args);
            eventInfoDict.Add(eventType, infos);
        }
    }

    public void RemoveListener<T>(JKEventType eventType, Action<T, object[]> action, bool checkArgs = false, params object[] args)
    {
        if(eventInfoDict.ContainsKey(eventType))
        {
            (eventInfoDict[eventType] as JKEventListenerEventInfos<T>).RemoveListener(action, checkArgs, args);
        }
    }

    public void RemoveAllListener(JKEventType eventType)
    {
        if(eventInfoDict.ContainsKey(eventType))
        {
            //(eventInfoDict[eventType] as JKEventListenerEventInfos<T>).RemoveAll();
            eventInfoDict[eventType].RemoveAll();
            eventInfoDict.Remove(eventType);
        }
    }

    public void RemoveAllListener()
    {
        foreach( JKEventListenerEventInfosI  item in eventInfoDict.Values)
        {
            item.RemoveAll();
        }
        eventInfoDict.Clear();
    }


    #endregion


    #region 鼠标事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnBeginDrag, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnDrag, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnEndDrag, eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnClick, eventData);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnMouseEnter, eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnMouseExit, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TriggerAction(JKEventType.OnClickUp, eventData);
    }

    #endregion
    #region 碰撞事件

    private void OnCollisionEnter(Collision collision)
    {
        TriggerAction(JKEventType.OnCollisionEnter, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        TriggerAction(JKEventType.OnCollisionExit, collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        TriggerAction(JKEventType.OnCollisionStay, collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TriggerAction(JKEventType.OnCollisionEnter2D, collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        TriggerAction(JKEventType.OnCollisionExit2D, collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        TriggerAction(JKEventType.OnCollisionStay2D, collision);
    }

    #endregion
    #region 触发事件
    private void OnTriggerEnter(Collider other)
    {
        TriggerAction(JKEventType.OnTriggerEnter, other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerAction(JKEventType.OnTriggerExit, other);
    }

    private void OnTriggerStay(Collider other)
    {
        TriggerAction(JKEventType.OnTriggerStay, other);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerAction(JKEventType.OnTriggerEnter2D, collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggerAction(JKEventType.OnTriggerExit2D, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerAction(JKEventType.OnTriggerStay2D, collision);
    }

    #endregion

}
