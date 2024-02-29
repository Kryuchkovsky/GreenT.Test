using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SubGames.Mining.Logic
{
    public class MiningSubGameProvider : SubGameProvider, IPointerClickHandler
    {
        public override event Action Ended;
        
        [SerializeField] private List<Mineral> _minerals;
        [SerializeField, Range(0, 2)] private float _changingVisibilityDuration = 1;

        private TweenerCore<Vector3, Vector3, VectorOptions> _visibilityChangeSequence;
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
            ChangeVisibility(true);
        }

        public override void Close()
        {
            ChangeVisibility(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
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
                ChangeVisibility(false, () => Ended?.Invoke());
            }
        }

        private void ChangeVisibility(bool isVisible, TweenCallback callback = null)
        {
            _visibilityChangeSequence?.Kill();
            _visibilityChangeSequence = transform.DOScale(isVisible ? 1 : 0, _changingVisibilityDuration)
                .SetEase(Ease.OutBounce)
                .OnComplete(callback);
        }
    }
}