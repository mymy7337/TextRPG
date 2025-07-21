# 🧙‍♂️ TextRPG - 콘솔 기반 C# 텍스트 RPG

콘솔에서 즐기는 고전 RPG 스타일 게임!  
직업을 선택하고, 다양한 몬스터와 전투를 벌이며 퀘스트를 클리어해보세요.
---

## 🎮 주요 기능

- ✅ 직업 선택 (전사 / 마법사 / 궁수 / 도적 / 해적)
- ✅ 고유 스킬 사용 및 MP 소모
- ✅ 전투 시스템 (일반 공격, 스킬 공격)
- ✅ 던전 난이도 선택 및 실패 확률
- ✅ 레벨업 및 능력치 성장
- ✅ 상점에서 아이템 구매/판매
- ✅ 인벤토리 및 장착 시스템
- ✅ 퀘스트 수락 및 완료
- ✅ 게임 저장 및 불러오기 (다중 슬롯)
---

## 🛠 기술 스택

| 분류 | 내용 |
|------|------|
| 언어 | C# (.NET Console App) |
| 패턴 | OOP (상속, 다형성, 인터페이스) |
| 자료구조 | List, Dictionary, LINQ |
| 구조화 | 클래스 분리, 모듈화, 책임 분산 |
---

## 🚀 실행 방법

1. Visual Studio로 `TextRPG.sln` 열기
2. `Program.cs` 실행 (또는 Ctrl + F5)
3. 콘솔에서 지시에 따라 플레이 진행
---

## 🚀 GIF
![Text_RPG](https://github.com/user-attachments/assets/e6dddef7-eb3b-44ce-b8a2-c9f12f42ad8a)


🚀 TroubleShooting
1️⃣ 직업 클래스 분리의 한계와 구조 개선
<p align="center"> <img src="https://github.com/user-attachments/assets/47808fec-57a5-4cbb-a315-e8c7c9c67962" width="48%"> <img src="https://github.com/user-attachments/assets/6ee050ce-3498-4b37-a7f8-f5291f270163" width="48%"> </p>  
  
- 초기 설계에서는 직업마다 클래스를 따로 정의하여 능력치와 스킬을 관리했습니다.  
- 하지만 이 방식은 클래스 간 중복 코드가 많아지고,  
- 새로운 직업이 추가될 때마다 클래스를 새로 생성해야 하는 단점이 있었습니다.  
  
- 구조 개선을 통해 직업 정보를 문자열 필드로 전환하고,  
- 스킬은 SkillSet 인터페이스 기반으로 관리하도록 변경했습니다.  
- 그 결과 확장성과 유지보수성이 크게 향상되었습니다.  

    
2️⃣ 퀘스트 클래스 세분화  
<p align="center"> <img src="https://github.com/user-attachments/assets/8aa3e594-541b-4c16-bd03-502d9ce9cd33" width="48%"> <img src="https://github.com/user-attachments/assets/85f85828-4dd9-4979-8859-9a65baea2f8c" width="48%"> </p>  
  
- 초기에는 단일 Quest 클래스가 모든 퀘스트 데이터를 관리했습니다.    
- 이로 인해 조건 검사, 보상 처리, UI 출력 등 다양한 책임이 한 클래스에 집중되어 있었습니다.  
  
- 퀘스트의 양이 많아질수록 관리가 어려워졌고,  
- 코드 중복과 가독성 저하 문제도 발생했습니다.  
  
- 이를 해결하기 위해 Quest, QuestUI, QuestManager로 클래스를 분리하여  
- 각 역할에 맞게 책임을 나누었고, 구조가 명확해지며 유지보수성이 향상되었습니다.  

    
3️⃣ 재귀 호출: 상태 기반 전투 흐름의 구조적 개선  
<p align="center"> <img src="https://github.com/user-attachments/assets/bd198891-316d-4176-b7db-0a1430df3677" width="48%"> <img src="https://github.com/user-attachments/assets/37396ca3-c291-4fc1-985c-916b05b8d98f" width="48%"> </p>  
  
- 기존 전투 흐름은 MainUI → EncounterUI → Result → MainUI 구조에서  
- 각 메서드가 서로를 재귀적으로 호출하는 방식이었습니다.  
  
- 이 방식은 반복 전투 시 스택이 계속 쌓이는 문제를 유발했고,  
- 장시간 플레이 시 성능 저하나 스택 오버플로우 위험이 있었습니다.  
  
- 이를 해결하기 위해 BattleState enum과 while-switch 구조를 도입하여  
- 전투 상태를 명확히 구분하고, 반복 호출 없이 안전하게 흐름을 유지할 수 있도록 개선했습니다.  

    
4️⃣ 전투 체력 추적 구조 개선: a = b, b = c, c = a
<p align="center"> <img src="https://github.com/user-attachments/assets/bb036cc4-371a-4469-b714-e9b67ef8f1d6" width="48%"> <img src="https://github.com/user-attachments/assets/a52db704-e1c6-47e2-9e70-c082bc6ffc41" width="48%"> </p>  
  
- 전투 중 HP 변화가 콘솔에 명확히 출력되지 않거나,  
- 결과 화면에서 직관적으로 보이지 않는 문제가 있었습니다.  
  
- 이는 체력 값을 하나의 변수로만 관리하면서,  
- 전/중/후 상태를 명확히 구분하지 않았기 때문입니다.  
  
- 이에 따라 체력을 a = b, b = c, c = a 구조로 나눠  
- 전투 전/중/후 상태를 각각 추적할 수 있도록 개선하였습니다.  
- 결과적으로 체력 변화가 명확하게 시각화되었고,  
- 전투 흐름 이해도도 높아졌습니다.  

    
5️⃣ 아이템 효과 구조의 한계
<p align="center"> <img src="https://github.com/user-attachments/assets/991755ea-41c7-4745-b09e-0ec7b18899b5" width="48%"> <img src="https://github.com/user-attachments/assets/e4c29080-0ec2-4c67-aaea-17ee77ef84a7" width="48%"> </p>  
  
- 기존에는 장비 클래스 내에서 아이템 효과를 개별 변수로 선언해 관리했습니다.  
- 이 방식은 새로운 효과가 생길 때마다 변수를 추가해야 했고,  
- 하나의 아이템이 여러 효과를 가지는 데 어려움이 있었습니다.  
  
- 이 문제를 해결하기 위해 배열 기반 구조로 설계를 변경하고,  
- 부모 클래스인 Item이 효과 타입과 값을 배열로 관리하도록 리팩토링했습니다.  
  
- 이를 통해 하나의 아이템에 복수 효과를 적용할 수 있게 되었고,  
- 아이템 시스템의 확장성과 일관성 또한 개선되었습니다.  
