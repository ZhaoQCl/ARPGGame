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
    /// 角色状态类
    /// </summary>
    public class MyCharacterStatus : MonoBehaviour
    {
        [Tooltip("动画参数")]
        public MyCharacterAnimationParameter chParams;

        [Tooltip("血量")]
        public float HP;

        [Tooltip("最大血量")]
        public float maxHP;

        [Tooltip("法力")]
        public float SP;

        [Tooltip("最大法力")]
        public float maxSP;

        [Tooltip("基础攻击力")]
        public float baseATK;

        [Tooltip("防御力")]
        public float defence;

        [Tooltip("攻击间隔")]
        public float attackInterval;

        [Tooltip("攻击距离")]
        public float attackDistance;

        /// <summary>
        /// 受伤方法
        /// </summary>
        /// <param name="val">受到伤害</param>
        public void Damage(float val)
        {
            val -= defence;
            if (val <= 0) return;
            HP -= val;
            if (HP <= 0) Death();
        }

        /// <summary>
        /// 死亡
        /// </summary>

        //调用父类死亡方法，执行子类死亡方法。
        protected virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
        }
    }
}
