using System;
using UnityEngine;

namespace Control
{
    public class UIManager : MonoBehaviour
    {
		public Menu prepareGameMenu;
		public Menu playGameMenu;
		public Menu winGameMenu;
		public Menu loseGameMenu;

		private Menu _currentMenu;
		
        void Start()
        {			
			playGameMenu.Hide();
			winGameMenu.Hide();
			loseGameMenu.Hide();
			
			ShowMenu(prepareGameMenu);
        }

	    private void ShowMenu(Menu menu)
	    {
			if (_currentMenu != menu)
			{
				if (_currentMenu != null)
				{
					_currentMenu.Hide();					
				}				
				_currentMenu = menu;
				_currentMenu.Show();
			}			
		}

        public void Update()
        {
            switch (GameMainController.Instance.GameState)
            {
	            case EGameState.Prepare:
		            ShowMenu(prepareGameMenu);
					break;
	            case EGameState.Play:
					ShowMenu(playGameMenu);
					break;
	            case EGameState.Win:
					ShowMenu(winGameMenu);
					break;
	            case EGameState.Lose:
					ShowMenu(loseGameMenu);
					break;
	            default:
		            throw new ArgumentOutOfRangeException();
            }	       
        }		
    }
}
