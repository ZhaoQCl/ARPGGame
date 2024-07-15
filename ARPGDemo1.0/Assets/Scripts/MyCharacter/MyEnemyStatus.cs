using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPGDemo.MyCharacter
{
    public class MyEnemyStatus : MyCharacterStatus
    {
        protected override void Death()
        {
            base.Death();
            Destroy(gameObject, 10);
        }
    }
}

