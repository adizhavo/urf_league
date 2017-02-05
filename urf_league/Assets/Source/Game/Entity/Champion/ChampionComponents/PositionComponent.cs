using System;
using UnityEngine;

namespace URFLeague.Game.Entity.Component
{
    public class PositionComponent : EntityComponent 
    {
        private ChampionData cd;

        public PositionComponent(IEntity parent) : base(parent) { }

        #region implemented abstract members of EntityComponent

        public override void Boot()
        {
            if (base.data == null)
                throw new NullReferenceException("Entity data is null");

            cd = ((ChampionData)base.data);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");

            cd.gameObject.transform.position = new Vector3(cd.startPosition.x, cd.startPosition.y, cd.startPosition.z);
            cd.currentPosition = cd.startPosition;
        }

        public override void Awake()
        {
            cd.gameObject = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(cd.modelPath));
        }

        public override void FrameFeed()
        {
            cd.gameObject.transform.position = new Vector3(cd.currentPosition.x, cd.currentPosition.y, cd.currentPosition.z);
        }

        public override void Destroy()
        {
            GameObject.Destroy(cd.gameObject);
        }

        #endregion
    }
}