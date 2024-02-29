using System;
using DG.Tweening;
using UnityEngine;

namespace SubGames.Mining.Logic
{
    public abstract class Mineral : MonoBehaviour
    {
        public event Action<Mineral> Mined;
        
        private Sequence _miningAnimationSequence;
        private int _currentDurability;

        [field: SerializeField, Min(0)] public int Reward { get; private set; }
        [field: SerializeField, Range(0, 10)] public int Durability { get; private set; } = 1;
        
        public abstract MineralType Type { get; }

        private void Awake()
        {
            _currentDurability = Durability;
        }
        
        public abstract Sequence GetMiningAnimation();
        public abstract void SetSelectionEffectStatus(bool status);

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

        protected void OnMined()
        {
            if (_currentDurability > 0) return;

            gameObject.SetActive(false);
            Mined?.Invoke(this);
        }
    }
}