using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundFX_Manager : MonoBehaviour
{
    public static soundFX_Manager instance { get; private set; }

    [Header("Shooting SVX")]
    [SerializeField] AudioClip shootingBullet_Clip;
    [SerializeField] AudioClip shootingRocket_Clip;
    //[SerializeField] AudioClip shootingBullet_Clip_Enemy;

    [Header("Bullet Collision")]
    [SerializeField] AudioClip bulletCollision_Clip;

    [Header("Rocket Collusion")]
    [SerializeField] AudioClip rocketCollision_Clip;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayShootingBullet_Clip(string tag)
    {
        if (CompareTag("Player"))
        {
            PlayClip(shootingBullet_Clip, BC_MusicManager.instance.currentVolume_Value);
        }
        else
        {
            PlayClip(shootingBullet_Clip, BC_MusicManager.instance.currentVolume_Value / 2);
        }
    }

    public void PlayShootingRocket_Clip()
    {
        PlayClip(shootingRocket_Clip, BC_MusicManager.instance.currentVolume_Value * 2.5f);
    }

    public void PlayBulletCollision_Clip()
    {
        PlayClip(bulletCollision_Clip, BC_MusicManager.instance.currentVolume_Value / 2);
    }

    public void PlayRocketCollision_Clip()
    {
        PlayClip(rocketCollision_Clip, BC_MusicManager.instance.currentVolume_Value * 2.5f);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
        }
    }
    
}
