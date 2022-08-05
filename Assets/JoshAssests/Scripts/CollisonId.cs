using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This scrit should be attached to the player and checks for collsiosns
public class CollisonId : MonoBehaviour
{
    [SerializeField] private List<string> obsticleTags; // contines the tags for all obsticles which the player must avoid

    public UnityEvent OnObsticleHit; // Invoked when the player hits and obsticles

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        string collsionTag = collision.transform.tag;
        Debug.Log("hit" + collsionTag);
        if (obsticleTags.Contains(collsionTag))
        {
            Debug.Log("gg");
            OnObsticleHit?.Invoke();
        }

    }

}
