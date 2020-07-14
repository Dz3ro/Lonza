using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectInteractiveWithTool : MonoBehaviour
{
    public abstract void OnToolUse(Item item);
    
}
