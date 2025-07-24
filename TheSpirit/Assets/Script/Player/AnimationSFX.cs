using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSFX : MonoBehaviour
{
    public AK.Wwise.Event fs;
    

    public void PlayFootstep()
    {
        fs.Post(gameObject);
    }
}
