using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    public int channelIndex; //마지막으로 실행된 sfxPlayer의 인덱스
    AudioSource[] sfxPlayers;

    public AudioClip walkClip;
    public float walkVolume;
    public AudioSource walkPlayer;


    public enum Bgm { Menu, Mekka, Jerusalem }
    public enum Sfx { bone, fireballSpell, fireballCrack, LevelUp, Sanshir, StageFail, UiButton, Walk}

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        GameObject walkObject = new GameObject("walkPlayer");
        walkObject.transform.parent = transform;
        walkPlayer = walkObject.AddComponent<AudioSource>();
        walkPlayer.clip = walkClip;
        Init();
        PlayBgm(Bgm.Menu);
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
        }

        GameObject walkObject = new GameObject("walkPlayer");
        walkObject.transform.parent = transform;
        walkPlayer = walkObject.AddComponent<AudioSource>();
        walkPlayer.playOnAwake = false;
        walkPlayer.volume = walkVolume;


    }

    public void PlayBgm(Bgm bgm)
    {
        bgmPlayer.clip = bgmClip[(int)bgm];
        bgmPlayer.volume = 0.5f;
        bgmPlayer.Play();
    }

    public void StopBgm()
    {
        bgmPlayer.Stop();
    }
    public void PlaySfx(Sfx sfx)
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) continue;

            channelIndex = loopIndex;

            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();


            return;
        }

    }


    public void StopSfx()
    {
        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i].Stop();
        }
    }
}
