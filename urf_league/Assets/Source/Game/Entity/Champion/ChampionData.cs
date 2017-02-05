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

        public static WorldCoordinates operator + (WorldCoordinates c1, WorldCoordinates c2)
        {
            return new WorldCoordinates
            {
                x = c1.x + c2.x,
                y = c1.y + c2.y,
                z = c1.z + c2.z
            };
        }

        public static WorldCoordinates operator - (WorldCoordinates c1, WorldCoordinates c2)
        {
            return new WorldCoordinates
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