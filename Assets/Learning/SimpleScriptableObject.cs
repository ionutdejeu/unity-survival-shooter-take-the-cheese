using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Learning
{
    [CreateAssetMenu(menuName = "New Game Database")]
    public class SimpleScriptableObject:ScriptableObject
    {
        public List<Games> games = new List<Games>();

        public enum GameType { Action, Adventure, RPG, Puzzle, FPS, RTS };
        public enum GameQuality { Bad, Decent, Good };

        [System.Serializable]
        public class Games
        {
            public GameObject GamePrefab;
            public string Name;
            public string Description;
            public GameQuality GameQuality;
            public GameType GameType;
            public string Review;
            public int Cost;
        }
    }
}
