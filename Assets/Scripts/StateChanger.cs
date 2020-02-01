using UnityEngine;
using System.Collections.Generic;

public abstract class StateChanger : MonoBehaviour
{
    static List<StateChanger> elements = new List<StateChanger>();
    protected bool ShouldRevert { get; private set; }
    public StateChanger()
    {
        elements.Add(this);
    }
    public static void RevertToDefaultState()
    {
        foreach (StateChanger s in elements)
        {
            s.ShouldRevert = true;
        }
    }
    protected void CheckIfRevertRequested()
    {
        if (ShouldRevert)
        {
            ShouldRevert = false;
            RevertState();
        }
    }
    abstract protected void RevertState();
}
