using System.Collections;

namespace Agava.LoadingScreen
{
    public interface ILoadingScreenView
    {
        IEnumerator Prepare();
        void Update(float progress);
        IEnumerator Completion();
        IEnumerator FinalizeLoading();
    }
}