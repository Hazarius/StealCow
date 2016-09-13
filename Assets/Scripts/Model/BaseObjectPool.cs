using System.Collections.Generic;
using UnityEngine;

namespace Model
{
	public class BaseObjectPool<T> : MonoBehaviour where T : BaseModel
	{
		public GameObject prefab;
		private List<T> _pool;

		void Awake()
		{
			_pool = new List<T>();
		}
		
		public T GetModel()
		{
			for (int i = 0; i < _pool.Count; i++)
			{
				if (!_pool[i].IsEnabled)
				{
					return _pool[i];
				}
			}			
			var obj = Instantiate(prefab);
			obj.transform.SetParent(transform, false);
			var newModel = obj.GetComponent<T>();
			if (newModel != null)
			{
				_pool.Add(newModel);
			}			
			return newModel;
		}

		public void Clear()
		{
			for (int i = 0; i < _pool.Count; i++)
			{
				_pool[i].Retain();
			}
		}
	}
}
