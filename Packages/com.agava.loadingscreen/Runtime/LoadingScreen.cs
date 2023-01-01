using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Agava.LoadingScreen
{
    public class LoadingScreen
    {
        private readonly string _screenName;

        public LoadingScreen(string screenName)
        {
            _screenName = screenName;
        }

        public void LoadScene(int sceneBuildIndex)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneBuildIndex));
        }

        public void LoadScene(string sceneName)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneName));
        }

        public void LoadScene(int sceneBuildIndex, LoadSceneMode mode)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneBuildIndex, mode));
        }

        public void LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneBuildIndex, parameters));
        }

        public void LoadScene(string sceneName, LoadSceneMode mode)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneName, mode));
        }

        public void LoadScene(string sceneName, LoadSceneParameters parameters)
        {
            LoadScene(() => SceneManager.LoadSceneAsync(sceneName, parameters));
        }

        private void LoadScene(Func<AsyncOperation> sceneOperation)
        {
            var screenViewObject = Resources.Load(_screenName);

            if (screenViewObject == null)
                throw new ArgumentException($"Resource {_screenName} not found");

            var objectInstance = UnityEngine.Object.Instantiate(screenViewObject) as GameObject;
            var screenViewInstance = objectInstance.GetComponent<ILoadingScreenView>();
            
            UnityEngine.Object.DontDestroyOnLoad(objectInstance);

            var loadOperation = LoadOperation(sceneOperation, screenViewInstance);
            (screenViewInstance as MonoBehaviour).StartCoroutine(loadOperation);
        }

        private IEnumerator LoadOperation(Func<AsyncOperation> loadOperation, ILoadingScreenView screenView)
        {
            yield return screenView.Prepare();

            var operation = loadOperation?.Invoke();
            operation.allowSceneActivation = false;

            while (operation.isDone == false)
            {
                screenView.Render(operation.progress);

                if (operation.progress >= 0.9f)
                {
                    screenView.Render(operation.progress);
                    yield return screenView.Completion();
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }

            yield return screenView.FinalizeLoading();

            UnityEngine.Object.Destroy((screenView as MonoBehaviour).gameObject);
            Resources.UnloadUnusedAssets();
        }
    }
}