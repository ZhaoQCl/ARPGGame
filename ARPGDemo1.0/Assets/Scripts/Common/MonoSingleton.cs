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
    /// 脚本单例类
    /// </summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        //设置为泛型T，防止对象无法调用子类方法

        //代码复用
        //解决方案：做了一个父类，统一创建instance属性，子类自动获得，

        //T 表示子类类型
        //public static T Instance { get; private set; }
        private static T instance;

        //Awake调用影响脚本生命周期
        //解决方案：按需加载，创建字段，仅在创建时调用
        public static T Instance
        {
            get
            {
                if (instance == null) 
                {
                    //在场景中根据类型查找引用
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        //创建脚本对象(立即执行Awake)
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
        //Awake，如果自行挂载脚本，则自动instance赋值
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
