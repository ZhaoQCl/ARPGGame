using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/*
 * 
 * 
 */
namespace ns 
{
    /// <summary>
    /// 
    /// </summary>
    public class Coroutine02 : MonoBehaviour
    {

        // a1 b1 d1 f1 ..... c106 e106
        private Coroutine coroutine;

        private void Start()
        {
            print("a:" + Time.frameCount);
            coroutine = StartCoroutine(Fun1());
            print("d:" + Time.frameCount);
            StartCoroutine(Fun2());
            print("f:" + Time.frameCount);
        }

        private IEnumerator Fun1()
        {
            print("b:" + Time.frameCount);
            yield return new WaitForSeconds(2);
            print("c:" + Time.frameCount);
        }

        private IEnumerator Fun2()
        {
            yield return coroutine;
            print("e:" + Time.frameCount);
        }

        
        public Transform[] wayPoints;
        /*
         ͨ��Э��ʵ��Ѱ·A   B   C....
        Transform  ----> A
        Translate()����Ѱ·
        Vector3.MoveTowards()��Ѱ·������һ�����ƶ�һС��
         */
        public float speed;

        private IEnumerator MoveToTraget(Vector3 position)
        {
            transform.LookAt(position);
            while (Vector3.Distance(transform.position, position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator PathFinding()
        {
            for(int i = 0; i < wayPoints.Length; i++)
            {
                yield return StartCoroutine(MoveToTraget(wayPoints[i].position));
            } 
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Ѱ·"))
            {
                StartCoroutine(PathFinding());
            }
        }
    }
}
