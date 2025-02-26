using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundUI : MonoBehaviour
{
    public Transform background1;  // 첫 번째 배경
    public Transform background2;  // 두 번째 배경
    public float scrollSpeed = 2f;  // 배경 이동 속도

    private Vector3 startPosition1;  // 첫 번째 배경의 초기 위치
    private Vector3 startPosition2;  // 두 번째 배경의 초기 위치

    void Start()
    {
        // 배경의 초기 위치 저장
        startPosition1 = background1.position;
        startPosition2 = background2.position;
    }

    void Update()
    {
        // 배경 이동
        MoveBackground();
    }

    void MoveBackground()
    {
        // 배경 이동
        background1.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        background2.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // 첫 번째 배경이 화면 밖으로 나갔을 때, 두 번째 배경 뒤로 보내기
        if (background1.position.x <= -background1.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            background1.position = new Vector3(background2.position.x + background2.GetComponent<SpriteRenderer>().bounds.size.x, background1.position.y, background1.position.z);
        }

        // 두 번째 배경이 화면 밖으로 나갔을 때, 첫 번째 배경 뒤로 보내기
        if (background2.position.x <= -background2.GetComponent<SpriteRenderer>().bounds.size.x)
        {
            background2.position = new Vector3(background1.position.x + background1.GetComponent<SpriteRenderer>().bounds.size.x, background2.position.y, background2.position.z);
        }
    }
}
