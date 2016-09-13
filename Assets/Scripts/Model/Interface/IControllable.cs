using Control;
using UnityEngine;

namespace Model
{
    public interface IControllable : IPositionProvider
    {		
		void SetTarget(Vector3 targetPosition);
	    void SetTargetProvider(IControllableProvider provider);
    }
}
