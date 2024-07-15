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
            if (GUILayout.Button("����"))
            {
                iterator = Fun1();
                //iterator = Fun1(() => 1 > 2);
            }
            if (GUILayout.Button("ִ��һ��"))
            {
                iterator.MoveNext();
            }
            if (GUILayout.Button("Э��"))
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
                yield return null;//�ȴ�һ֡��Ⱦ
                //yield return new WaitForSeconds(1);//�ȴ�һ��
                //yield return new WaitUntil(handler);//�ȴ�ί��

            }
        }

        /*
         * ���嵭��
         * color a 1 -> 0
         */

        private MeshRenderer meshRenderer;
        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public float fadeSpeed = 10;

        //����
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

        //��ɫ����


    }
}
