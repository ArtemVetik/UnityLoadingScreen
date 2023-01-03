using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    internal class FadeLoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        [SerializeField] private DurationAnimationCurve _showAnimation;
        [SerializeField] private DurationAnimationCurve _hideAnimation;
        [SerializeField] private MaskableGraphic _graphic;

        public IEnumerator Prepare()
        {
            return _showAnimation.Execute((value) => 
            {
                var color = _graphic.color;
                color.a = value;
                _graphic.color = color;
            });
        }

        public IEnumerator Completion()
        {
            return null;
        }

        public IEnumerator FinalizeLoading()
        {
            return _hideAnimation.Execute((value) =>
            {
                var color = _graphic.color;
                color.a = value;
                _graphic.color = color;
            });
        }

        public void Render(float progress) { }
    }
}