using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelector : MonoBehaviour
{
    [Header(" 캐릭터 데이터")]
    [SerializeField] private CharacterList characterList; // 캐릭터 리스트 (ScriptableObject)

    [Header(" UI 요소")]
    [SerializeField] private Transform content; // ScrollView의 Content
    [SerializeField] private GameObject characterButtonPrefab; // 캐릭터 버튼 프리팹
    [SerializeField] private Image characterPreviewImage; // 선택한 캐릭터 미리보기
    [SerializeField] private TextMeshProUGUI characterNameText; // 캐릭터 이름 표시
    [SerializeField] private Button confirmButton; // 선택한 캐릭터 적용 버튼

    [Header(" 플레이어 캐릭터")]
    [SerializeField] private PlayerCharacter playerCharacter; // 플레이어 캐릭터

    public static CharacterSelector Instance;
    public CharacterData SelectedCharacterData;
    private CharacterData selectedCharacter; // 현재 선택한 캐릭터

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
        }
        else
        {
            Destroy(gameObject);
        }
        if (playerCharacter == null)
        {
            playerCharacter = FindObjectOfType<PlayerCharacter>();
            if (playerCharacter == null)
            {
                Debug.LogError(" 씬에서 PlayerCharacter를 찾을 수 없습니다!");
            }
        }
    }
    private void Start()
    {
        GenerateCharacterButtons();
        confirmButton.interactable = false; // 캐릭터를 선택할 때까지 비활성화
        confirmButton.onClick.AddListener(ApplySelectedCharacter);
    }

    private void GenerateCharacterButtons()
    {
        foreach (CharacterData character in characterList.characters)
        {
            GameObject newButton = Instantiate(characterButtonPrefab, content);
            CharacterButton buttonScript = newButton.GetComponent<CharacterButton>();
            buttonScript.Setup(character, this);
        }
    }

    public void SelectCharacter(CharacterData character)
    {
        if (character != null)
        {
            selectedCharacter = character;
            characterPreviewImage.sprite = selectedCharacter.characterSprite;
            characterNameText.text = selectedCharacter.characterName;

            Debug.Log($" 미리보기 업데이트: {selectedCharacter.characterName}");
            confirmButton.interactable = true; // 캐릭터 선택 시 버튼 활성화
        }
    }

    public void ApplySelectedCharacter()
    {
        if (selectedCharacter == null)
        {
            Debug.LogError("선택된 캐릭터가 없습니다!");
            return;
        }

        Debug.Log($"{selectedCharacter.characterName} 캐릭터 변경 시도!");

        if (playerCharacter == null)
        {
            Debug.LogError(" PlayerCharacter가 설정되지 않았습니다!");
            return;
        }

        playerCharacter.SetCharacter(selectedCharacter);
    }

}
