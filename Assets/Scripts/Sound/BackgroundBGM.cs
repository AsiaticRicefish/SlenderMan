using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBGM : MonoBehaviour
{
    [SerializeField] private AudioSource BackgroundSound;

    private void Start()
    {
        if (!BackgroundSound.isPlaying)
        {
            BackgroundSound.loop = true;
            BackgroundSound.Play();
        }
    }
}
