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
            foreach(var cn in componentDataMap)
            {
                IAttachableEntity component = DataProvider.RequestObjectInstance<IAttachableEntity>(cn.className).AttachTo(this);
                IEntityData customData = null;
                if (!string.IsNullOrEmpty(cn.jsonDataPath))
                {
                    customData = DataProvider.RequestObjectFromJson<IEntityData>(cn.dataClassName, cn.jsonDataPath);
                }

                component.Boot(customData);
                activeComponents.Add(component);
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

    [System.Serializable]
    public struct ClassDataMap
    {
        public string className;
        public string dataClassName;
        public string jsonDataPath;
    }
}