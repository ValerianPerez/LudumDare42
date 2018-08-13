using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _Master;
    private AudioSource[] Slaves = new AudioSource[3];
    public float prevTime = 0.0f;
    public AudioClip _guitarClip, _bassClip, _elecG1Clip, _elecG2Clip;
    ResourceManager resManager;
    private bool bassPlay = false,
                guitar1Play = false,
                guitar2Play = false;

    
    void Start()
    {
        //GuitarLoop
        Debug.Log("Start started");
        _Master = gameObject.AddComponent<AudioSource>();
        _Master.clip = _guitarClip; ;
        _Master.loop = true;
        _Master.volume = 0.0f;
        _Master.Play();
        StartCoroutine(FadeIn(_Master));

        //BassLoop
        Slaves[0] = gameObject.AddComponent<AudioSource>();
        Slaves[0].clip = _bassClip;
        Slaves[0].loop = true;
        Slaves[0].volume = 0.0f;
        Slaves[0].Play();
        Slaves[0].timeSamples = _Master.timeSamples;

        //Elec Guitar1 Loop
        Slaves[1] = gameObject.AddComponent<AudioSource>();
        Slaves[1].clip = _elecG1Clip;
        Slaves[1].loop = true;
        Slaves[1].volume = 0.0f;
        Slaves[1].Play();
        Slaves[1].timeSamples = _Master.timeSamples;

        //Elec Guitar2 Loop
        Slaves[2] = gameObject.AddComponent<AudioSource>();
        Slaves[2].clip = _elecG2Clip;
        Slaves[2].loop = true;
        Slaves[2].volume = 0.0f;
        Slaves[2].Play();
        Slaves[2].timeSamples = _Master.timeSamples;

        resManager = ((ResourceManager)GetComponentInParent<ResourceManager>());
        Debug.Log("Start ended");
    }


    /// <summary>
    /// This function raise the bass loop's volume
    /// </summary>
    void launchBass()
    {
        Debug.Log("launChBass started");
        // Synchronization init
        float tempTime = _Master.timeSamples;
        prevTime = _Master.timeSamples + 2;

        bool done = false;
        while (!done)
        {
            tempTime = _Master.timeSamples;
            Debug.Log("Prev = " + prevTime.ToString() + " Temp = " + tempTime.ToString());
            if (prevTime > tempTime)
            {
                StartCoroutine(FadeIn(Slaves[0]));
                done = true;
            }
            prevTime = tempTime;
        }
        Debug.Log("LaunchBass ended");
    }

    /// <summary>
    /// Allows to play either the first guitar loop either the second one (<c>1</c> : Simple Riff, <c>2</c>: Fast Riff)
    /// </summary>
    /// <param name="fadeIn">AudioSource to fade in </param>
    /// <param name="fadeOut">AudioSource to fade out</param>
    void LaunchElecGuitar(int fadeIn, int fadeOut)
    {

        Debug.Log("LaunchGuitar started - " + fadeIn.ToString() + " - " + fadeOut.ToString());
        // Synchronization init
        float prevTimeElec = _Master.timeSamples;
        float tempTime = _Master.timeSamples + 2.0f;
        bool done = false;

        while (!done)
        {

            StartCoroutine(FadeIn(Slaves[fadeIn]));
            // Check if the other guitar loop is playing
            if (Slaves[fadeOut].volume != 0.0f)
                StartCoroutine(FadeOut(Slaves[fadeOut]));
            //Get out of this loop
            done = true;
        }
        Debug.Log("LaunchGuitar ended - " + fadeIn.ToString() + " - " + fadeOut.ToString());
    }


    /// <summary>
    /// Fade out given AudioSource
    /// </summary>
    /// <param name="audioSource"><c>Audiosource</c> to fade Out</param>
    /// <param name="minVolume">Volume level when the fade out is ending</param>
    /// <returns></returns>
    public static IEnumerator FadeOut(AudioSource audioSource, float minVolume = 0.0f)
    {
        Debug.Log("Fade out " + audioSource.clip.name + " started.");
        bool done = false;
        while (!done)
        {
            audioSource.volume -= 0.001f;
            if (audioSource.volume <= minVolume)
            {
                audioSource.volume = minVolume;
                done = true;
                Debug.Log("Fade out " + audioSource.clip.name + " ended.");
            }
        }

        yield return null;
    }

    /// <summary>
    /// Fade In given AudioSource
    /// </summary>
    /// <param name="audioSource"><c>Audiosource</c> to fade In</param>
    /// <param name="maxVolume">Volume level when the fade in is ending</param>
    /// <returns></returns>
    public static IEnumerator FadeIn(AudioSource audioSource, float maxVolume = 1.0f)
    {
        Debug.Log("Fade in " + audioSource.clip.name + " started.");
        bool done = false;
        while (!done)
        {
            audioSource.volume += 0.001f;
            if (audioSource.volume >= maxVolume)
            {
                audioSource.volume = maxVolume;
                done = true;
                Debug.Log("Fade in " + audioSource.clip.name + " ended.");
            }
        }
        yield return null;
    }


    // Update is called once per frame
    void Update()
    {
        if (resManager == null)
            resManager = (ResourceManager)this.GetComponentInParent<ResourceManager>();


        if (!bassPlay && resManager.FoodResource < 1450.0f)
        {
            launchBass();
            bassPlay = true;
        }

        if (!guitar1Play && resManager.FoodResource < 900.0f && resManager.FoodResource > 250.0f)
        {
            LaunchElecGuitar(1, 2);
            guitar1Play = true;
            guitar2Play = false;
        }

        if (!guitar2Play && resManager.FoodResource <= 250.0f)
        {
            LaunchElecGuitar(2, 1);
            guitar1Play = false;
            guitar2Play = true;
        }
    }
}
