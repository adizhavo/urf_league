using System;
using UnityEngine;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class RenderOrientationComponent : Attachable<AttachableEntityData, ChampionData>
    {
        #region implemented abstract members of Attachable

        public override void Boot() { }

        public override void Awake() { }

        public override void FrameFeed()
        {
            parentData.gameObject.transform.LookAt((parentData.currentPosition + parentData.orientation).toVector3());
        }

        public override void Destroy() { }
        #endregion
    }
}