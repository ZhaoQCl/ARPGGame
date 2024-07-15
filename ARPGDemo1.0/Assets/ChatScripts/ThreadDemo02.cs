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
    public class ThreadDemo02 : MonoBehaviour
    {
        private void Start()
        {
            //ThreadPool.QueueUserWorkItem(Fun1);
            ThreadPool.QueueUserWorkItem(obj => 
            {
                Fun2();
            });
        }

        private void Fun1(object state)
        {
            Fun2();
        }

        private void Fun2()
        {

        }
    }


}
