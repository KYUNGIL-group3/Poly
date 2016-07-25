using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    static AudioManager _instance = null;
    //32개
    /*
    1.게임 실행 후 Main UI 씬 출력 시, 재생 스테이지 입장 시 재생 종료
    2.스테이지 입장 후 재생
    3.보스 등장 시 재생
    4.Main UI 씬에서 Start 터치 시 재생
    5.각 스테이지 입장 후 시작 위치에 캐릭터 스폰 시 재생
    6.각 메뉴 UI 버튼 터치 시 재생
    7.캐릭터 이동 시, 걷는 애니메이션에 맞춰 재생
    8.워프 포탈 이용 시 재생
    9.낙하 이동존 밟은 뒤 뛰어내릴 때 재생
    10.착지 지점에 착지할 때 재생
    11.무기를 휘두를 때 재생
    12.무기로 적 타격 시 재생
    13.PC가 적에게 피격 시 재생(일부 몬스터 별로 상이)
    14.3타로 충격파 발생 시(지면 강타 시) 재생
    15.화살이 날아갈 때 재생
    16.화살이 오브젝트, 몬스터와 충돌할 때 재생
    17.무기 교체 시 재생
    18.타임 컨트롤 스킬 사용 시 재생
    19.스킬 경로를 설정할 때, 연결되는 점 하나가 추가될 때마다 재생
    20.스킬 경로 설정 후 스킬 발동 시에 재생
    21.스킬 발동 시에 적 타격 시 재생
    22.몬스터 스폰 시 재생
    23.적 몬스터 처치 시, 파편들로 깨질 때 재생
    24.적 몬스터 처치 후, 파편 흡수 시 재생
    25.웨이브 차단막 제거될 때 재생
    26.Enjoy 스테이지처럼 벽 사라질 때 재생
    27.스테이지 클리어 화면 출력 시 재생
    28.스테이지 실패 화면 출력 시 재생
    29.PC 사망 시, 파티클이 깨질 때 재생
    30.회복 오브젝트 드랍 시 재생
    31.회복 오브젝트 획득 시 재생
    32.캐릭터 부활 시 재생
    */
    public AudioClip MainUISound;   //1
    public AudioClip StageSound;    //2
    public AudioClip BossSound;     //3
    public AudioClip StartTouchSound;   //4 적용
    public AudioClip PlayerSpawnSound;  //5
    public AudioClip UIButtonTouchSound;    //6 적용
    public AudioClip PlayerMoveSound;   //7
    public AudioClip WarpPotalSound;    //8
    public AudioClip FallSound; //9
    public AudioClip LandSound; //10
    public AudioClip WeaponSwingSound;  //11
    public AudioClip WeaponHitSound;    //12    //적용
    public AudioClip PlayerHitSound;    //13    //적용
    public AudioClip ShockWaveSound;    //14
    public AudioClip ArrowShotSound;    //15    //적용
    public AudioClip ArrowHitSound;     //16    //적용
    public AudioClip WeaponTransSound;  //17    //적용
    public AudioClip TimeSkillOnSound;  //18
    public AudioClip SkillPointSound;   //19
    public AudioClip SkillActiveSound;  //20
    public AudioClip SkillHitSound;     //21
    public AudioClip MonsterSpawnSound; //22    //적용
    public AudioClip FragmentBrokenSound;   //23    
    public AudioClip FragmentAbsorbSound;   //24    //적용
    public AudioClip WaveWallRemoveSound;   //25
    public AudioClip WallRemoveSound;   //26
    public AudioClip StageClearSound;   //27
    public AudioClip StageFailSound;    //28
    public AudioClip PlayerFragmentBrokenSound; //29
    public AudioClip RecoveryItemDropSound; //30
    public AudioClip RecoveryItemTakeSound; //31    //적용
    public AudioClip PlayerRevivalSound;    //32

    public static AudioManager Instance()
    {
        return _instance;
    }
    // Use this for initialization
    void Start () {
		if(_instance==null)
        {
            _instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void PlayMainUISound()
    {
        GetComponent<AudioSource>().PlayOneShot(MainUISound);
    }
    public void PlayStageSound()
    {
        GetComponent<AudioSource>().PlayOneShot(StageSound);
    }
    public void PlayBossSound()
    {
        GetComponent<AudioSource>().PlayOneShot(BossSound);
    }
    public void PlayStartTouchSound()
    {
        GetComponent<AudioSource>().PlayOneShot(StartTouchSound);
    }
    public void PlayPlayerSpawnSound()
    {
        GetComponent<AudioSource>().PlayOneShot(PlayerSpawnSound);
    }
    public void PlayUIButtonTouchSound()
    {
        GetComponent<AudioSource>().PlayOneShot(UIButtonTouchSound);
    }
    public void PlayPlayerMoveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(PlayerMoveSound);
    }
    public void PlayWarpPotalSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WarpPotalSound);
    }
    public void PlayFallSound()
    {
        GetComponent<AudioSource>().PlayOneShot(FallSound);
    }
    public void PlayLandSound()
    {
        GetComponent<AudioSource>().PlayOneShot(LandSound);
    }
    public void PlayWeaponSwingSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WeaponSwingSound);
    }
    public void PlayWeaponHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WeaponHitSound);
    }
    public void PlayPlayerHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(PlayerHitSound);
    }
    public void PlayShockWaveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(ShockWaveSound);
    }
    public void PlayArrowShotSound()
    {
        GetComponent<AudioSource>().PlayOneShot(ArrowShotSound);
    }
    public void PlayArrowHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(ArrowHitSound);
    }
    public void PlayWeaponTransSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WeaponTransSound);
    }
    public void PlayTimeSkillOnSound()
    {
        GetComponent<AudioSource>().PlayOneShot(TimeSkillOnSound);
    }
    public void PlaySkillPointSound()
    {
        GetComponent<AudioSource>().PlayOneShot(SkillPointSound);
    }
    public void PlaySkillActiveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(SkillActiveSound);
    }
    public void PlaySkillHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(SkillHitSound);
    }
    public void PlayMonsterSpawnSound()
    {
        GetComponent<AudioSource>().PlayOneShot(MonsterSpawnSound);
    }
    public void PlayFragmentBrokenSound()
    {
        GetComponent<AudioSource>().PlayOneShot(FragmentBrokenSound);
    }
    public void PlayFragmentAbsorbSound()
    {
        GetComponent<AudioSource>().PlayOneShot(FragmentAbsorbSound);
    }
    public void PlayWaveWallRemoveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WaveWallRemoveSound);
    }
    public void PlayWallRemoveSound()
    {
        GetComponent<AudioSource>().PlayOneShot(WallRemoveSound);
    }
    public void PlayStageClearSound()
    {
        GetComponent<AudioSource>().PlayOneShot(StageClearSound);
    }
    public void PlayStageFailSound()
    {
        GetComponent<AudioSource>().PlayOneShot(StageFailSound);
    }
    public void PlayPlayerFragmentBrokenSound()
    {
        GetComponent<AudioSource>().PlayOneShot(PlayerFragmentBrokenSound);
    }
    public void PlayRecoveryItemDropSound()
    {
        GetComponent<AudioSource>().PlayOneShot(RecoveryItemDropSound);
    }
    public void PlayRecoveryItemTakeSound()
    {
        GetComponent<AudioSource>().PlayOneShot(RecoveryItemTakeSound);
    }
    public void PlayPlayerRevivalSound()
    {
        GetComponent<AudioSource>().PlayOneShot(PlayerRevivalSound);
    }


}
