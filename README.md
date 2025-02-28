# 🎮 LegendNinja 🎮

https://github.com/user-attachments/assets/7771a099-5b70-4d5a-ab06-f41af2ddb0f8


# 📌 프로젝트 개요
LegendNinja는 로그라이크 스타일의 액션 게임으로, 강력한 닌자가 되어 몰려오는 적들을 물리치는 게임입니다.
다양한 **스킬**과 **무기**를 조합해 생존을 목표로 합니다.
[빌드 링크](https://play.unity.com/en/games/39c7b41f-32cd-4582-9b28-952342f04667/legend-ninja)
# 팀원
팀장 : 김태겸

팀원 : 배연두, 손효정, 이정구

제작기간 : 2025.02.21 ~ 2025.02.28 (7일)
# 🔥 주요 기능
![Frame 1 (1)](https://github.com/user-attachments/assets/74a80a53-f719-47fd-b28e-9d63c8c5f428)

## 타일맵
- [에셋](https://pixel-boy.itch.io/ninja-adventure-asset-pack)

  타일맵 무료 에셋

- [장애물 스포너](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/Map/ObstacleSpawner.cs)

  맵에 장애물을 소환
  <details>
  <summary>랜덤하게 장애물 배치</summary>

   ```
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
   ```
  </details>

  <details>
    <summary>장애물끼리 겹치는지 확인</summary>
    
    ```
    private bool IsPositionOccupied(Vector2 position,List<Vector2> _spawnedPosition)
    {
        foreach (Vector2 spawnedPosiotion in _spawnedPosition)
        {
            if (Vector2.Distance(position, spawnedPosiotion) < 4f) 
            {
                return true;
            }
        }

        return false;
    }
    ```
    
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
### [데이터 테이블 로더](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/DataTableLoader.cs)  
### [몬스터](https://github.com/BeautifulMaple/LegendNinja/tree/main/Assets/Scripts/Entity/Monster)  
#### 몬스터 스폰
* 각 웨이브에 등장 가능한 몬스터 종류와 개수를 [WaveDataTable](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Resources/WaveDataTable.json)에서 관리합니다. 데이터를 로드해서 웨이브 정보 내에서 랜덤하게 몬스터를 스폰합니다. 
* 몬스터 데이터는 [MonsterDataTable](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Resources/MonsterTable.json)에서 관리하고, 로드된 데이터로 기본 능력치를 설정합니다.
#### 몬스터 종류 
|근접 몬스터|원거리 몬스터|보스 몬스터|
|---|---|---|
| ![melee](https://github.com/user-attachments/assets/4f346428-2bcd-4472-9fba-0ee683f0f1ac) | ![ranged](https://github.com/user-attachments/assets/927dc3d6-2712-4913-a3b0-5c9da0a1701b) | ![hide](https://github.com/user-attachments/assets/c56bb983-2b51-477e-a871-aae53fe8ab45) |

## 스테이지
- [스테이지](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/Map/WaveManager.cs)
- [포탈](https://github.com/BeautifulMaple/LegendNinja/blob/main/Assets/Scripts/Map/WavePortal.cs)

총 2개의 스테이지로 구성.

각 스테이지는 5개의 웨이브를 가진다. 맵에있는 몬스터를 전부 처치하면 웨이브 클리어, 이후 포탈을 통해 다음 웨이브로 진입가능. 마지막인 5웨이브 에서는 보스몬스터 등장. 5개의 웨이브를 전부 클리어하면 다음 스테이지로 진출.

웨이브는 클리어할 때 마다 난이도가 증가(장애물 개수증가, 몬스터 종류와 몬스터 마리수 증가) 





  
