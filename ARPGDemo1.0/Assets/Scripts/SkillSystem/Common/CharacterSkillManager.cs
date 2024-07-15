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
        //技能列表
        public SkillData[] skills;

        private void Start()
        {
            for(int i = 0 ; i < skills.Length; i++) { InitSkill(skills[i]); }
        }

        //初始化技能
        private void InitSkill(SkillData skillData)
        {
            /*
                                            资源映射表
            资源名称                                    资源完整路径
            BaseMeleeAttackSkiill    =     Skill/BaseMeleeAttackSkill
             */
            //data.prefabName --> data.skillPrefab
            //若不是固定的路径，就不能固定死Skill/prefabName
            //skillData.skillPrefab = Resources.Load<GameObject>("Skill/" + skillData.prefabName);
            skillData.skillPrefab = ResourceManager.Load<GameObject>(skillData.prefabName);

            skillData.owner = this.gameObject;
        }

        //技能释放条件：冷却 法力 
        public SkillData PrepareSkill(int id)
        {
            //根据id 查找技能数据
            SkillData skillData = skills.Find(s => s.skilllD == id);

            //获取当前角色法力值
            float sp = GetComponent<MyCharacterStatus>().SP;
            //判断条件
            if (skillData != null&&skillData.coolRemain<=0&&skillData.costSP<=sp) 
                return skillData;
            else return null;
            //返回技能数据
        }

        //生成技能
        public void GenerateSkill(SkillData skillData)
        {
            //创建技能预制件
            //GameObject skillGO= Instantiate(skillData.skillPrefab, transform.position, transform.rotation);
            GameObject skillGO = GameObjectPool.Instance.CreateObject(skillData.prefabName, skillData.skillPrefab, transform.position, transform.rotation);
            
            //传递技能数据
            SkillDeployer deployer = skillGO.GetComponent<SkillDeployer>();
            deployer.SkillData = skillData;//内部创建算法对象
            deployer.DeploySkill();
            //销毁技能
            //Destroy(skillGO,skillData.durationTime);
            GameObjectPool.Instance.CollectObject(skillGO, skillData.durationTime);

            //技能冷却
            StartCoroutine(CoolTimeDown(skillData));
        }

        //技能冷却
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
