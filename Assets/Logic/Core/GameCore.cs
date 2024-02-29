using System.Collections.Generic;
using SubGames;
using UnityEngine;

namespace Logic.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private List<SubGameInvoker> _subGameInvokerList;
        [SerializeField] private SubGamesManagementSystem _subGamesManagementSystem;
        [SerializeField] private TransparentBackground _transparentBackground;

        private void Awake()
        {
            foreach (var invoker in _subGameInvokerList)
            {
                invoker.Invoked += LoadSubGame;
            }

            _subGamesManagementSystem.SubGameUnloaded += OnSubGameUnloaded;
            
            _transparentBackground.Clicked += UnloadSubGame;
        }

        private void OnDestroy()
        {
            foreach (var invoker in _subGameInvokerList)
            {
                invoker.Invoked -= LoadSubGame;
            }
            
            _subGamesManagementSystem.SubGameUnloaded -= OnSubGameUnloaded;
            
            _transparentBackground.Clicked -= UnloadSubGame;
        }

        private void LoadSubGame(int index)
        {
            _subGamesManagementSystem.Load(index);
            _transparentBackground.ChangeVisibility(true);
        }
        
        private void UnloadSubGame()
        {
            _subGamesManagementSystem.Unload();
            _transparentBackground.ChangeVisibility(false);
        }
        
        private void OnSubGameUnloaded()
        {
            _transparentBackground.ChangeVisibility(false);
        }
    }
}