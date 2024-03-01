using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.SubGames.Mining
{
    public class MiningSubGameProvider : SubGameProvider, IPointerClickHandler
    {
        public override event Action Ended;

        [SerializeField] private List<Mineral> _minerals;
        [SerializeField, Range(0, 2)] private float _changingVisibilityDuration = 1;

        private Sequence _visibilityChangeSequence;
        private int _stageIndex;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
            
            foreach (var mineral in _minerals)
            {
                mineral.Mined += OnMiningCompleted;
            }
        }

        private void OnDestroy()
        {
            foreach (var mineral in _minerals)
            {
                mineral.Mined -= OnMiningCompleted;
            }
        }

        public override void Launch()
        {
            _stageIndex = _minerals.Count - 1;
            _minerals[_stageIndex].SetSelectionEffectStatus(true);
            ChangeVisibility(true);
        }

        public override void Close(TweenCallback callback = null)
        {
            ChangeVisibility(false, callback);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_stageIndex < 0) return;
            
            _minerals[_stageIndex].Extract();
        }

        private void OnMiningCompleted(Mineral mineral)
        {
            _stageIndex -= 1;

            for (int i = 0; i < _minerals.Count; i++)
            {
                _minerals[i].SetSelectionEffectStatus(_stageIndex == i);
            }
            
            if (_stageIndex < 0)
            {
                Ended?.Invoke();
            }
        }

        private void ChangeVisibility(bool isVisible, TweenCallback callback = null)
        {
            _visibilityChangeSequence?.Kill();
            _visibilityChangeSequence = DOTween.Sequence(transform.DOScale(isVisible ? 1 : 0, _changingVisibilityDuration)
                .SetEase(Ease.OutBounce)
                .OnComplete(callback));
        }
    }
}