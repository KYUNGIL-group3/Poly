using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    static AudioManager _instance = null;
    //32개
    public AudioClip MainUISound;
    public AudioClip StageSound;
    public AudioClip BossSound;
    public AudioClip StartTouchSound;
    public AudioClip PlayerSpawnSound;
    public AudioClip UIButtonTouchSound;
    public AudioClip PlayerMoveSound;
    public AudioClip WarpPotalSound;
    public AudioClip FallSound;
    public AudioClip LandSound;
    public AudioClip WeaponSwingSound;
    public AudioClip WeaponHitSound;
    public AudioClip PlayerHitSound;
    public AudioClip ShockWaveSound;
    public AudioClip ArrowShotSound;
    public AudioClip ArrowHitSound;
    public AudioClip WeaponTransSound;
    public AudioClip TimeSkillOnSound;
    public AudioClip SkillPointSound;
    public AudioClip SkillActiveSound;
    public AudioClip SkillHitSound;
    public AudioClip MonsterSpawnSound;
    public AudioClip FragmentBrokenSound;
    public AudioClip FragmentAbsorbSound;
    public AudioClip WaveWallRemoveSound;
    public AudioClip WallRemoveSound;
    public AudioClip StageClearSound;
    public AudioClip StageFailSound;
    public AudioClip PlayerFragmentBrokenSound;
    public AudioClip RecoveryItemDropSound;
    public AudioClip RecoveryItemTakeSound;
    public AudioClip PlayerRevivalSound;

    public static AudioManager Instance()
    {
        return _instance;
    }
    // Use this for initialization
    void Start () {
		
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
