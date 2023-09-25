using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFX_Manager : MonoBehaviour
{
    public static VisualFX_Manager instance { get; private set; }

    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] ParticleSystem fireVFX;

    private PlayerController playerScript;

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

    public void PlayExplosionVFX(Vector3 position)
    {
        if (explosionVFX != null)
        {
            ParticleSystem instance = Instantiate(explosionVFX, position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayHitEffect(Vector3 position)
    {
        if (hitVFX != null)
        {
            position.z = -7.0f;
            ParticleSystem instance = Instantiate(hitVFX, position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    public void PlayFireEffect(Vector3 position, PlayerController _playerScript)
    {
        if (fireVFX != null)
        {
            playerScript = _playerScript;
            ParticleSystem instance = Instantiate(fireVFX, position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            InvokeRepeating("SetInstance_Position", Time.deltaTime, Time.deltaTime);
        }
    }

    private void SetInstance_Position()
    {
        if (playerScript == null)
        {
            CancelInvoke();
        }
        else
        {
            GameObject temp = GameObject.Find("WFX_FlameThrower Looped(Clone)");
            temp.transform.position = playerScript.GetPlayer_Position();
            //instance.gameObject.transform.position = playerScript.GetPlayer_Position();
        }

    }
}


