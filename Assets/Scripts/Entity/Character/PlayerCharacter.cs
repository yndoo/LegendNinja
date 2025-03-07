using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterSpriteRenderer;
    [SerializeField] private Animator animator;

    private void Start()
    {
        // CharacterSelector를 통해 전달된 데이터를 확인
        if (CharacterSelector.Instance != null && CharacterSelector.Instance.SelectedCharacterData != null)
        {
            SetCharacter(CharacterSelector.Instance.SelectedCharacterData);
        }
        else
        {
            Debug.LogWarning("선택된 캐릭터 데이터가 없습니다.");
        }
    }

    public void SetCharacter(CharacterData newCharacter)
    {
        if (newCharacter == null)
        {
            Debug.LogWarning("선택한 캐릭터 데이터가 올바르지 않습니다.");
            return;
        }

        if (characterSpriteRenderer == null)
        {
            Debug.LogError("PlayerCharacter: SpriteRenderer가 설정되지 않았습니다!");
            return;
        }

        // 애니메이션 비활성 후 스프라이트 변경
        if (animator != null)
        {
            animator.enabled = false;
        }

        characterSpriteRenderer.sprite = newCharacter.characterSprite;
        Debug.Log($"변경된 스프라이트 : {characterSpriteRenderer.sprite.name}");

        if (animator != null && newCharacter.characterAnimator != null)
        {
            animator.runtimeAnimatorController = newCharacter.characterAnimator;
            Debug.Log($"애니메이터 변경: {newCharacter.characterAnimator.name}");
        }
        else
        {
            Debug.LogWarning("AnimatorOverrideController가 설정되지 않았습니다.");
        }

        // 잠시 후 애니메이터 다시 활성화(애니메이션 적용)
        StartCoroutine(ReenableAnimator());
    }

    private System.Collections.IEnumerator ReenableAnimator()
    {
        yield return new WaitForSeconds(0.1f);
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
}
