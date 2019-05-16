using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioMixerSnapshot lvl;
    public AudioMixerSnapshot bossLvl;
    public AudioMixerSnapshot youWin;
    public AudioMixerSnapshot youLose;
    public AudioMixerSnapshot newSkill;
    public AudioMixerSnapshot final;

    public void Snapshots(string currentEvent) {
        switch (currentEvent) {
            case "youwin":    youWin.TransitionTo(0);   break;
            case "youlose":   youLose.TransitionTo(0);  break;
            case "bossMusic": bossLvl.TransitionTo(1);  break;
            case "bossdead":  lvl.TransitionTo(2);      break;
            case "newskill":  newSkill.TransitionTo(4); break;
            case "final":     final.TransitionTo(0);    break;
        }
    }

    public AudioMixer mixerLvl1;
    public LevelManager lvlManager;
    AudioSource AudSrc;
    void Start() {
        lvl.TransitionTo(0);
        lvlManager = GameObject.Find("Managers").GetComponent<LevelManager>();
        AudSrc = GetComponent<AudioSource>();
    }

    public bool playerHasDied;
    bool PlayerScream;
    void Update() {

        if (playerHasDied) {
            if (!PlayerScream) {
                AudSrc.PlayOneShot(sfx[0]);
                PlayerScream = true;
            }
        }

        Snapshots(null);
        if (lvlManager.lvlOnScene == 4) final.TransitionTo(0);
    }

    public List<AudioClip> sfx = new List<AudioClip>();
    public AudioClip GetSFX(int sound) {
        return sfx[sound];
    }
}
