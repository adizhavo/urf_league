using System;

namespace URFLeague.Game.Entity
{
    public class ChampionPositionComponent : EntityComponent 
    {
        private ChampionData cd;

        public ChampionPositionComponent(IEntity parent) : base(parent) { }

        #region implemented abstract members of EntityComponent

        public override void Boot()
        {
            ChampionData cd = ((ChampionData)base.data);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");
        }

        public override void Awake()
        {
            cd.gameObject = UnityEngine.Resources.Load<UnityEngine.GameObject>(cd.modelPath);
        }

        public override void FrameFeed()
        {
            cd.gameObject.transform.position = cd.worldPosition;
        }

        public override void Destroy()
        {
            UnityEngine.GameObject.Destroy(cd.gameObject);
        }

        #endregion
    }
}