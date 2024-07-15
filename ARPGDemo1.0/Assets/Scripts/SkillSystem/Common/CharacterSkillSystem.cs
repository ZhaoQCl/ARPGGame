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
    /// ��װ����ϵͳ���ṩ�򵥵ļ����ͷŹ���
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

        //���ż��ܶ��� --- ���ɼ���
        //���ż��ܶ��� --- ��������չ�---- ���ɼ��ܺ��չ�
        //�����չ����� --- ���ɼ��ܺ��չ�
        //��������
        //private List<SkillData> skillData;

        private SkillData skillData;
        /// <summary>
        /// ʹ�ü��ܹ�������ң�
        /// </summary>
        public void AttackUseSkill(int skillID,bool isBatter = false)
        {
            //��������������һ���ͷŵļ����л�ȡ�����ı��
            if (skillData != null &&isBatter)
                skillID = skillData.nextBatterld;
            //׼������
            skillData = skillManager.PrepareSkill(skillID);
            if (skillData == null) return;
            //���Ŷ���
            anim.SetBool(skillData.animationName, true);

            if (skillData.attackType != SkillAttackType.Single) return;
            //�������
            Transform targetTF = SelectTarget();
            //-- ����Ŀ��
            transform.LookAt(targetTF);
            //-- ѡ��Ŀ��
            //1.ѡ��Ŀ�꣬���ָ��ʱ���ȡ��ѡ�С�
            //2.ѡ��AĿ�꣬���Զ�ȡ��ǰѡ������Ŀ�꣬ȡ��A��ѡ��B
            //ȡ���ϴ�����
            SetSelectedActiveFx(false);

            selectedTF = targetTF;
            //ѡ�е�ǰ����
            SetSelectedActiveFx(true);

        }
        [HideInInspector]
        public Transform selectedTF;

        //��������״̬
        private void SetSelectedActiveFx(bool state)
        {
            if (selectedTF == null) return;
            var selected = selectedTF.GetComponent<MyCharacter.MyCharacterSelected>();
            if (selected) selected.SetSelectedActive(state);
        }

        //ѡȡ����
        private Transform SelectTarget()
        {
            Transform[] targets = new SectorAttackSelector().SelectTarget(skillData, transform);
            return targets.Length != 0 ? targets[0] : null;
        }
        /// <summary>
        /// ʹ��������ܣ�NPC��
        /// </summary>
        public void UseRandomSkill()
        {
            //���󣺴� ������ ����ѡ������ļ���
            //-- ��ɸѡ�����п����ͷŵļ��ܣ��ٲ��������
            var usableSkills =  skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skilllD) != null);
            if (usableSkills.Length == 0) return;
            int index = Random.Range(0,usableSkills.Length);

            //�ͷż���
            AttackUseSkill(usableSkills[index].skilllD);
        }
    }
}
