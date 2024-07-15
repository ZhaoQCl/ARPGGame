using Common;
using System.Collections;
using System.Collections.Generic;
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
    public class Bullet : MonoBehaviour,IResetable
    {
        private Vector3 targetPos;

        public void OnReset()
        {
            throw new System.NotImplementedException();
        }

        private void Start()
        {
            //������ǰ��50m����������
            //TransformPoint����������-->��������
            targetPos = transform.TransformPoint(0,0,50);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 50);
            if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                //Destroy(gameObject);
                GameObjectPool.Instance.CollectObject(gameObject);
            }
        }

        
    }
}
