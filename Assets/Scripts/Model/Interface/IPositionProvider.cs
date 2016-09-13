using UnityEngine;

namespace Model
{
    public interface IPositionProvider
    {
        Vector3 Position { get; }
    }
}
