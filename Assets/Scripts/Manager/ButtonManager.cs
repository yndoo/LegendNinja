using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button StartButton;
    public Button SettingButton;
    public Button ExitButton;
    public Button CloseButton;

    public GameObject settingUI;

    private void Start()
    {
        if (StartButton != null)
            StartButton.onClick.AddListener(OnButtonClick);

        if (SettingButton != null)
            SettingButton.onClick.AddListener(OnButtonClick);

        if (ExitButton != null)
            ExitButton.onClick.AddListener(OnButtonClick);

        if (CloseButton != null)
            CloseButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        SoundManager.instance.PlaySFX(1);
    }

    public void OffSettingUI()
    {
        settingUI.SetActive(false);
    }
    public void OnSettingUI()
    {
        settingUI.SetActive(true);
    }
}
