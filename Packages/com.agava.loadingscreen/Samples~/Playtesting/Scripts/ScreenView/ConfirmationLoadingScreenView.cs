using TMPro;
using UnityEngine;
using System.Collections;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    internal class ConfirmationLoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        [SerializeField] private TMP_Text _info;

        public IEnumerator Prepare()
        {
            return null;
        }

        public void Render(float progress)
        {
            _info.text = $"Loading {progress:0.00}%...";
        }

        public IEnumerator Completion()
        {
            _info.text = "Tap to continue";
            return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        public IEnumerator FinalizeLoading()
        {
            return null;
        }
    }
}
