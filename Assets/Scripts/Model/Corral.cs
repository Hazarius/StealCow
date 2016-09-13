using System;
using System.Collections.Generic;
using Control;
using UnityEngine;

namespace Model
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Corral : BaseModel, IControllableProvider
    {	    
	    private float _checkTimer;
	    private int cowCount;
	    private bool isCheckEnabled;

        private readonly List<IControllable> _controllables = new List<IControllable>();

		public IControllable Controllable
		{
			get { return this; }
		}

		public void Init(CorralInitialData data)
        {
			_controllables.Clear();
			_destination = transform.position = data.position;
            transform.localScale = data.size;
			IsEnabled = true;
			gameObject.SetActive(true);
		}

	    protected override void UpdateObjectInternal(float dt)
	    {
		    if (isCheckEnabled)
		    {
			    if (_checkTimer >= Constants.MaxCheckTime)
			    {
				    cowCount = 0;
					_checkTimer = 0;
					isCheckEnabled = false;
			    }
			    else
			    {
				    _checkTimer += dt;
			    }
		    }
	    }

	    private void OnTriggerEnter2D(Collider2D triggeredCollider)
        {
            var cow = triggeredCollider.gameObject.GetComponent<Cow>();
            if (cow != null)
            {
                if (!_controllables.Contains(cow))
                {
                    cow.SetTargetProvider(this);
                    GameLogicController.Instance.OnCowCorraled();
	                if (!isCheckEnabled)
	                {
		                isCheckEnabled = true;
	                }
					if (isCheckEnabled)
					{
						cowCount++;
						if (cowCount > Constants.RequiredCowForBonus)
						{
							GameMainController.Instance.GenerateBonusObject();
							cowCount -= Constants.RequiredCowForBonus;
							_checkTimer = 0;
							isCheckEnabled = false;							
						}
					}
				}                
            }
        }

	    protected override void ClearInternal()
	    {			
			cowCount = 0;
		    _checkTimer = 0;
			isCheckEnabled = false;
			_controllables.Clear();
			base.ClearInternal();
	    }

		public void FreeControllableMember(IControllable member)
		{
		}
	}   
}
