using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.SubGames.Mining
{
    public class Gem : Mineral
    {
        [SerializeField] private Image _image;
        [SerializeField] private ParticleSystem _selectionEffect;
        [SerializeField] private int _showingDuration = 1;
        [SerializeField] private int _targetScale = 3;
        [SerializeField] private float _scalingDuration = 0.5f;

        public override MineralType Type => MineralType.Gem;

        public override Sequence GetMiningAnimation()
        {
            var baseSequence = base.GetMiningAnimation();
            
            if (_currentDurability <= 0)
            {
                baseSequence.Append(transform.DOLocalMove(Vector3.zero, _scalingDuration))
                    .Join(transform.DOScale(_targetScale, _scalingDuration))
                    .AppendInterval(_showingDuration)
                    .Append(transform.DOScale(0, _scalingDuration));
            }

            return baseSequence;
        }

        public override void SetSelectionEffectStatus(bool status)
        {
            _image.material.SetFloat("_Thickness", status ? 0.1f : 0);
            _selectionEffect.gameObject.SetActive(status);
            
            if (status)
            {
                _selectionEffect.Play();
            }
            else
            {
                _selectionEffect.Stop();
            }
        }
    }
}