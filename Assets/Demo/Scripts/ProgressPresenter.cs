using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bloodstone.Demo
{
    public class ProgressPresenter : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider = null;

        [SerializeField]
        private TMP_Text _textComponent = null;

        public void UpdateProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);

            _slider.value = progress;
            _textComponent.text = $"Progress: {progress:F2}";
        }
    }
}