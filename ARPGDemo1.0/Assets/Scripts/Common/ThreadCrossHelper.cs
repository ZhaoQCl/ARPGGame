using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// 线程交叉访问助手类
    /// </summary>
    public class ThreadCrossHelper : MonoSingleton<ThreadCrossHelper>
    {
        class DelayedItem
        {
            public Action CurrentAction { get; set; }
            public DateTime Time { get; set; }
        }

        public override void Init()
        {
            base.Init();
            actionList = new List<DelayedItem>();
        }

        private List<DelayedItem> actionList;

        private void Update()
        {
            for(int i = actionList.Count - 1; i >= 0; i--)
            {
                //如果发现到达执行时间，则进行线程
                if (actionList[i].Time <= DateTime.Now)
                {
                    actionList[i].CurrentAction();
                    actionList.RemoveAt(i);
                }
            }
        }

        public void ExecuteOnMainThread(Action action, float delay = 0)
        {
            //同时操作actionList，容易造成冲突 
            lock (actionList)
            {
                var item = new DelayedItem()
                {
                    CurrentAction = action,
                    Time = DateTime.Now.AddSeconds(delay)
                };
                actionList.Add(item);
            }
        }
    }
}
