using UnityEngine;

public class Door : MonoBehaviour, IInteraction
{
    public Animator anim;

    [ContextMenu("Interact door")]
    public void Interaction() 
    {
        anim.SetTrigger("Interact");
    }

    public void Interact()
    {
        anim.SetTrigger("Interact");
    }
}
