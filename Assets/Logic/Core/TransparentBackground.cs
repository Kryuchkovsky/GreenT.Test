using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logic.Core
{
    public class TransparentBackground : MonoBehaviour, IPointerClickHandler
    {
        public event Action Clicked;

        [SerializeField] private Image _image;
        [SerializeField, Range(0, 2)] private float _visibilityChangeDuration = 0.5f;
        [SerializeField, Range(0, 1)] private float _targetBackgroundAlphaValue = 0.5f;
        
        private Sequence _visibilityChangeSequence;

        public void ChangeVisibility(bool isVisible)
        {
            if (isVisible)
            {
                gameObject.SetActive(true);
            }
            
            _visibilityChangeSequence?.Kill();
            _visibilityChangeSequence = DOTween.Sequence()
                .Append(_image.DOFade(isVisible ? _targetBackgroundAlphaValue : 0, _visibilityChangeDuration))
                .AppendCallback(() => gameObject.SetActive(isVisible));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
    }
}