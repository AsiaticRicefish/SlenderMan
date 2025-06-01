using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBGM : MonoBehaviour
{
    [SerializeField] private AudioSource MainMenuBgm;

    private void Start()
    {
        if (!MainMenuBgm.isPlaying)
        {
            MainMenuBgm.loop = true;
            MainMenuBgm.Play();
        }
    }
}
