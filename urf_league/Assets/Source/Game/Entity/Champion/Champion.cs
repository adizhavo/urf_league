using System;
using Newtonsoft.Json;
using URFLeague.Game.Factory;
using System.Collections.Generic;

namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {   
        public ChampionData championStats;

        public string[] componentsId;

        private List<EntityComponent> components = new List<EntityComponent>();

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot()
        {
            foreach(var id in componentsId)
                components.Add(EntityComponentFactory.RequestComponent(id, this));

            foreach(var cmp in components)
                cmp.Boot();
        }

        public void Awake()
        {
            foreach(var cmp in components)
                cmp.Awake();
        }

        public void FrameFeed()
        {
            foreach(var cmp in components)
                cmp.FrameFeed();
        }

        public void Destroy()
        {
            foreach(var cmp in components)
                cmp.Destroy();
        }
        #endregion
    }
}