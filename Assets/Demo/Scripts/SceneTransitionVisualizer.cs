using System.Collections;
using UnityEngine;

namespace Bloodstone.Demo
{
    public class SceneTransitionVisualizer : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _fadableGroup = null;

        [SerializeField]
        private SceneController _controller = null;

        [SerializeField]
        private ProgressPresenter _presenter = null;

        private bool _transitionInProgress = false;

        public void ChangeScene(int sceneIndex)
        {
            StartCoroutine(ShowTransition());

            _controller.LoadScene(sceneIndex, UpdateProgress, IsOutsideTransition);
        }

        public bool IsOutsideTransition()
        {
            return !_transitionInProgress;
        }

        private void UpdateProgress(float progress)
        {
            if (progress == 1)
            {
                StartCoroutine(HideTransition());
            }

            _presenter.UpdateProgress(progress);
        }

        private IEnumerator HideTransition()
        {
            _transitionInProgress = true;

            for (var t = 1f; t > 0f; t -= Time.deltaTime)
            {
                _fadableGroup.alpha = Mathf.Clamp01(t);
                yield return null;
            }

            SwitchFadableGroup(0f);
            _transitionInProgress = false;
        }

        private IEnumerator ShowTransition()
        {
            _transitionInProgress = true;

            for (var t = 0f; t < 1f; t += Time.deltaTime)
            {
                _fadableGroup.alpha = Mathf.Clamp01(t);
                yield return null;
            }

            SwitchFadableGroup(1f);
            _transitionInProgress = false;
        }

        private void SwitchFadableGroup(float alpha)
        {
            _fadableGroup.alpha = alpha;
            _fadableGroup.interactable = alpha == 1f;
            _fadableGroup.blocksRaycasts = alpha == 1f;
        }
    }
}