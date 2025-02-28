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
- 에셋출처 및 주요 기능 설명
## 플레이어 
- 코드 링크 설정
- 주요 기능 설명
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
- 코드 링크 설정
- 주요 기능 설명
