using UnityEngine;
using UnityEngine.UI;

namespace Control
{
	public class StartButtonController : MonoBehaviour
	{
		public Text buttonText;
		public Button button;
		public void Init(int level)
		{
			buttonText.text = "Start Level " + (level + 1);
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => GameMainController.Instance.StartGame(level));
		}
	}
}
