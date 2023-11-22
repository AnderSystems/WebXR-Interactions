using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSystem : MonoBehaviour
{
    public int CurrentDialog;
    [Space]
    public AudioSource source;

    [System.Serializable]
    public class dialog
    {
        [Tooltip("Play this dialog automatically?")] public bool PlayAutomatially;
        public AudioClip clip;
        [TextArea()] public string Subtitle;
        public int NextClip = -1;
    }
    [SerializeField]
    public List<dialog> Dialogs = new List<dialog>();

    public void Play()
    {

    }

    public void Play(int dialog)
    {

    }

    public void Play(int dialog, bool wait)
    {

    }

    public void Play(AudioSource dialog)
    {

    }

    public void Play(AudioSource dialog, bool wait)
    {

    }

    public void Pause()
    {

    }

    public void Stop()
    {

    }

    public void PlayNext()
    {

    }

    public void RepeatCurrent()
    {

    }
}
