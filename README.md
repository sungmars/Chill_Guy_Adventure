# 👨‍🔧 Chill Guy's의 모험
### [7조] Chill조

![image](/README/team7.png)  
### **팀장 - 박진우**  
### 팀원 - 이성재  
### 팀원 - 송원석  
### 팀원 - 임석준  
### 팀원 - 김효준  

<br>

# 🎮 게임 실행 화면 프리뷰
![alt text](/README/선택.gif)


# 📂 프로젝트 구조
```bash
┣ 📂PersonalWorks
┃ ┣ 📂I_Script
┃ ┃ ┣ 📜AnimationHandler.cs
┃ ┃ ┣ 📜BaseController.cs
┃ ┃ ┣ 📜PlayerController.cs
┃ ┃ ┣ 📜ProjectileController.cs
┃ ┃ ┣ 📜ProjectileManager.cs
┃ ┃ ┣ 📜RangeWeaponHandler.cs
┃ ┃ ┣ 📜WeaponHandler.cs
┃ ┣ 📂K_Script
┃ ┣ 📂L_Script
┃ ┃ ┣ 📂Audio
┃ ┃ ┃ ┣ 📜AudioManager.cs
┃ ┃ ┃ ┣ 📜BGMController.cs
┃ ┃ ┃ ┣ 📜BossAudioController.cs
┃ ┃ ┃ ┣ 📜EnemyAudioController.cs
┃ ┃ ┃ ┣ 📜PlayerAudioController.cs
┃ ┃ ┣ 📂Enemy
┃ ┃ ┃ ┣ 📜AssassinEnemy.cs
┃ ┃ ┃ ┣ 📜EnemyController.cs
┃ ┃ ┃ ┣ 📜EnemyManager.cs
┃ ┃ ┃ ┣ 📜EnemyProjectile.cs
┃ ┃ ┃ ┣ 📜MeleeEnemy.cs
┃ ┃ ┃ ┣ 📜RangedEnemy.cs
┃ ┃ ┣ 📂Setting
┃ ┃ ┃ ┣ 📜Setting.cs
┃ ┃ ┣ 📂StartScene
┃ ┃ ┃ ┣ 📜RandomPNG.cs
┃ ┃ ┃ ┣ 📜StartSceneManager.cs
┃ ┣ 📂P_Script
┃ ┃ ┣ 📂Entity
┃ ┃ ┃ ┣ 📜EndingScenePlayerObject.cs
┃ ┃ ┃ ┣ 📜EnemySpawnObject.cs
┃ ┃ ┣ 📂Helper
┃ ┃ ┃ ┣ 📜ExtenstionUnilty.cs
┃ ┃ ┃ ┣ 📜MonoSingleton.cs
┃ ┃ ┃ ┣ 📜MonoSingletonDontDestroy.cs
┃ ┃ ┣ 📂Manager
┃ ┃ ┃ ┣ 📜EnemySpawnManager.cs
┃ ┃ ┃ ┣ 📜GameManager.cs
┃ ┃ ┃ ┣ 📜ParticleManager.cs
┃ ┃ ┃ ┣ 📜SkillManager.cs
┃ ┃ ┣ 📂ScriptableObject
┃ ┃ ┃ ┣ 📜EnemySpawnData.cs
┃ ┃ ┣ 📂Skill
┃ ┃ ┃ ┣ 📂Entity
┃ ┃ ┃ ┃ ┣ 📜AreaSkillObject.cs
┃ ┃ ┃ ┃ ┣ 📜BuffSkillObject.cs
┃ ┃ ┃ ┃ ┣ 📜RangeSkillObject.cs
┃ ┃ ┃ ┣ 📂Handler
┃ ┃ ┃ ┃ ┣ 📜AreaSkillHandler.cs
┃ ┃ ┃ ┃ ┣ 📜BuffSkillHandler.cs
┃ ┃ ┃ ┃ ┣ 📜RangeSkillHandler.cs
┃ ┃ ┃ ┃ ┣ 📜SkillHandler.cs
┃ ┃ ┣ 📂UI
┃ ┃ ┃ ┣ 📜GetSkillUI.cs
┃ ┃ ┃ ┣ 📜PlayerSelectOptionUI.cs
┃ ┃ ┃ ┣ 📜SkillUI.cs
┃ ┣ 📂S_Script
┃ ┃ ┣ 📂BossScripts
┃ ┃ ┃ ┣ 📜BossColliderController.cs
┃ ┃ ┃ ┣ 📜BossController.cs
┃ ┃ ┃ ┣ 📜BossManager.cs
┃ ┃ ┣ 📂BossSkillsScripts
┃ ┃ ┃ ┣ 📜ChillAttackAnimController.cs
┃ ┃ ┃ ┣ 📜ChillAttackCollisonController.cs
┃ ┃ ┃ ┣ 📜ChillAttackController.cs
┃ ┃ ┃ ┣ 📜ChillDogAttackController.cs
┃ ┃ ┃ ┣ 📜KeyBoardAttackController.cs
┃ ┃ ┃ ┣ 📜RushAttackScripts.cs
┃ ┃ ┃ ┣ 📜SoundWaveAttackController.cs
┃ ┃ ┣ 📂EndingScripts
┃ ┃ ┃ ┣ 📜EndingSceneManager.cs
┃ ┃ ┣ 📂IntroScripts
┃ ┃ ┃ ┣ 📜IntroBossController.cs
┃ ┃ ┃ ┣ 📜IntroCameraController.cs
┃ ┃ ┃ ┣ 📜IntroManager.cs
┃ ┃ ┣ 📜BossHpUI.cs
┃ ┃ ┣ 📜GameOverManager.cs
```

