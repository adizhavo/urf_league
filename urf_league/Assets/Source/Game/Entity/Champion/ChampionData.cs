using System;
using UnityEngine;

namespace URFLeague.Game.Entity
{
    [Serializable]
    public class ChampionData : IEntityData
    {
        public string modelPath;
        public GameObject gameObject;
        public WorldCoordinates worldPosition;
    }

    [Serializable]
    public struct WorldCoordinates
    {
        public float x, y, z;
    }
}