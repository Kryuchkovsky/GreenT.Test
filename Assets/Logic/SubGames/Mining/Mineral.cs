using System;
using DG.Tweening;
using UnityEngine;

namespace Logic.SubGames.Mining
{
    public abstract class Mineral : MonoBehaviour
    {
        public event Action<Mineral> Mined;

        protected int _currentDurability;
        
        [SerializeField] private int _miningStrength = 50;
        [SerializeField, Range(0, 3)] public float _miningAnimationDuration = 0.5f;
        
        private Sequence _miningAnimationSequence;

        [field: SerializeField, Range(1, 10)] public int Durability { get; private set; } = 1;
        
        public abstract MineralType Type { get; }

        private void Awake()
        {
            _currentDurability = Durability;
        }

        public abstract void SetSelectionEffectStatus(bool status);

        public virtual Sequence GetMiningAnimation() => DOTween.Sequence().Append(transform.DOShakePosition(_miningAnimationDuration, _miningStrength));

        public void Extract()
        {
            if (_currentDurability <= 0) return;
            
            _currentDurability -= 1;
            
            PlayMiningAnimation();
        }

        private void PlayMiningAnimation()
        {
            if (_miningAnimationSequence != null && _miningAnimationSequence.IsPlaying())
            {
                _miningAnimationSequence.Complete();
            }
            
            gameObject.SetActive(true);
            _miningAnimationSequence ??= DOTween.Sequence()
                .Append(GetMiningAnimation())
                .AppendCallback(OnMined)
                .SetAutoKill(false)
                .SetRecyclable(true)
                .Pause();
            _miningAnimationSequence?.Restart();
        }

        private void OnMined()
        {
            if (_currentDurability > 0) return;

            gameObject.SetActive(false);
            Mined?.Invoke(this);
        }
    }
}