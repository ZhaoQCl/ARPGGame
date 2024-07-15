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
    /// ��ɫ��������ƽ�ɫ�˶�
    /// </summary>
    public class MyCharacterMotor : MonoBehaviour
    {


        //��ת����
        [Tooltip("��ת�ٶ�")]
        public float rotationSpeed = 0.5f;

        //�ƶ��ٶ�
        [Tooltip("�ƶ��ٶ�")]
        public float moveSpeed = 5;

        //����ϵͳ
        
        //���������
        private CharacterController chController;

        private void Start()
        {
            chController = GetComponent<CharacterController>();
        }

        //ע��Ŀ�귽����ת
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion quaternion = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, rotationSpeed);
        }
        //�ƶ�
        public void Movement(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            LookAtTarget(direction);
            Vector3 motion = new Vector3(transform.forward.x, -1, transform.forward.z);
            chController.Move(motion * Time.deltaTime * moveSpeed);
            //��ǰ�ƶ�
            //CharacterController    Move
        }

    }
}

