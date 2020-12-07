using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
        
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            //ordena os sons e cria AudioSources para quando o metodo Play for chamado; estes ja podem ter o volume e pitch ajustados antes mesmo do começo da cena
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        //Toca o som especificado quando o método é chamado
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
