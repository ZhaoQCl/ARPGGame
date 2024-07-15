using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
/*
 * 
 * 
 */
namespace ns 
{
    /// <summary>
    /// 
    /// </summary>
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public void Fire()
        {
            // �������� -->  ����λ��/��ת  --> ����ִ��Awake()
            //Instantiate(bulletPrefab,transform.position,transform.rotation);
            GameObjectPool.Instance.CreateObject("bullet",bulletPrefab,transform.position,transform.rotation);
            //bullet.transform.parent = transform.FindChildByName("FirePoint");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("����"))
            {
                Fire();
            }
            if (GUILayout.Button("������"))
            {
                GameObjectPool.Instance.Clear("bullet");
            }
            if (GUILayout.Button("���ȫ��"))
            {
                GameObjectPool.Instance.ClearAll();
            }
        }
    }
}
