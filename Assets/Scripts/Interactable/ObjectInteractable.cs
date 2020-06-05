using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    public virtual void WhenPlayerInteracts()
    {
        print("base interaction");
    }
}