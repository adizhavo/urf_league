using System;
using System.Collections.Generic;
using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {   
        public ChampionData championStats;

        public string[] componentsType;
        public string[] skillsId;

        private List<IAttachableEntity> components = new List<IAttachableEntity>();
        private List<IAttachableEntity> skills = new List<IAttachableEntity>();

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot()
        {
            foreach(var componentName in componentsType)
                components.Add(DataProvider.RequestObjectInstance<IAttachableEntity>(componentName).AttachTo(this));

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