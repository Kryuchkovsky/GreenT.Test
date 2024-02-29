using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SubGames.Mining.Logic
{
    public class Gem : Mineral
    {
        [SerializeField] private Image _image;
        [SerializeField] private ParticleSystem _selectionEffect;
        
        public override MineralType Type => MineralType.Gem;

        public override Sequence GetMiningAnimation()
        {
            return DOTween.Sequence()
                .Append(transform.DOLocalMove(Vector3.zero, 0.5f))
                .Join(transform.DOScale(2, 0.5f))
                .AppendInterval(1)
                .Append(transform.DOScale(0, 0.5f));
        }

        public override void SetSelectionEffectStatus(bool status)
        {
            _image.material.SetFloat("_Thickness", status ? 0.1f : 0);
            
            if (status)
            {
                _selectionEffect.Play();
            }
            else
            {
                _selectionEffect.Stop();
            }

            _selectionEffect.gameObject.SetActive(status);
        }
    }
}