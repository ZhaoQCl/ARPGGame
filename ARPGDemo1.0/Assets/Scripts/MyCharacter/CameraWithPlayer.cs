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
    public class CameraWithPlayer : MonoBehaviour
    {
        public Transform playerTransform;
        public Vector3 offset;
        //public float followSpeed = 2f;
        //public float followHeight = 4f;
        //public float distance = 8f;

        //private Vector3 velocity = Vector3.zero;

        public void LateUpdate()
        {
            transform.position = playerTransform.position + offset; 
            //Vector3 platerPosition = playerTransform.position - (playerTransform.forward * distance) 
            //    + new Vector3(0, followHeight, 0);
            //transform.position = Vector3.SmoothDamp(transform.position, platerPosition, ref velocity, 1f / followSpeed);
            //transform.LookAt(playerTransform);
        }
    }
}
