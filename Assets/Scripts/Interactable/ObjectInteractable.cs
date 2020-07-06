using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    public virtual void WhenPlayerInteracts()
    {
        print("base interaction");
    }

    // all my gameojects are named like Rock0 Stick1 Rock3(Clone)
    // this emthod is supposed to read jsut the first word

    protected string ReadGameObjectName()
    {
        var name = gameObject.name;
        var hasCloneInName = false;
        string finalName;

        if (name.Length > 10)
        {
            var partName = name.Substring(name.Length - 7);

            if (partName == "(Clone)")
                hasCloneInName = true;
        }
        if (!hasCloneInName)
            finalName = gameObject.name.Substring(0, gameObject.name.Length - 1);
        else
            finalName = gameObject.name.Substring(0, gameObject.name.Length - 8);

        return finalName;
    }
}