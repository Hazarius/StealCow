using Control;
using Model;
using UnityEngine;

public class Dog : BaseModel, IControllableProvider
{    
	private readonly ControllableGroup _group = new ControllableGroup();

	protected override void InternalInit()
	{		
		//_group.SetTargetProvider(this);
		//_group.AddMember(this);
	}

	private void OnTriggerEnter2D(Collider2D triggeredCollider)
    {
        var cow = triggeredCollider.gameObject.GetComponent<Cow>();
        if (cow != null && !cow.IsControlled)
        {
// 	        if (!_group.IsContainMember(cow)) // group behavior
// 	        {
// 		        _group.AddMember(cow);
// 	        }
			cow.SetTargetProvider(this); // following behavior
        }
    }

	public void FreeControllableMember(IControllable controllable)
	{
		_group.RemoveMember(controllable);
	}

	protected override void ClearInternal()
	{
		_group.Clear();
		base.ClearInternal();
	}

	public IControllable Controllable
	{
		get
		{
			if (_group.MembersCount > 0) return _group;
			return this;
		}
	}
}