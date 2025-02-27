using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Character/New Character")]

public class CharacterData : ScriptableObject
{
    public string characterName; // 캐릭터 이름
    public Sprite characterSprite; // 캐릭터 스프라이트
    public AnimatorOverrideController characterAnimator; //애니메이션 컨트롤러
}

