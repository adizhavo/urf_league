using System.Collections.Generic;
using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {   
        public ChampionData championStats;
        public ClassDataMap[] componentDataMap;
        private List<IAttachableEntity> activeComponents = new List<IAttachableEntity>();

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot(IEntityData initData = null)
        {
            foreach(var map in componentDataMap)
            {
                IAttachableEntity aEnt = DataProvider.RequestAttachableEntityFromDataMap<IAttachableEntity>(map);
                aEnt.AttachTo(this);
                activeComponents.Add(aEnt);
            }
        }

        public void Awake()
        {
            foreach(var cmp in activeComponents)
                cmp.Awake();
        }

        public void FrameFeed()
        {
            foreach(var cmp in activeComponents)
                cmp.FrameFeed();
        }

        public void Destroy()
        {
            foreach(var cmp in activeComponents)
                cmp.Destroy();
        }

        #endregion
    }
}