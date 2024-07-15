using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.MyCharacter
{
    public class MyPlayerStatus : MyCharacterStatus
    {
        /// <summary>
        /// 死亡
        /// </summary>
        /// 
        private new void Start()
        {
            //通过子类型引用调用，覆盖父类型同名方法，好像它不存在
            //在unity中，将子类附加到物体中，创建子类对象，相当于通过子类型引用调用脚本生命周期
            //解决脚本生命周期冲突：可以在子类方法中使用base.父类方法防止隐藏
        }
        protected override void Death()
        {
            //在运行时修改父类方法表地址
            //因为需要调用父类，执行子类方法
            base.Death();
            print("游戏结束");    
        }
    }
}
