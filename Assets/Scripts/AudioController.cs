using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public GameObject easyMusic;
    public GameObject hardMusic;
    public GameObject finalMusic;

    public void EasyMusicPlay()
    {
        finalMusic.GetComponent<AudioSource>().Stop();
        hardMusic.GetComponent<AudioSource>().Stop();
        easyMusic.GetComponent<AudioSource>().Play();
    }

    public void HardMusicPlay()
    {
        finalMusic.GetComponent<AudioSource>().Stop();
        easyMusic.GetComponent<AudioSource>().Stop();
        hardMusic.GetComponent<AudioSource>().Play();
    }

    public void FinalMusicPlay()
    {
        hardMusic.GetComponent<AudioSource>().Stop();
        easyMusic.GetComponent<AudioSource>().Stop();
        finalMusic.GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        EasyMusicPlay();
    }
}
