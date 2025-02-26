using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PublicDefinitions
{
    #region 열거형
    /// <summary>
    /// 전투 타입  
    /// </summary>
    public enum EAttackType
    {
        Melee,  // 근접 공격 
        Ranged, // 원거리 공격 (투사체발사)
        AoE,    // 범위 공격 (Area of Effect)
        Boss,
    }
    #endregion 

    #region 상수
    #endregion 
}
