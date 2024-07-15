using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/*
 * 
 * 
 */
namespace ARPGDemo.Skill 
{
    [RequireComponent(typeof(CharacterSkillManager))]
    /// <summary>
    /// 封装技能系统，提供简单的技能释放功能
    /// </summary>
    public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterSkillManager skillManager;
        private Animator anim;
        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DeploySkill;
        }

        private void DeploySkill()
        {
            skillManager.GenerateSkill(skillData);
        }

        //播放技能动画 --- 生成技能
        //播放技能动画 --- 如果穿插普攻---- 生成技能和普攻
        //播放普攻动画 --- 生成技能和普攻
        //动作队列
        //private List<SkillData> skillData;

        private SkillData skillData;
        /// <summary>
        /// 使用技能攻击（玩家）
        /// </summary>
        public void AttackUseSkill(int skillID,bool isBatter = false)
        {
            //如果连击，则从上一个释放的技能中获取连击的编号
            if (skillData != null &&isBatter)
                skillID = skillData.nextBatterld;
            //准备技能
            skillData = skillManager.PrepareSkill(skillID);
            if (skillData == null) return;
            //播放动画
            anim.SetBool(skillData.animationName, true);

            if (skillData.attackType != SkillAttackType.Single) return;
            //如果单攻
            Transform targetTF = SelectTarget();
            //-- 朝向目标
            transform.LookAt(targetTF);
            //-- 选中目标
            //1.选中目标，间隔指定时间后取消选中。
            //2.选中A目标，在自动取消前选中其他目标，取消A，选中B
            //取消上次物体
            SetSelectedActiveFx(false);

            selectedTF = targetTF;
            //选中当前物体
            SetSelectedActiveFx(true);

        }
        [HideInInspector]
        public Transform selectedTF;

        //设置物体状态
        private void SetSelectedActiveFx(bool state)
        {
            if (selectedTF == null) return;
            var selected = selectedTF.GetComponent<MyCharacter.MyCharacterSelected>();
            if (selected) selected.SetSelectedActive(state);
        }

        //选取物体
        private Transform SelectTarget()
        {
            Transform[] targets = new SectorAttackSelector().SelectTarget(skillData, transform);
            return targets.Length != 0 ? targets[0] : null;
        }
        /// <summary>
        /// 使用随机技能（NPC）
        /// </summary>
        public void UseRandomSkill()
        {
            //需求：从 管理器 中挑选出随机的技能
            //-- 先筛选出所有可以释放的技能，再产生随机数
            var usableSkills =  skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skilllD) != null);
            if (usableSkills.Length == 0) return;
            int index = Random.Range(0,usableSkills.Length);

            //释放技能
            AttackUseSkill(usableSkills[index].skilllD);
        }
    }
}
