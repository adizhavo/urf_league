using System;
using UnityEngine;
using URFLeague.Config;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class MovementComponent : Attachable<AttachableEntityData, ChampionData>
    {
        #region implemented abstract members of Attachable

        public override void Boot() { }

        public override void Awake() { }

        public override void FrameFeed()
        {
            WorldCoordinate lookDirection = parentData.targetPosition - parentData.currentPosition;
            if (lookDirection.toVector3().sqrMagnitude > GameConfig.MIN_TARGET_POS_DISTANCE)
            {
                parentData.orientation = lookDirection.Normalized();
                parentData.currentPosition += parentData.orientation * parentData.movementSpeed * Time.deltaTime;

                #if UNITY_EDITOR
                LogVectors();
                #endif
            }
        }

        public override void Destroy() { }

        #endregion

        private void LogVectors()
        {
            Debug.DrawLine(parentData.currentPosition.toVector3(), parentData.targetPosition.toVector3(), Color.red);
            Debug.DrawLine(parentData.currentPosition.toVector3(), (parentData.currentPosition + parentData.orientation).toVector3(), Color.blue);
        }
    }
}