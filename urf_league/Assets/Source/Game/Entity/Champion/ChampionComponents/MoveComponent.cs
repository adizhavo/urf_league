using System;
using UnityEngine;
using URFLeague.Config;

namespace URFLeague.Game.Entity.Component
{
    public class MoveComponent : EntityComponent
    {
        private ChampionData cd;

        public MoveComponent(IEntity parent) : base(parent) { }

        #region implemented abstract members of EntityComponent
        public override void Boot()
        {
            if (base.data == null)
                throw new NullReferenceException("Entity data is null");

            cd = ((ChampionData)base.data);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");
        }

        public override void Awake() { }

        public override void FrameFeed()
        {
            WorldCoordinate lookDirection = cd.targetPosition - cd.currentPosition;
            if (lookDirection.toVector3().sqrMagnitude > Mathf.Epsilon)
            {
                cd.orientation = lookDirection.Normalized();
                cd.currentPosition += cd.orientation * cd.movementSpeed * Time.deltaTime;
            }
        }

        public override void Destroy() { }
        #endregion
    }
}