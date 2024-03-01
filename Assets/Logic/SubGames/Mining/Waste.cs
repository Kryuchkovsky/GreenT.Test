using DG.Tweening;
using UnityEngine;

namespace Logic.SubGames.Mining
{
    public class Waste : Mineral
    {
        [SerializeField] private float _scalingDuration = 0.5f;
        
        public override MineralType Type => MineralType.Waste;
        
        public override Sequence GetMiningAnimation()
        {
            var baseSequence = base.GetMiningAnimation();
            
            if (_currentDurability <= 0)
            {
                baseSequence.Append(transform.DOScale(0, _scalingDuration));
            }

            return baseSequence;
        }
        
        public override void SetSelectionEffectStatus(bool status)
        {
        }
    }
}