using UnityEngine.UI;

namespace Control
{
	public class LoseGameMenu : Menu
	{
		public Button restartButton;
		public Text restartButtonText;

		public override void Show()
		{
			var gmc = GameMainController.Instance;
			restartButton.gameObject.SetActive(true);
			restartButton.onClick.RemoveAllListeners();
			restartButtonText.text = "Restart Level " + (gmc.CurrentLevel +1);
			restartButton.onClick.AddListener(() => gmc.StartGame(gmc.CurrentLevel));
			base.Show();
		}
	}
}
