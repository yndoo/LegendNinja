using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource bgmSource; // 배경음
    private AudioSource sfxSource; // 효과음

    public AudioClip bgmClip; // 배경음 클립
    public AudioClip[] sfxClips; // 여러 개의 효과음 저장
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 안 사라짐
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        bgmClip = Resources.Load<AudioClip>("Audio/TestBGM2"); 
        sfxClips = new AudioClip[3]; 

        sfxClips[0] = Resources.Load<AudioClip>("Audio/Shuriken");
        //sfxClips[1] = Resources.Load<AudioClip>("Sound/");
        //sfxClips[2] = Resources.Load<AudioClip>("Sound/");


        // 볼륨 설정
        bgmSource.volume = 0.4f;
        sfxSource.volume = 0.25f;

        PlayBGM(); // 배경음 시작
    }

    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(int index)
    {
        if (sfxSource != null && index >= 0 && index < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }
}

