using UnityEngine;
using UnityEngine.UI;

namespace Control
{
	public class WinLevelMenu : Menu
	{
		public Text timeValueText;
		public Button continueButton;
		public Text continueButtonText;
		public GameObject gameOverPanel;

		public override void Show()
		{
			timeValueText.text = GameLogicController.Instance.CurrentTime.ToString("###");
			var gmc = GameMainController.Instance;
			if (gmc.CurrentLevel + 1 < gmc.levels.Count)
			{
				gameOverPanel.gameObject.SetActive(false);
				continueButton.gameObject.SetActive(true);
				continueButton.onClick.RemoveAllListeners();
				continueButtonText.text = "Start Level " + (gmc.CurrentLevel + 2);
				continueButton.onClick.AddListener(() => gmc.StartGame(gmc.CurrentLevel + 1));
			}
			else
			{
				continueButton.gameObject.SetActive(false);
				gameOverPanel.gameObject.SetActive(true);
			}
			
			base.Show();
		}
	}
}
