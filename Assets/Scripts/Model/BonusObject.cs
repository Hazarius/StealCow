using Control;
using UnityEngine;

namespace Model
{
	public class BonusObject : BaseModel
	{
		public float TimeBonus { get; private set; }
		private float _internalTimer;
		public void Init(Vector3 position, float timeBonus)
		{
			TimeBonus = timeBonus;
			transform.position = position;
			IsEnabled = true;			
		}

		protected override void UpdateObjectInternal(float dt)
		{
			if (_internalTimer < Constants.BonusObjectLiveTime)
			{
				_internalTimer += dt;
			}
			if (_internalTimer >= Constants.BonusObjectLiveTime)
			{
				Retain();
			}
			base.UpdateObjectInternal(dt);
		}

		private void OnTriggerEnter2D(Collider2D triggeredCollider)
		{
			var dog = triggeredCollider.gameObject.GetComponent<Dog>();
			if (dog!= null)
			{
				GameLogicController.Instance.ApplyBonus(this);
				Retain();
			}
		}

		protected override void ClearInternal()
		{
			_internalTimer = 0f;
			base.ClearInternal();
		}
	}
}
