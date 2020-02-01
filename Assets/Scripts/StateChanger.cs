using UnityEngine;
using System.Collections.Generic;

public abstract class StateChanger : MonoBehaviour
{
    static List<StateChanger> elements = new List<StateChanger>();
    public StateChanger()
    {
        elements.Add(this);
    }
    public static void RevertToDefaultState()
    {
        foreach (StateChanger s in elements)
        {
            s.Invoke("RevertState", 0.0f);
        }
    }
    abstract protected void RevertState();
}
