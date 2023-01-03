using System.Collections;
using UnityEngine;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    internal class SimpleAnimatedLoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        public IEnumerator Prepare() => null;

        public void Render(float progress) { }

        public IEnumerator Completion() => null;

        public IEnumerator FinalizeLoading() => null;
    }
}
