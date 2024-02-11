using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] Sounds;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance ==null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach(Sound s in Sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch =s.pitch;
            s.source.loop=s.loop;
        }
    }

    // Update is called once per frame
    private void Start()
    {
       Play("BGM");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.source.Play();
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach(Sound s in Sounds)
        {
            s.source.Stop();
        }
    }

    public void setOrUnsetVolume() {
        foreach(Sound s in Sounds)
        {
            s.source.volume = s.source.volume == 0 ? s.volume : 0;;
        }
    }

    public void toggleBreathing() {
        if(Sounds[15].source.isPlaying) {
            Pause("Breathing");
        } else {
            Play("Breathing");
        }
    }
}
