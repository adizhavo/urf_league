using System;
using UnityEngine;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class PositionComponent : Attachable<AttachableEntityData>
    {
        private ChampionData cd;

        #region implemented abstract members of Attachable

        public override void Boot()
        {
            cd = ((ChampionData)adata.parentData);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");
        }

        public override void Awake()
        {
            cd.gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(cd.modelPath));
            cd.gameObject.transform.position = cd.startPosition.toVector3();
            cd.currentPosition = cd.startPosition;
        }

        public override void FrameFeed()
        {
            cd.gameObject.transform.position = cd.currentPosition.toVector3();
        }

        public override void Destroy()
        {
            GameObject.Destroy(cd.gameObject);
        }

        #endregion
    }
}