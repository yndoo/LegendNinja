# 🎮 LegendNinja 🎮

https://github.com/user-attachments/assets/7771a099-5b70-4d5a-ab06-f41af2ddb0f8


# 📌 프로젝트 개요
“궁수의 전설”에서 영감을 받아 개발된 액션 로그라이크 게임입니다. 
플레이어는 강력한 닌자가 되어 끊임없이 몰려오는 적들을 물리치며, 다양한 스킬과 무기를 조합하여 생존해야 합니다. 
# 팀원
팀장 : 김태겸
팀원 : 배연두
팀원 : 손효정
팀원 : 이정구

# 🔥 주요 기능
## 타일맵
- [에셋](https://pixel-boy.itch.io/ninja-adventure-asset-pack)
  타일맵

- [장애물 스포너](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/Map/ObstacleSpawner.cs)
  장애물을 소환
  <details>
  <summary>랜덤하게 장애물 배치</summary>
  
    public void SpawnObstacles(Vector2 position, int prefabIndex)
    {
        if (obstaclePrefabs.Length == 0) return;

        prefabIndex = Mathf.Clamp(prefabIndex, 0, obstaclePrefabs.Length - 1);
        GameObject obstacle = Instantiate(obstaclePrefabs[prefabIndex], position, Quaternion.identity);
        spawnedObstacles.Add(obstacle); //생성된 장애물을 리스트에 추가
       
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float y = Random.Range(-mapSize.y / 2, mapSize.y / 2);
        return new Vector2(x, y);
    }
  
  </details>
  


  

## 플레이어 
<details><summary> [주요기능] 가장 가까운 적 탐지 로직</summary>

  ```
  Transform FindCloseMonster()
  {
    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster"); 
    Transform ClosestEnemy = null;
    float MaxDistance = 50f; 
    float ClosestDistance = MaxDistance;
    Vector2 PlayerPos = transform.position; 
    foreach (GameObject monster in monsters)
        {
            float Distance = Vector2.Distance(PlayerPos, monster.transform.position); 
            if (Distance < ClosestDistance) 
            {
                ClosestDistance = Distance; 
                ClosestEnemy = monster.transform; 
        }
    }
    return ClosestEnemy; 
  }
  ```
</details>

## 설명 (각 1줄씩 설명)<br>
1. 몬스터 태그를 가진 오브젝트를 찾습니다.<br>
2. 가장 가까운 적 오브젝트를 저장할 변수를 만들고 처음엔 없으니 Null로 지정합니다.<br>
3. 처음에 볼 거리를 50f로 설정합니다. (탐색할 최대거리)<br>
4. 가장 가까운 거리를 처음에 탐색할 최대거리로 초기화 시킵니다.<br>
5. 플레이어의 위치를 받아옵니다.<br>
6. foreach문을 돌려서 플레이어와 적 사이의 거리를 Distance를 통해 계산합니다.<br>
6-1. 만약 지금까지 저장된 가장 가까운 거리보다 작으면<br>
6-2. 그 거리를 새로운 가장 가까운 거리로 다시 저장합니다.<br>
6-3. 그 해당 적의 transform을 저장합니다.<br>
7. 가장 가까운 적을 반환합니다. (없으면 Null을 반환합니다.)<br>

## 스킬 및 업그레이드
### [스킬](https://github.com/BeautifulMaple/LegendNinja/tree/main/Assets/Scripts/Skill)
### [UI](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/UI/SkillSelectionUI.cs)
### SkillData
  - SkillData 클래스는 개별 스킬의 데이터를 저장합니다. 이 클래스는 스킬의 ID, 이름, 타입, 값, 설명, 스프라이트 경로 등을 포함합니다.
### SkillManager
  - SkillManager 클래스는 스킬 데이터를 관리하고, 스킬을 적용하는 역할을 합니다. 주요 메서드는 다음과 같습니다.
    - GetSkillList(): 스킬 리스트를 반환합니다.
    - ApplySkill(int skillId): 주어진 스킬 ID에 해당하는 스킬을 적용합니다.
    - AddweaponList(SkillData skill): 무기 리스트에 스킬을 추가합니다.
### SkillSelectionUI
  - SkillSelectionUI 클래스는 스킬 선택 UI를 관리합니다. 주요 메서드는 다음과 같습니다.
    - SetupSkillButtons(): 스킬 선택 UI를 설정하고 3개의 랜덤한 스킬을 표시합니다.
    - SelectSkill(SkillData skillData): 선택한 스킬을 적용하고 패널을 닫습니다.
    - OpenPanel(): 스킬 선택 패널을 열고 스킬 버튼을 초기화합니다.
## 몬스터
- 코드 링크 설정
- 주요 기능 설명
## 스테이지
총 2개의 스테이지로 구성.

각 스테이지는 5개의 웨이브를 가진다. 맵에있는 몬스터를 전부 처치하면 웨이브 클리어, 이후 포탈을 통해 다음 웨이브로 진입가능. 마지막인 5웨이브 에서는 보스몬스터 등장. 5개의 웨이브를 전부 클리어하면 다음 스테이지로 진출.

웨이브는 클리어할 때 마다 난이도가 증가(장애물 개수증가, 몬스터 종류와 몬스터 마리수 증가) 

- [스테이지](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/Map/WaveManager.cs)


  
