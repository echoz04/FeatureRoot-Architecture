using DG.Tweening;
using Sources.Core.Features.UI.Root;
using UnityEngine;

namespace Sources.Core.Features.UI.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BasePopup<TRoot> : BaseUIView where TRoot : IUIRoot
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private bool _isShowing = false;
        private Tween _tween;

        private void OnValidate()
        {
            _canvasGroup ??= GetComponent<CanvasGroup>();
        }

        protected virtual void OnDisable()
        {
            _tween?.Kill();
            _tween = null;

            transform.localScale = Vector3.one;

            if (_canvasGroup != null)
                _canvasGroup.alpha = 1f;

            _isShowing = false;
        }

        public override void Show()
        {
            if (_isShowing == true)
                return;

            _isShowing = true;

            gameObject.SetActive(true);

            _tween?.Kill();

            ShowAnimation();
        }

        public override void Hide()
        {
            if (_isShowing == false)
                return;

            _isShowing = false;

            _tween?.Kill();

            HideAnimation();
        }

        protected virtual void ShowAnimation()
        {
            transform.localScale = Vector3.zero;

            if (_canvasGroup != null)
                _canvasGroup.alpha = 0f;

            var sequence = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));

            if (_canvasGroup != null)
                sequence.Join(_canvasGroup.DOFade(1f, 0.3f));

            _tween = sequence.OnComplete(() => _tween = null);
        }

        protected virtual void HideAnimation()
        {
            var sequence = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack));

            if (_canvasGroup != null)
                sequence.Join(_canvasGroup.DOFade(0f, 0.3f));

            _tween = sequence.OnComplete(() =>
            {
                _tween = null;
                gameObject.SetActive(false);
            });
        }
    }
}