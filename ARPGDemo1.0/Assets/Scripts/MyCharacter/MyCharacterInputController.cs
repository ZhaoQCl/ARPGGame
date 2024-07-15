using ARPGDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPGDemo.Skill;
/*
 * 
 * 
 */
namespace ARPGDemo.MyCharacter
{
    /// <summary>
    /// 角色输入控制器
    /// </summary>


    [RequireComponent(typeof(MyCharacterMotor),typeof(MyPlayerStatus))]

    public class MyCharacterInputController : MonoBehaviour
    {

        //角色马达
        private MyCharacterMotor chMotor;
        private MyPlayerStatus playerStatus;
        private Animator animator;
        private CharacterSkillSystem skillSystem;

        private void Start()
        {
            chMotor = GetComponent<MyCharacterMotor>();
            playerStatus = GetComponent<MyPlayerStatus>();
            animator = GetComponentInChildren<Animator>();
            skillSystem = GetComponent<CharacterSkillSystem>();
        }
        private void JoystickMove(MovingJoystick move)
        {
            if (IsAttacking()) return;
            chMotor.Movement(new Vector3(move.joystickAxis.x, 0, move.joystickAxis.y));
            //调用马达移动功能
        }
        private void JoystickMoveEnd(MovingJoystick move)
        {
            animator.SetBool(playerStatus.chParams.idle, true);
            animator.SetBool(playerStatus.chParams.run, false);
        }
        private void JoystickMoveStart(MovingJoystick move)
        {
            animator.SetBool(playerStatus.chParams.run, true);
            animator.SetBool(playerStatus.chParams.idle, false);
        }

        private void OnEnable()
        {
            //注册事件
            EasyJoystick.On_JoystickMove += JoystickMove;
            EasyJoystick.On_JoystickMoveEnd += JoystickMoveEnd;
            EasyJoystick.On_JoystickMoveStart += JoystickMoveStart;
            EasyButton.On_ButtonDown += ButtonDown;
            //EasyButton.On_ButtonPress += ButtonPress;
            EasyButton.On_ButtonUp += ButtonUp;
        }


        private void OnDisable()
        {
            //注销事件
            EasyJoystick.On_JoystickMove -= JoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= JoystickMoveEnd;
            EasyJoystick.On_JoystickMoveStart -= JoystickMoveStart;
            EasyButton.On_ButtonDown -= ButtonDown;
            //EasyButton.On_ButtonPress -= ButtonPress;
            EasyButton.On_ButtonUp -= ButtonUp;
        }

        private void ButtonUp(string buttonName)
        {
            //if (buttonName.Equals("Skillbutton"))
            //    animator.SetBool(playerStatus.chParams.attack01, false);
            //else if (buttonName.Equals("Skillbutton01"))
            //    animator.SetBool(playerStatus.chParams.attack02, false);
            //else if (buttonName.Equals("Skillbutton02"))
            //    animator.SetBool(playerStatus.chParams.attack03, false);
        }

        //多个按钮绑定一个方法
        //需要通过事件参数类，区分点击的按钮。
        private float lastPressTime = -1;
        private void ButtonPress(string buttonName)
        {
            //需求：按住间隔如果过小(2) 则取消攻击
            //间隔小于5秒视为连击

            //间隔：当前按下时间 - 上次按下时间
            float currentTime = Time.time - lastPressTime;

            if (currentTime < 2) return;

            bool isBatter = currentTime <= 5;

            skillSystem.AttackUseSkill(1001, isBatter);

            lastPressTime = Time.time;
            print(lastPressTime);
        }

        private void ButtonDown(string buttonName)
        {

            //如果正在攻击 则退出
            if (IsAttacking()) return;
            int id = 0;
            switch (buttonName)
            {
                case "Skillbutton":
                    id = 1001;
                    break;
                case "Skillbutton01":
                    id = 1002;
                    break;
                case "Skillbutton02":
                    id = 1003;
                    break;
            }
            //CharacterSkillManager skillManager = GetComponent<CharacterSkillManager>();
            //animator.SetBool(actName, true);
            //SkillData skillData = skillManager.PrepareSkill(id);
            //if (skillData != null)
            //    skillManager.GenerateSkill(skillData);
            skillSystem.AttackUseSkill(id);

        }


        private bool IsAttacking()
        {
            return 
                animator.GetBool(playerStatus.chParams.attack01) || 
                animator.GetBool(playerStatus.chParams.attack02) || 
                animator.GetBool(playerStatus.chParams.attack03);
        }

    }
}
