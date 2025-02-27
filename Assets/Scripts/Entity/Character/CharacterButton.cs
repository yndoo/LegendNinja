using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Image characterImage; // 캐릭터 아이콘
    [SerializeField] private TextMeshProUGUI characterNameText; // 캐릭터 이름

    private CharacterData characterData;
    private CharacterSelector characterSelector;

    public void Setup(CharacterData character, CharacterSelector selector)
    {
        characterData = character;
        characterSelector = selector;

        characterImage.sprite = character.characterSprite;
        characterNameText.text = character.characterName;

        GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log($" {characterData.characterName} 버튼 클릭됨!");
            characterSelector.SelectCharacter(characterData);
        });
    }
}
