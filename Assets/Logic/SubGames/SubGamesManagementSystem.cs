using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.SubGames
{
    public class SubGamesManagementSystem : MonoBehaviour
    {
        public event Action SubGameUnloaded;

        [SerializeField] private List<SubGameProvider> _subGameList;

        private Dictionary<int, SubGameProvider> _subGameDictionary;
        private SubGameProvider _currentSubGame;
        
        private void Awake()
        {
            _subGameDictionary = _subGameList.ToDictionary(g => g.Index, g => g);
        }

        public void Load(int index)
        {
            if (_currentSubGame) return;

            _currentSubGame = Instantiate(_subGameDictionary[index], transform);
            _currentSubGame.Launch();
            _currentSubGame.Ended += Unload;
        }

        public void Unload()
        {
            if (!_currentSubGame) return;
            
            _currentSubGame.Ended -= Unload;
            
            _currentSubGame.Close(() =>
            {
                Destroy(_currentSubGame.gameObject);
                SubGameUnloaded?.Invoke();
            });
        }
    }
}