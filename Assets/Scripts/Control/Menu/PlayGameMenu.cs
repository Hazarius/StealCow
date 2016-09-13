using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
	public class PlayGameMenu : Menu
	{
		public GameObject buttonPrefab;
		private List<GameObject> _buttons = new List<GameObject>();
		public Text timeValue;
		public Text scoreValue;

		public override void Show()
		{
			SetControllables(GameMainController.Instance.ControllablePool.controllables);
			base.Show();
		}

		void Update()
		{
			var glc = GameLogicController.Instance;
			timeValue.text = glc.MaxTime > 0f? Mathf.Clamp(glc.MaxTime - glc.CurrentTime, 0f, glc.MaxTime).ToString("###0") : "infinity";
			scoreValue.text = glc.CurrentCows + "/" + glc.ExpectedCows;
		}

		private void SetControllables(List<IControllable> controllables)
		{
			foreach (var go in _buttons)
			{
				Destroy(go);
			}
			_buttons.Clear();
			foreach (var controllable in controllables)
			{
				var button = Instantiate(buttonPrefab);
				var buttonController = button.GetComponent<SelectionButtonController>();
				buttonController.Init(controllable);
				button.transform.SetParent(buttonPrefab.transform.parent, false);
				button.gameObject.SetActive(true);
				_buttons.Add(button);
			}
		}

		public void OnBackButtonClick()
		{
			GameMainController.Instance.Reset();
		}
	}
}