using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPGDemo.MyCharacter;
/*
* 
* 
*/
namespace ARPGDemo.Skill 
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterSkillManager : MonoBehaviour
    {
        //�����б�
        public SkillData[] skills;

        private void Start()
        {
            for(int i = 0 ; i < skills.Length; i++) { InitSkill(skills[i]); }
        }

        //��ʼ������
        private void InitSkill(SkillData skillData)
        {
            /*
                                            ��Դӳ���
            ��Դ����                                    ��Դ����·��
            BaseMeleeAttackSkiill    =     Skill/BaseMeleeAttackSkill
             */
            //data.prefabName --> data.skillPrefab
            //�����ǹ̶���·�����Ͳ��̶ܹ���Skill/prefabName
            //skillData.skillPrefab = Resources.Load<GameObject>("Skill/" + skillData.prefabName);
            skillData.skillPrefab = ResourceManager.Load<GameObject>(skillData.prefabName);

            skillData.owner = this.gameObject;
        }

        //�����ͷ���������ȴ ���� 
        public SkillData PrepareSkill(int id)
        {
            //����id ���Ҽ�������
            SkillData skillData = skills.Find(s => s.skilllD == id);

            //��ȡ��ǰ��ɫ����ֵ
            float sp = GetComponent<MyCharacterStatus>().SP;
            //�ж�����
            if (skillData != null&&skillData.coolRemain<=0&&skillData.costSP<=sp) 
                return skillData;
            else return null;
            //���ؼ�������
        }

        //���ɼ���
        public void GenerateSkill(SkillData skillData)
        {
            //��������Ԥ�Ƽ�
            //GameObject skillGO= Instantiate(skillData.skillPrefab, transform.position, transform.rotation);
            GameObject skillGO = GameObjectPool.Instance.CreateObject(skillData.prefabName, skillData.skillPrefab, transform.position, transform.rotation);
            
            //���ݼ�������
            SkillDeployer deployer = skillGO.GetComponent<SkillDeployer>();
            deployer.SkillData = skillData;//�ڲ������㷨����
            deployer.DeploySkill();
            //���ټ���
            //Destroy(skillGO,skillData.durationTime);
            GameObjectPool.Instance.CollectObject(skillGO, skillData.durationTime);

            //������ȴ
            StartCoroutine(CoolTimeDown(skillData));
        }

        //������ȴ
        private IEnumerator CoolTimeDown(SkillData skillData)
        {
             skillData.coolRemain = skillData.coolTime;
             while(skillData.coolRemain > 0)
             {
                yield return new WaitForSeconds(1);
                skillData.coolRemain--;
             }
        }
    }
}
