using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Control
{
    public class SelectionButtonController : MonoBehaviour
    {
        public GameObject selection;
        public Button button;
        private IControllable _controllable;

        public void Init(IControllable controllable)
        {
            _controllable = controllable;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => GameInputController.Instance.SetControllable(controllable));
        }

        void Update()
        {
            var currentSelected = GameInputController.Instance.CurrentControllable != null &&
                                  GameInputController.Instance.CurrentControllable == _controllable;
            if (selection!=null &&selection.activeSelf!=currentSelected)
            {
                selection.SetActive(currentSelected);
            }
        }
    }
}
