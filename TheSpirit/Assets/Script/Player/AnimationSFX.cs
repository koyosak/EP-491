using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSFX : MonoBehaviour
{
    public AK.Wwise.Event fs;
    public AK.Wwise.Event punch;
    public AK.Wwise.Event sprint;

    public void PlayFootstep()
    {
        fs.Post(gameObject);
    }
    public void PlayPunch()
    {
        punch.Post(gameObject);
    }

    public void PlaySprint()
    {
        sprint.Post(gameObject);
    }
}
