using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace ARPGDemo.MyCharacter 
{
    /// <summary>
    /// ��Ϸ����ѡ����
    /// </summary>
    public class MyCharacterSelected : MonoBehaviour
    {
        private GameObject selectedGO;
        [Tooltip("ѡ������Ϸ��������")]
        public string selectedName = "selected";
        private void Start()
        {
            selectedGO = transform.Find(selectedName).gameObject;
        }
        [Tooltip("��ʾʱ��")]
        public float displayTime = 3;
        public void SetSelectedActive(bool state)
        {
            //����ѡ�������弤��״̬
            selectedGO.SetActive(state);
            //���õ�ǰ�ű�����״̬
            this.enabled = state;
            //�ȴ������������ ������Э��
            if(state)
            {
                hideTime = Time.time + 3;
            }

        }

        private float hideTime;

        private void Update()
        {
            if(hideTime <= Time.time)
            {
                SetSelectedActive(false);
            }
        }
    }
}
