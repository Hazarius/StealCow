using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Control
{
	public class ControllableGroup : IControllable
	{
		private readonly List<IControllable>  _group = new List<IControllable>();
		public int MembersCount { get { return _group.Count; } }

		private IControllableProvider _targetProvider;

		public Vector3 Position
		{
			get
			{
				var pos = Vector3.zero;
				if (_group.Count > 0)
				{
					for (int i = 0, iMax = _group.Count; i < iMax; i++)
					{
						pos += _group[i].Position;
					}
					return pos / _group.Count;
				}
				return pos;
			}
		}

		public void SetTarget(Vector3 targetPosition)
		{
			for (int i = 0, iMax = _group.Count; i < iMax; i++)
			{
				_group[i].SetTarget(targetPosition);
			}
		}

		public void SetTargetProvider(IControllableProvider provider)
		{
			_targetProvider = provider;
		}

		public bool IsContainMember(IControllable member)
		{
			return _group.Contains(member);
		}

		public void AddMember(IControllable member)
		{
			if (!IsContainMember(member))
			{
				member.SetTargetProvider(_targetProvider);
				_group.Add(member);
			}
		}

		public void RemoveMember(IControllable member)
		{
			if (IsContainMember(member))
			{
				member.SetTargetProvider(null);
				_group.Remove(member);
			}
		}

		public void Clear()
		{
			_group.Clear();
		}
	}
}
