
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
	public class PrepareGameMenu : Menu
	{		
		public GameObject buttonPrefab;
		private List<GameObject> _buttons = new List<GameObject>();
		public override void Show()
		{
			foreach (var button in _buttons)
			{
				Destroy(button);
			}
			_buttons.Clear();
			for (var i=0; i < GameMainController.Instance.levels.Count; i++)
			{				
				var buttonGo = Instantiate(buttonPrefab);
				buttonGo.transform.SetParent(buttonPrefab.transform.parent, false);

				var button = buttonGo.GetComponent<Button>();
				if (button!=null)
				{
					var buttonController = button.GetComponent<StartButtonController>();
					if (buttonController != null)
					{
						buttonController.Init(i);
					}									
				}
				buttonGo.gameObject.SetActive(true);
				_buttons.Add(buttonGo);
			}
			base.Show();
		}

		public void OnExitButtonClick()
		{
			Application.Quit();
		}
	}
}
