using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/*
 * 
 * 
 */
namespace ns 
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadDemo01 : MonoBehaviour
    {
        private ManualResetEvent signal;
        private Thread thread03;
        private void Start()
        {
            //主线程直接调用
            //Fun1();

            


            //自行创建的先冲，称之为子线程/辅助线程
            //1.通过方法创建线程
            Thread thread01 = new Thread(Fun1);
            //2.开启线程
            thread01.Start();


            Thread thread02 = new Thread(Fun2);
            thread02.Start(2);

            thread03 = new Thread(Fun3);
            thread03.Start();
        }

        private void Fun2(object obj)
        {
            int n = (int)obj;
            for(int i = 0; i < n; i++) 
            {
                print(i);
            }
        }

        private void Fun3()
        {
            int i = 0;
            while (true)
            {
                i++;
                print(i);
                Thread.Sleep(1000);
            }
        }

        private void Fun1()
        {
            for(int i = 0; i <5; i++)
            {
                //等一下
                signal.WaitOne();
                print(i);
                Thread.Sleep(1000);//线程睡眠一秒
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("暂停"))
            {
                signal.Reset();
            }
            if (GUILayout.Button("恢复"))
            {
                signal.Set();
            }
        }
        private void OnApplicationQuit()
        {
            thread03.Abort();
        }
    }
}
