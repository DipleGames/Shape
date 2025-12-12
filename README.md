# SHAPE
# 유니티 1인 개발 프로젝트

<img width="1228" height="353" alt="image" src="https://github.com/user-attachments/assets/7ac3dbb4-1595-4767-85a2-2859f006b3d9" /><img width="1228" height="353" alt="image" src="https://github.com/user-attachments/assets/2a5c2559-e0ec-4f9a-a8bf-15286f0d86c5" />

# 1. 프로젝트 소개 
‘SHAPE는 도형의 모양만을 이용하여 만든 간단한 컨셉의 로그라이크 게임으로 스테이지를 돌파해가며 최종 스테이지를 격파하는것을 목표로하는 게임입니다..


***개발 기간***: 2025.11.07 ~ 2025.11.20

***개발 환경***: Engine: Unity 6000.0.61f1

***Language***: C#

***IDE***: Visual Studio Code

# 2. 팀원 소개 및 역할
### 황은중 - 기획

### 황은중 - 개발

# 프로젝트 구상

### 기획 소개
- 게임 제목 : SHAPE
- 게임 장르 : 로그라이크
- 플랫폼 : PC
- 게임 설명:
  1. 플레이어는 처음 캐릭터를 선택합니다.
  2. 플레이어는 몰려드는 적을 기본공격 및 스킬으로 제압하고 적이 드롭하는 아이템(힐팩, 경험치팩)등을 획득하며 성장합니다. 
  3. 적을 처치하면 분노게이지가 상승하게되고 분노게이지가 MAX상태가되면 보스전(Boss State)에 돌입합니다.
  4. 보스를 격파하고나면 준비단계(Prepare State)를 30초간 제공하고 그 시간동안 스킬 및 기본공격, 무기등을 업그레이드 할 수 있습니다.
  5. 이렇게 성장해나가며 최종단계 7스테이지를 격파하는 간단한 게임입니다.

  
### 참고 게임 (세피리아)
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
