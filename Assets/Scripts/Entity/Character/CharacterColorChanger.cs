using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterColorChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer characterSprite;  // 2D 캐릭터 스프라이트
    [SerializeField] private Image characterPreviewImage; // UI 캐릭터 미리보기
    [SerializeField] private Image previewImage; // 미리보기 색상 UI

    [SerializeField] private Slider rSlider, gSlider, bSlider, aSlider;
    [SerializeField] private TMP_InputField rInput, gInput, bInput, aInput;

    private Color currentColor;

    void Start()
    {
        characterPreviewImage.sprite = characterSprite.sprite;

        //  초기 색상을 characterSprite의 material.color에서 가져옴
        currentColor = characterSprite.material.color;

        //  슬라이더 값 설정
        rSlider.value = currentColor.r;
        gSlider.value = currentColor.g;
        bSlider.value = currentColor.b;
        aSlider.value = currentColor.a;

        //  입력 필드 값 설정 (0~1 범위를 255 기준으로 변환)
        rInput.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
        aInput.text = Mathf.RoundToInt(currentColor.a * 255).ToString();

        // 슬라이더 값 변경 시 이벤트 연결
        rSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        gSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        bSlider.onValueChanged.AddListener(UpdateColorFromSlider);
        aSlider.onValueChanged.AddListener(UpdateColorFromSlider);

        // 입력 필드 값 변경 시 이벤트 연결
        rInput.onEndEdit.AddListener(UpdateColorFromInput);
        gInput.onEndEdit.AddListener(UpdateColorFromInput);
        bInput.onEndEdit.AddListener(UpdateColorFromInput);
        aInput.onEndEdit.AddListener(UpdateColorFromInput);

        ApplyColorToUI();
    }

    //  슬라이더 값 변경 시 색상 업데이트
    private void UpdateColorFromSlider(float value)
    {
        currentColor = new Color(rSlider.value, gSlider.value, bSlider.value, aSlider.value);
        ApplyColor();
    }

    //  입력 필드 값 변경 시 색상 업데이트
    private void UpdateColorFromInput(string value)
    {
        float r = Mathf.Clamp01(float.Parse(rInput.text) / 255f);
        float g = Mathf.Clamp01(float.Parse(gInput.text) / 255f);
        float b = Mathf.Clamp01(float.Parse(bInput.text) / 255f);
        float a = Mathf.Clamp01(float.Parse(aInput.text) / 255f);

        rSlider.value = r;
        gSlider.value = g;
        bSlider.value = b;
        aSlider.value = a;

        currentColor = new Color(r, g, b, a);
        ApplyColor();
    }

    //  UI 요소 업데이트 (미리보기 & 숫자 입력 필드 반영)
    private void ApplyColorToUI()
    {
        rInput.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
        gInput.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
        bInput.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
        aInput.text = Mathf.RoundToInt(currentColor.a * 255).ToString();

        previewImage.color = currentColor;
        characterPreviewImage.color = currentColor;
    }

    //  캐릭터 색상 적용
    private void ApplyColor()
    {
        characterSprite.material.color = currentColor; //  material.color 사용
        previewImage.color = currentColor;
        characterPreviewImage.color = currentColor;
    }
}
