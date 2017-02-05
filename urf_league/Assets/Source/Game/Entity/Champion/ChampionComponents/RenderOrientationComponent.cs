using System;
using UnityEngine;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class RenderOrientationComponent : Attachable<AttachableEntityData>
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
            cd.gameObject.transform.LookAt((cd.currentPosition + cd.orientation).toVector3());
        }

        public override void Destroy() { }
        #endregion
    }
}