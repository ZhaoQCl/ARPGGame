using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace Common
{
    /// <summary>
    /// 变换组件助手类
    /// </summary>
    public static class TransformHelper
    {
        /// <summary>
        /// 未知层级，查找后代指定名称的变换组件
        /// </summary>
        /// <param name="currentTF">当前变换组件</param>
        /// <param name="childNmae">后代名称</param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF, string childName)
        {
            //递归：方法内部调用又调用自身的过程。
            //1.在子物体种查找
            Transform childTf = currentTF.Find(childName);
            if (childTf != null) return childTf;
            for(int i = 0; i < currentTF.childCount; i++)
            {
                //2.将任务交给子物体
                childTf = FindChildByName(currentTF.GetChild(i), childName);
                if (childTf != null) return childTf;
            }
            return null;
        }
    }
}
