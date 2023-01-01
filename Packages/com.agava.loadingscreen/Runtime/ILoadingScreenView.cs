using System.Collections;

namespace Agava.LoadingScreen
{
    public interface ILoadingScreenView
    {
        IEnumerator Prepare();
        void Render(float progress);
        IEnumerator Completion();
        IEnumerator FinalizeLoading();
    }
}