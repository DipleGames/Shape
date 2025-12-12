# SHAPE
# 유니티 1인 개발 프로젝트

<img width="2456" height="706" alt="image" src="https://github.com/user-attachments/assets/7ac3dbb4-1595-4767-85a2-2859f006b3d9" />
<img width="2456" height="706" alt="image" src="https://github.com/user-attachments/assets/2a5c2559-e0ec-4f9a-a8bf-15286f0d86c5" />

# 1. 프로젝트 소개 
‘프로젝트 AM은’왕국의 지휘관이 되어 더 강력한 병력을 만들어 승리를 쟁취하는 것을 목표로 합니다. 건물을 배치하여 재화를 획득하고 유닛을 생산할 수 있습니다. 건물을 머지·강화하여 더 강력한 유닛을 생산하고 적의 넥서스를 파괴하는 전략형 오토배틀러 게임입니다.


***개발 기간***: 2025.12.01 ~ 2025.12.04

***개발 환경***: Engine: Unity 2022.3.62f2

***Language***: C#

***IDE***: Visual Studio, Rider

# 2. 팀원 소개 및 역할
### 임승연 - 기획
- 프로젝트 기획 및 총괄

### 김종원 - 개발
- 개발 팀장
- 유닛 및 전투 시스템
- 맵 및 카메라

### 안건우 - 개발
- 건물 및 머지 시스템
- 인벤토리 및 Grid시스템

### 전규태 - 개발
- UIManager 및 Main,Title UI
- 씬 전환 및 로드

### 차동욱 - 개발
- Manager 관리 및 코어 시스템
- 미니맵 

# 프로젝트 구상

### 기획 소개
- 게임 제목 : (가제) 프로젝트 AM
- 게임 장르 : 오토배틀러 + 머지
- 플랫폼 : 모바일 (Android, iOS)
- 게임 설명:
  1. 플레이어는 5x5 그리드에 다양한 건물을 전략적으로 배치합니다.
  2. 유저는 재화를 소비하여 건물을 건설합니다. 건물은 재화와 유닛을 생산합니다.
  3. 건물은 머지 시스템을 통해 강력한 상위 건물로 업그레이드 할 수 있습니다.
  4. 생산된 유닛은 자동으로 공격합니다.
  5. 자원 순환 구조를 통해 자신만의 빌드를 구축하는 ‘오토배틀러 + 머지 빌더’ 형태의 게임입니다.
<img width="2048" height="1153" alt="image" src="https://github.com/user-attachments/assets/628539ce-b169-4098-8e2a-deff59fe346a" />

  
### 참고 게임 (스타크래프트: 데저트 스트라이크)
<img width="451" height="455" alt="image" src="https://github.com/user-attachments/assets/d9fb77f7-ac9c-4c66-abce-6c83da99627d" />

### 폴더 구조
<pre>
📦 Project-AM
└── 📂 Assets
    └── 📂 02_Scripts
        ├── 📜 Enums.cs
        ├── 📂 Building
        │   ├── 📜 BuildingComponent.cs
        │   ├── 📜 BuildingData.cs
        │   ├── 📜 BuildingEntity.cs
        │   ├── 📜 BuildingEvents.cs
        │   ├── 📜 BuildingPreviewComponent.cs
        │   └── 📂 Grid
        │       ├── 📜 BuildGridContainer.cs
        │       └── 📜 GridCell.cs
        ├── 📂 Data
        │   └── 📂 Audio
        │       ├── 📜 BGMData.cs
        │       └── 📜 SFXData.cs
        ├── 📂 Inventory
        │   ├── 📜 GachaBuilding.cs
        │   ├── 📜 InventoryComponent.cs
        │   ├── 📜 InventoryEvents.cs
        │   └── 📜 InventorySlot.cs
        ├── 📂 Manager
        │   ├── 📜 AudioManager.cs
        │   ├── 📜 EnemySpawnerDataManager.cs
        │   ├── 📜 GameManager.cs
        │   ├── 📜 NexusManager.cs
        │   ├── 📜 ResourceManager.cs
        │   ├── 📜 Singleton.cs
        │   ├── 📜 StageManager.cs
        │   ├── 📜 UIManager.cs
        │   └── 📜 UnitDataManager.cs
        ├── 📂 Map
        │   ├── 📜 ClampCamera.cs
        │   ├── 📜 Nexus.cs
        │   └── 📜 NexusHpBar.cs
        ├── 📂 UI
        │   ├── 📜 ButtonXSpeed.cs
        │   ├── 📜 CameraController.cs
        │   ├── 📜 NexusCondition.cs
        │   ├── 📜 TextMoney.cs
        │   └── 📜 TextTimer.cs
        ├── 📂 Unit
        │   ├── 📜 EnemySpawner.cs
        │   ├── 📜 EnemySpawnerData.cs
        │   ├── 📜 EnemySpawnerDataJson.cs
        │   ├── 📜 EnemyUnit.cs
        │   ├── 📜 MapBoundary.cs
        │   ├── 📜 PlayerSpawner.cs
        │   ├── 📜 PlayerUnit.cs
        │   ├── 📜 UnitBase.cs
        │   ├── 📜 UnitCombat.cs
        │   ├── 📜 UnitData.cs
        │   ├── 📜 UnitDataJson.cs
        │   ├── 📜 UnitHPVisual.cs
        │   ├── 📜 UnitMovement.cs
        │   └── 📜 UnitSpawner.cs
        └── 📂 Utils
            └── 📜 Extensions.cs
</pre>

### 게임 구조도
<img width="9366" height="4774" alt="image" src="https://github.com/user-attachments/assets/4a65c717-7cc3-42c5-9ce4-857851cb35b6" />
