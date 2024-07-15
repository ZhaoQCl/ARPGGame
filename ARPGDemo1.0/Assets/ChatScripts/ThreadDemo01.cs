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
            //���߳�ֱ�ӵ���
            //Fun1();

            


            //���д������ȳ壬��֮Ϊ���߳�/�����߳�
            //1.ͨ�����������߳�
            Thread thread01 = new Thread(Fun1);
            //2.�����߳�
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
                //��һ��
                signal.WaitOne();
                print(i);
                Thread.Sleep(1000);//�߳�˯��һ��
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("��ͣ"))
            {
                signal.Reset();
            }
            if (GUILayout.Button("�ָ�"))
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
