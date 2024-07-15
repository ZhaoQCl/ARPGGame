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
    /// ��ɫ���������
    /// </summary>


    [RequireComponent(typeof(MyCharacterMotor),typeof(MyPlayerStatus))]

    public class MyCharacterInputController : MonoBehaviour
    {

        //��ɫ���
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
            //��������ƶ�����
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
            //ע���¼�
            EasyJoystick.On_JoystickMove += JoystickMove;
            EasyJoystick.On_JoystickMoveEnd += JoystickMoveEnd;
            EasyJoystick.On_JoystickMoveStart += JoystickMoveStart;
            EasyButton.On_ButtonDown += ButtonDown;
            //EasyButton.On_ButtonPress += ButtonPress;
            EasyButton.On_ButtonUp += ButtonUp;
        }


        private void OnDisable()
        {
            //ע���¼�
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

        //�����ť��һ������
        //��Ҫͨ���¼������࣬���ֵ���İ�ť��
        private float lastPressTime = -1;
        private void ButtonPress(string buttonName)
        {
            //���󣺰�ס��������С(2) ��ȡ������
            //���С��5����Ϊ����

            //�������ǰ����ʱ�� - �ϴΰ���ʱ��
            float currentTime = Time.time - lastPressTime;

            if (currentTime < 2) return;

            bool isBatter = currentTime <= 5;

            skillSystem.AttackUseSkill(1001, isBatter);

            lastPressTime = Time.time;
            print(lastPressTime);
        }

        private void ButtonDown(string buttonName)
        {

            //������ڹ��� ���˳�
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
