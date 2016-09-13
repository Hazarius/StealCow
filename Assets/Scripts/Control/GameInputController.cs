using System;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Control
{
    public class GameInputController : MonoBehaviour
    {
        public static GameInputController Instance { get; private set; }
        public IControllable CurrentControllable { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        void Update()
        {
			if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
				return;			
			if (Input.GetMouseButtonDown(0))
			{
				var hitPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);				
				Debug.DrawRay(hitPosition, Vector3.forward * 30, Color.red, 5);

				var hits = Physics2D.RaycastAll(hitPosition, Vector3.forward, 30);
				Array.Sort(hits, RendererComparator);

				for (int i = 0, iMax = hits.Length; i < iMax; i++)
				{					
					var hit = hits[i];

					if (hit.rigidbody.gameObject.tag == "Dog")
					{
						var dog = hit.rigidbody.gameObject.GetComponent<Dog>();
						if (dog != null)
						{
							if (dog == CurrentControllable)
							{
								hitPosition.z = 0;
								CurrentControllable.SetTarget(hitPosition);
							}
							else
							{
								SetControllable(dog);
							}

							/*
							 if (dog.Controllable!=null && dog.Controllable == CurrentControllable)
							{
								hitPosition.z = 0;
								CurrentControllable.SetTarget(hitPosition);
							}
							else
							{
								SetControllable(dog.Controllable);
							}*/
						}
						break;
					}
					if (CurrentControllable != null)
					{
						if (hit.rigidbody.gameObject.tag == "Ground")
						{
							hitPosition.z = 0;
							CurrentControllable.SetTarget(hitPosition);
							break;
						}
					}					
				}							
			}
        }

	    private static int RendererComparator(RaycastHit2D lhs, RaycastHit2D rhs)
	    {
		    var lhsRenderer = lhs.transform.gameObject.GetComponent<SpriteRenderer>();
			var rhsRenderer = rhs.transform.gameObject.GetComponent<SpriteRenderer>();
		    if (lhsRenderer == null || rhsRenderer == null)
			    return 1;
		    if (lhsRenderer.sortingOrder == rhsRenderer.sortingOrder) return 0;
		    return lhsRenderer.sortingOrder < rhsRenderer.sortingOrder ? 1 : -1;			
	    }


		public void SetControllable(IControllable currentControllable)
        {
            CurrentControllable = currentControllable;
        }

	    public void Reset()
	    {
		    SetControllable(null);
	    }

        private void OnDestroy()
        {
            Instance = null;
        }
    }    
}
