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
            // 创建物体 -->  设置位置/旋转  --> 立即执行Awake()
            //Instantiate(bulletPrefab,transform.position,transform.rotation);
            GameObjectPool.Instance.CreateObject("bullet",bulletPrefab,transform.position,transform.rotation);
            //bullet.transform.parent = transform.FindChildByName("FirePoint");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("发射"))
            {
                Fire();
            }
            if (GUILayout.Button("清空类别"))
            {
                GameObjectPool.Instance.Clear("bullet");
            }
            if (GUILayout.Button("清空全部"))
            {
                GameObjectPool.Instance.ClearAll();
            }
        }
    }
}
