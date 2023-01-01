using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    [RequireComponent(typeof(Button))]
    internal class SwitchSceneButton : MonoBehaviour
    {
        [SerializeField] private string _screenName;

        private LoadingScreen _loadingScreen;
        private Button _button;

        private void Awake()
        {
            _loadingScreen = new LoadingScreen(_screenName);
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            _loadingScreen.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        }
    }
}