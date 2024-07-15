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
    /// 游戏物体选择器
    /// </summary>
    public class MyCharacterSelected : MonoBehaviour
    {
        private GameObject selectedGO;
        [Tooltip("选择器游戏物体名称")]
        public string selectedName = "selected";
        private void Start()
        {
            selectedGO = transform.Find(selectedName).gameObject;
        }
        [Tooltip("显示时间")]
        public float displayTime = 3;
        public void SetSelectedActive(bool state)
        {
            //设置选择器物体激活状态
            selectedGO.SetActive(state);
            //设置当前脚本激活状态
            this.enabled = state;
            //等待三秒禁用物体 可以用协程
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
