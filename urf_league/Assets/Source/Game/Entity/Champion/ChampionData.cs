using System;
using UnityEngine;

namespace URFLeague.Game.Entity
{
    [Serializable]
    public class ChampionData : IEntityData
    {
        public string modelPath;
        public GameObject gameObject;
        public WorldCoordinate startPosition;
        public WorldCoordinate currentPosition;
        public WorldCoordinate targetPosition;
        public WorldCoordinate orientation;
        public float movementSpeed;
    }

    [Serializable]
    public struct WorldCoordinate
    {
        public float x, y, z;

        public static WorldCoordinate operator + (WorldCoordinate c1, WorldCoordinate c2)
        {
            return new WorldCoordinate
            {
                x = c1.x + c2.x,
                y = c1.y + c2.y,
                z = c1.z + c2.z
            };
        }

        public static WorldCoordinate operator - (WorldCoordinate c1, WorldCoordinate c2)
        {
            return new WorldCoordinate
            {
                x = c1.x - c2.x,
                y = c1.y - c2.y,
                z = c1.z - c2.z
            };
        }

        public Vector3 toVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}