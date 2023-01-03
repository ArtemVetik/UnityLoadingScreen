using System;
using System.Collections;
using UnityEngine;

namespace Agava.LoadingScreen.Samples.Playtesting
{
    [Serializable]
    internal class DurationAnimationCurve
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _duration;

        internal IEnumerator Execute(Action<float> onUpdate)
        {
            var length = _curve.keys[_curve.length - 1].time - _curve.keys[0].time;
            var time = 0f;

            while (time < length)
            {
                time += length / _duration * Time.deltaTime;
                onUpdate?.Invoke(_curve.Evaluate(time));

                yield return null;
            }
        }
    }
}
