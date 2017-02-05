using System;
using UnityEngine;

namespace URFLeague.Game.Entity.Component
{
    public class RenderOrientationComponent : EntityComponent
    {
        private ChampionData cd;

        public RenderOrientationComponent(IEntity parent) : base(parent) { }

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
            cd.gameObject.transform.LookAt((cd.currentPosition + cd.orientation).toVector3());
        }

        public override void Destroy() { }
        #endregion
    }
}