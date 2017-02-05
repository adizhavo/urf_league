using System;
using UnityEngine;
using URFLeague.Config;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class MovementComponent : Attachable<AttachableEntityData>
    {
        private ChampionData cd;

        #region implemented abstract members of Attachable
        public override void Boot()
        {
            cd = ((ChampionData)adata.parentData);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");
        }

        public override void Awake() { }

        public override void FrameFeed()
        {
            WorldCoordinate lookDirection = cd.targetPosition - cd.currentPosition;
            if (lookDirection.toVector3().sqrMagnitude > GameConfig.MIN_TARGET_POS_DISTANCE)
            {
                cd.orientation = lookDirection.Normalized();
                cd.currentPosition += cd.orientation * cd.movementSpeed * Time.deltaTime;

                #if UNITY_EDITOR
                LogVectors();
                #endif
            }
        }

        public override void Destroy() { }
        #endregion

        private void LogVectors()
        {
            Debug.DrawLine(cd.currentPosition.toVector3(), cd.targetPosition.toVector3(), Color.red);
            Debug.DrawLine(cd.currentPosition.toVector3(), (cd.currentPosition + cd.orientation).toVector3(), Color.blue);
        }
    }
}