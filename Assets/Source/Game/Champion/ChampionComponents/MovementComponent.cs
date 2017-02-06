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
            parentData.orientation = lookDirection.Normalized();

            if (lookDirection.toVector3().sqrMagnitude > GameConfig.MIN_TARGET_POS_DISTANCE)
            {
                Vector3 offsetVector = parentData.orientation.toVector3() * parentData.movementSpeed * Time.deltaTime;
                offsetVector = Vector3.ClampMagnitude(offsetVector, lookDirection.toVector3().magnitude);
                parentData.currentPosition += new WorldCoordinate
                                              {
                                                  x = offsetVector.x,
                                                  y = offsetVector.y, 
                                                  z = offsetVector.z
                                              };

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