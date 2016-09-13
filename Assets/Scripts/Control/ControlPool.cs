using System.Collections.Generic;
using Model;

public class ControlPool
{
    public readonly List<IControllable> controllables;

    public ControlPool()
    {
        controllables = new List<IControllable>();
    }

    public void AddControllable(IControllable controllable)
    {
        if (!controllables.Contains(controllable))
        {
            controllables.Add(controllable);
        }
    }

    public void RemoveControllable(IControllable controllable)
    {
        if (controllables.Contains(controllable))
        {
            controllables.Remove(controllable);
        }
    }

    public void Clear()
    {
        controllables.Clear();        
    }
}