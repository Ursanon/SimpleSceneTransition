using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bloodstone
{
    public class SceneController : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void LoadScene(int sceneIndex, Action<float> progressCallback, Func<bool> predicate)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIndex, progressCallback, predicate));
        }

        private IEnumerator LoadSceneCoroutine(int sceneIndex, Action<float> progressCallback, Func<bool> canActivate)
        {
            const float activationThreshold = 0.9f;

            var loading = SceneManager.LoadSceneAsync(sceneIndex);
            loading.allowSceneActivation = false;

            var progress = loading.progress;
            while (progress < activationThreshold)
            {
                progress = loading.progress;

                progressCallback?.Invoke(progress);

                yield return null;
            }

            yield return new WaitUntil(canActivate);
            loading.allowSceneActivation = true;

            while (!loading.isDone)
            {
                yield return null;
            }

            progressCallback?.Invoke(loading.progress);
        }
    }
}