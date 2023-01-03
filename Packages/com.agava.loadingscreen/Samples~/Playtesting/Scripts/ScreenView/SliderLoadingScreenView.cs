using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    internal class SliderLoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        private const float LerpEpsilon = 0.01f;

        [SerializeField, Min(0.1f)] private float _sliderSpeed = 10f;
        [SerializeField] private DurationAnimationCurve _showAnimation;
        [SerializeField] private DurationAnimationCurve _hideAnimation;
        [SerializeField] private GameObject _content;

        private CanvasGroup _canvasGroup;
        private Slider _progress;
        private float _targetProgress;

        private void OnValidate()
        {
            if (GetComponentInChildren<CanvasGroup>() == null)
                throw new Exception($"{nameof(CanvasGroup)} component is missing");

            if (GetComponentInChildren<Slider>() == null)
                throw new Exception($"{nameof(Slider)} component is missing");
        }

        private void Awake()
        {
            _canvasGroup = GetComponentInChildren<CanvasGroup>();
            _progress = GetComponentInChildren<Slider>();

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _progress.value = Mathf.Lerp(_progress.value, _targetProgress, _sliderSpeed * Time.deltaTime);
        }

        public IEnumerator Prepare()
        {
            _canvasGroup.alpha = 0f;
            _content.gameObject.SetActive(false);

            yield return _showAnimation.Execute((value) => _canvasGroup.alpha = value);

            _content.gameObject.SetActive(true);

            yield return null;
        }

        public void Render(float progress)
        {
            _targetProgress = progress;
        }

        public IEnumerator Completion()
        {
            _targetProgress = 1f;
            return new WaitUntil(() => 1f - _progress.value < LerpEpsilon);
        }

        public IEnumerator FinalizeLoading()
        {
            _content.gameObject.SetActive(false);
            return _hideAnimation.Execute((value) => _canvasGroup.alpha = value);
        }
    }
}