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
    /// �ű�������
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        //����Ϊ����T����ֹ�����޷��������෽��

        //���븴��
        //�������������һ�����࣬ͳһ����instance���ԣ������Զ���ã�

        //T ��ʾ��������
        //public static T Instance { get; private set; }
        private static T instance;

        //Awake����Ӱ��ű���������
        //���������������أ������ֶΣ����ڴ���ʱ����
        public static T Instance
        {
            get
            {
                if (instance == null) 
                {
                    //�ڳ����и������Ͳ�������
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        //�����ű�����(����ִ��Awake)
                        new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }
                }
                return instance;
            }
        }
        //Awake��������й��ؽű������Զ�instance��ֵ
        protected void Awake()
        {
            if(instance == null)
            {
                instance = this as T;
                Init();
            }  
        }

        public virtual void Init() { }

        
    }
}
