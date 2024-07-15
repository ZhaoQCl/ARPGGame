using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.MyCharacter
{
    /// <summary>
    /// 动画角色系统
    /// </summary>
    [System.Serializable]//可以序列化
    public class MyCharacterAnimationParameter
    {
        public string run = "run";

        public string idle = "idle";

        public string death = "death";

        public string walk = "walk";

        public string attack01 = "attack1";

        public string attack02 = "attack2";

        public string attack03 = "attack3";
    }
}
