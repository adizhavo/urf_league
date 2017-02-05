using System;
using UnityEngine;

namespace URFLeague.Game.Entity.Component
{
    public class RenderOrientationComponent : EntityComponent
    {
        private ChampionData cd;

        public RenderOrientationComponent(string cmpId, IEntity parent) : base(cmpId, parent) { }

        #region implemented abstract members of EntityComponent
        public override void Boot()
        {
            cd = ((ChampionData)componentData.parentData);

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