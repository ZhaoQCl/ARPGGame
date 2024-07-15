using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

/*
 * 
 * 
 */
namespace ARPGDemo.MyCharacter
{
    /// <summary>
    /// 角色马达：负责控制角色运动
    /// </summary>
    public class MyCharacterMotor : MonoBehaviour
    {


        //旋转速率
        [Tooltip("旋转速度")]
        public float rotationSpeed = 0.5f;

        //移动速度
        [Tooltip("移动速度")]
        public float moveSpeed = 5;

        //动画系统
        
        //输入控制器
        private CharacterController chController;

        private void Start()
        {
            chController = GetComponent<CharacterController>();
        }

        //注视目标方向旋转
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion quaternion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, rotationSpeed);
        }
        //移动
        public void Movement(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            LookAtTarget(direction);
            Vector3 motion = new Vector3(transform.forward.x, -1, transform.forward.z);
            chController.Move(motion * Time.deltaTime * moveSpeed);
            //向前移动
            //CharacterController    Move
        }

    }
}

