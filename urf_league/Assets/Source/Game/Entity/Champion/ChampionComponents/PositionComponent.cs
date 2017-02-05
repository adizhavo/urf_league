using System;
using UnityEngine;

namespace URFLeague.Game.Entity.Component
{
    public class PositionComponent : EntityComponent 
    {
        private ChampionData cd;

        public PositionComponent(string cmpId, IEntity parent) : base(cmpId, parent) { }

        #region implemented abstract members of EntityComponent

        public override void Boot()
        {
            cd = ((ChampionData)componentData.parentData);

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