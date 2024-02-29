using DG.Tweening;
using UnityEngine;

namespace SubGames.Mining.Logic
{
    public class Waste : Mineral
    {
        public override MineralType Type => MineralType.Waste;
        
        [field: SerializeField, Range(0, 3)] public float MiningAnimationDuration { get; private set; } = 1;
        
        public override Sequence GetMiningAnimation()
        {
            return DOTween.Sequence()
                .Append(transform.DOShakePosition(MiningAnimationDuration, 30));
        }

        public override void SetSelectionEffectStatus(bool status)
        {
        }
    }
}