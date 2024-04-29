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

// 事件工具
public class JKEventListener : MonoBehaviour, IMouseEvent
{
    #region 内部类和接口
    /// 一次事件
    [Pool]
    private class JKEventListenerEventInfo<T>
    {
        public Action<T, object[]> action;
        public object[] args;

        public void Init(Action<T, object[]> action , object[] args)
        {
            this.action = action;
            this.args = args;
        }

        public void TriggerEvent(T eventData)
        {
            action?.Invoke(eventData, args);
        }
    }


    // 一类事件
    private class JKEventListenerEventInfos<T>
    {
        private List<JKEventListenerEventInfo<T>> eventList = new List<JKEventListenerEventInfo<T>>();

        public void AddListener(Action<T, object[]> action, params object[] args)
        {
            JKEventListenerEventInfo<T> info = ResManager.Instance.Load<JKEventListenerEventInfo<T>>();
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
                            eventList[i].action.JKObjectPushPool();
                            eventList.RemoveAt(i);
                            return;
                        }
                    }
                    else
                    {
                        eventList[i].action.JKObjectPushPool();
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
                eventList[i].JKObjectPushPool();
            }
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

    #region 鼠标事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    #endregion
    #region 碰撞事件

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    #endregion
    #region 触发事件
    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    #endregion

}
