using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This scrit should be attached to the player and checks for collsiosns
public class CollisonId : MonoBehaviour
{
    [SerializeField] private List<string> obsticleTags; // contines the tags for all obsticles which the player must avoid
    [SerializeField] private List<string> interactionTags;

    public UnityEvent OnObsticleHit; // Invoked when the player hits and obsticles

    private PlayerBounds bounds;

    private void Awake()
    {
        if(!(bounds = GetComponent<PlayerBounds>())) { Debug.LogWarning("No player bounds component on " + gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckTags(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckTags(collision);
    }

    private void CheckTags(Collider2D collider)
    {
        string collsionTag = collider.tag;
        if (obsticleTags.Contains(collsionTag))
        {
            OnObsticleHit?.Invoke();
        }
        else if (interactionTags.Contains(collsionTag))
        {
            Interaction action = collider.GetComponent<Interaction>();
            action.Interact(bounds);
        }
    }

}