<br>

# 🎮 게임 플레이 가이드
- WASD : 이동
- F, G : 근접 무기, 원거리 무기
- 마우스 왼쪽, 오른쪽 버튼 : 스킬 1, 스킬 2

# ⚙ 주요 시스템
### 🏹 전투 시스템

플레이어를 향해 돌격해오는 몬스터들  
플레이어는 자동으로 다가오는 적들을 공격  
마우스 클릭에 따라 다른 스킬 사용  

### 🏰 보스 시스템

랜덤한 보스 스킬 공격  
1. 돌진 공격 - 보스가 플레이어를 향해 돌진
2. 음파 공격 - 보스가 플레이어를 향해 점점 커지는 음파를 발사
3. 석상 공격 - 현재 플레이어의 위치와 랜덤한 위치에 떨어지는 석상 소환
4. 보조 몬스터 소환 - 보스를 보조하는 몬스터를 소환해 플레이어를 공격
5. 키보드 입력 공격 - 플레이어에게 스턴을 먹이고 시간내에 주어진 입력을 해야하는 공격

### 🎒 강화 시스템

스테이지 클리어시 클릭에 따른 다양한 스킬 선택  
원하는 무기 업그레이드  

### 🏟️ 스테이지 시스템
스테이지에 따른 다양한 몬스터들 생성  
스테이지 내 모든 몹 처치시 다음 스테이지 이동  
모든 스테이지 클리어시 보스 스테이지 이동  
보스 스테이지 클리어시 끝  

### 🕹️ 캐릭터 선택 시스템
기본 캐릭터 해금  
1회 게임 클리어시 새로운 캐릭터 선택 가능  

<br>



# 💻 씬 구조
### 메인화면
![alt text](/README/스타트씬.png)  
Chill한 메인이 5초간격으로 바뀝니다.

### 캐릭터 선택
![alt text](/README/캐릭터선택.png)
2개의 캐릭터중 하나를 선택해 사용할 수 있습니다.(오른쪽 캐릭터는 클리어 시 사용가능합니다.)

### 설정
![alt text](/README/설정.png)
브금 및 효과 사운드 볼륨을 조절할 수 있습니다.

### 웨이브 시작을 알림
![alt text](/README/게임스타트1.png)
스테이지 진입시 1웨이브 부터 3웨이브 까지 진행 됩니다.

### 몹 소환중
![alt text](/README/게임스타트2.png)  
곧 몬스터가 나올 공간에 타이머가 표시됩니다.

### 스킬 및 기본공격을 이용한 전투
![alt text](/README/게임스타트3.png)  
F키와 G키로 무기를 바꾸고 왼쪽,오른쪽 마우스클릭으로 스킬을 이용하여 몬스터들을 물리칩니다.

### 스테이지 클리어
![alt text](/README/스킬선택.png)  
모든 웨이브가 종료된 뒤에 스킬을 선택 할 수 있습니다.

## 🏹 보스와의 전투
### 1. 돌진
![alt text](/README/돌진_공격.gif)  
Chill Guy가 뒤로한번 이동한 뒤 돌진합니다.

### 2. 음파
![alt text](/README/음파공격.gif)  
Chill한 브금과 함께 음파공격을 합니다.

### 3. 칠가이 석상 떨구기
![alt text](/README/석상떨구기.gif)  
Chill한 석상들이 떨어집니다.

### 4. 소규모 칠도그
![alt text](/README/개들을_풀어라.gif)  
Chill한 강아지들을 소환하여 공격합니다.

### 5. 캐릭터 스턴넣기
![alt text](/README/비아키스패턴.gif)  
키보드를 패턴에 맞게 빨리 풀어야 합니다

## 엔딩
![image](/README/엔딩.gif)  
기뻐하는 캐릭터가 Chill하게 변하면서 엔딩이 나옵니다.