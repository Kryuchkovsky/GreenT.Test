using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.SubGames
{
    public class SubGameInvoker : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> Invoked;

        [SerializeField] private Vector3 _scalingForce = Vector3.one * 0.2f;
        [SerializeField, Range(0, 2)] private float _scalingDuration = 0.3f;
        [SerializeField] private int _subGameIndex;

        public void OnPointerClick(PointerEventData eventData)
        {
            _scalingForce = Vector3.one * 0.2f;
            transform.DOPunchScale(_scalingForce, 0.3f);
            Invoked?.Invoke(_subGameIndex);
        }
    }
}