using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BC_MusicManager : MonoBehaviour
{
    public static BC_MusicManager instance { get; private set; }

    [SerializeField] AudioClip defaultMusic;
    [SerializeField] AudioClip ingameMusic;
    private AudioSource track01, track02;
    private bool isPlayingTrack01;

    [SerializeField] Slider volumeSlider;
    public float currentVolume_Value {get; private set;}

    private GameObject mainCamera;

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

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject;
        DontDestroyOnLoad(mainCamera);

        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
        track01.loop = true;
        track02.loop = true;

        isPlayingTrack01 = true;
        UpdateVolume();
        SwapTrack(defaultMusic);
    }

    public void UpdateVolume()
    {
        currentVolume_Value = volumeSlider.GetComponent<Slider>().value;
        track01.volume = currentVolume_Value;
        track02.volume = currentVolume_Value;
    }

    public void SwapTrack(AudioClip newClip)
    { 
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));
        isPlayingTrack01 = !isPlayingTrack01;
    }

    public void IngameMusic()
    {
        SwapTrack(ingameMusic);
    }

    private IEnumerator FadeTrack(AudioClip newClip)
    {
        float timeToFade = 0.5f;
        float timeElapsed = 0;
        if (isPlayingTrack01)
        {
            track02.clip = newClip;
            track02.Play();

            while (timeElapsed < timeToFade)
            {
                track02.volume = Mathf.Lerp(0, currentVolume_Value, timeElapsed / timeToFade);
                track01.volume = Mathf.Lerp(currentVolume_Value, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track01.Stop();
        }
        else
        {
            track01.clip = newClip;
            track01.Play();
            while (timeElapsed < timeToFade)
            {
                track01.volume = Mathf.Lerp(0, currentVolume_Value, timeElapsed / timeToFade);
                track02.volume = Mathf.Lerp(currentVolume_Value, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            track02.Stop();
        }

    }
}
