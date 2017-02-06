using System;
using UnityEngine;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class PositionComponent : Attachable<AttachableEntityData, ChampionData>
    {
        #region implemented abstract members of Attachable

        public override void Boot() { }

        public override void Awake()
        {
            parentData.gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(parentData.modelPath));
            parentData.gameObject.transform.position = parentData.startPosition.toVector3();
            parentData.currentPosition = parentData.startPosition;
        }

        public override void FrameFeed()
        {
            parentData.gameObject.transform.position = parentData.currentPosition.toVector3();
        }

        public override void Destroy()
        {
            GameObject.Destroy(parentData.gameObject);
        }

        #endregion
    }
}