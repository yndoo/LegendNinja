using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Character/Character List")]

public class CharacterList : ScriptableObject
{
    public List<CharacterData> characters; // 캐릭터 데이터 리스트
}
