using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepHandler : MonoBehaviour
{
    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioClip[] walkClips;
    [SerializeField] AudioClip[] runClips;

    [HideInInspector] public bool isRunning;

    public void OnFootstep()
    {
      if (footstepSource == null) return;

      AudioClip[] clipPool = isRunning ? runClips : walkClips;

      if (clipPool.Length == 0) return;

        int index = Random.Range(0, clipPool.Length);
        footstepSource.PlayOneShot(clipPool[index]);

    }
}
