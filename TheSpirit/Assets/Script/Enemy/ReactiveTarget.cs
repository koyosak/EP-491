using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{


    
    private Quaternion _initRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion _endRotation = Quaternion.Euler(-75, 0, 0);

    // This code will be triggered once the entity has been shot.
    public void ReactToHit()
    {
        // Switch AI off to prevent entity from continuing to move.
        Destroy(gameObject);
    }
}
