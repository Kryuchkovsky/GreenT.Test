using System;
using DG.Tweening;
using UnityEngine;

namespace Logic.SubGames
{
    public abstract class SubGameProvider : MonoBehaviour
    {
        public abstract event Action Ended;

        [field: SerializeField] public int Index { get; private set; }
        
        public abstract void Launch();
        public abstract void Close(TweenCallback callback = null);
    }
}