using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    public Transform interactionTransfrom;

    private bool isFocus = false;

    private Transform player;

    private bool hasInteracted = false;

    public virtual void Interact()
    {

    }

    public virtual void InteractExit()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransfrom.position);
            if (distance <= radius)
            {
                Debug.Log("Interact");
                Interact();
                hasInteracted = true;
            }
            else
            {
                Debug.Log("Interact Exit");
                InteractExit();
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

}

// https://www.youtube.com/watch?v=9tePzyL6dgc
