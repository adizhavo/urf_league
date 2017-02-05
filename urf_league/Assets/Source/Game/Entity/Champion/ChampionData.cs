using System;
using UnityEngine;

namespace URFLeague.Game.Entity
{
    [Serializable]
    public class ChampionData : IEntityData
    {
        public string modelPath;
        public GameObject gameObject;
        public WorldCoordinates startPosition;
        public WorldCoordinates currentPosition;
        public WorldCoordinates targetPosition;
        public float movementSpeed;
    }

    [Serializable]
    public struct WorldCoordinates
    {
        public float x, y, z;
    }
}