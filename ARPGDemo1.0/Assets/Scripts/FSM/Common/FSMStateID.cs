using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * 
 */
namespace AI.FSM
{
    /// <summary>
    /// ×´Ì¬±àºÅ
    /// </summary>
    public enum FSMStateID
    {
        //²»´æÔÚ×´Ì¬
        None,
        //Ä¬ÈÏ
        Default,
        //ËÀÍö
        Dead,
        //ÏÐÖÃ
        Idle,
        //×·Öð
        Pursuit,
        //¹¥»÷
        Attacking,
        //Ñ²Âß
        Patrolling
    }
}
