using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    public class Coroutine01 : MonoBehaviour
    {
        private IEnumerator iterator;
        private void OnGUI()
        {
            if (GUILayout.Button("启动"))
            {
                iterator = Fun1();
                //iterator = Fun1(() => 1 > 2);
            }
            if (GUILayout.Button("执行一次"))
            {
                iterator.MoveNext();
            }
            if (GUILayout.Button("协程"))
            {
                StartCoroutine(iterator);
            }

            if (GUILayout.Button("Fade"))
            {
                StartCoroutine(Fade());
            }
        }

        private IEnumerator Fun1()
        {
            for (int i = 0; i < 5; i++)
            {
                print(i + "------" + Time.frameCount);
                yield return null;//等待一帧渲染
                //yield return new WaitForSeconds(1);//等待一秒
                //yield return new WaitUntil(handler);//等待委托

            }
        }

        /*
         * 物体淡出
         * color a 1 -> 0
         */

        private MeshRenderer meshRenderer;
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public float fadeSpeed = 10;

        //淡出
        private IEnumerator Fade()
        {
            Color currentColor = meshRenderer.material.color;
            while (currentColor.a > 0)
            {
                currentColor.a -= fadeSpeed * Time.deltaTime;
                meshRenderer.material.color = currentColor;
                yield return null;
            }
            currentColor.a = 0;
            meshRenderer.material.color = currentColor;
        }

        //颜色渐变


    }
}
