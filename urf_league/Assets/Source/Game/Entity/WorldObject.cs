using System.Collections.Generic;
using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public abstract class WorldObject<CustomData> : IComplexEntity where CustomData : IEntityData
    {   
        public CustomData championStats;
        public ClassDataMap[] componentDataMap;
        private List<IAttachableEntity> enabledComponents = new List<IAttachableEntity>();
        private List<IAttachableEntity> disabledComponents = new List<IAttachableEntity>();

        #region IComplexEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot(IEntityData initData = null)
        {
            foreach(var map in componentDataMap)
                AddComponent(map);
        }

        public void Awake()
        {
            for(i = 0; i < enabledComponents.Count; i ++)
                enabledComponents[i].Awake();
        }

        public void FrameFeed()
        {
            for(i = 0; i < enabledComponents.Count; i ++)
                enabledComponents[i].FrameFeed();
        }

        public void Destroy()
        {
            for(i = 0; i < enabledComponents.Count; i ++)
                enabledComponents[i].Destroy();
        }

        public void AddComponent(ClassDataMap map, bool awake = false)
        {
            IAttachableEntity aEnt = DataProvider.RequestAttachableEntityFromDataMap<IAttachableEntity>(map);
            aEnt.AttachTo(this);
            enabledComponents.Add(aEnt);
            if (awake) aEnt.Awake();
        }

        public void EnableComponent<T>() where T : IAttachableEntity
        {
            foreach(IAttachableEntity disabled in disabledComponents)
                if (disabled is T)
                {
                    enabledComponents.Add(disabled);
                    disabledComponents.Remove(disabled);
                    return;
                }

            UnityEngine.Debug.Log("The selected type was not found was not found");
        }

        public void EnableAllComponents()
        {
            foreach(IAttachableEntity disabled in disabledComponents)
                enabledComponents.Add(disabled);

            disabledComponents.Clear();
        }

        public void DisableComponent<T>() where T : IAttachableEntity
        {
            foreach(IAttachableEntity enabled in enabledComponents)
                if (enabled is T)
                {
                    disabledComponents.Add(enabled);
                    enabledComponents.Remove(enabled);
                    return;
                }

            UnityEngine.Debug.Log("The selected type was not found was not found");
        }

        public void DisableAllComponents()
        {
            foreach(IAttachableEntity enabled in enabledComponents)
                disabledComponents.Add(enabled);

            enabledComponents.Clear();
        }

        public void DisableAllComponentsExcept<T>() where T : IAttachableEntity
        {
            IAttachableEntity exception = null;

            foreach(IAttachableEntity enabled in enabledComponents)
            {
                if (enabled is T) 
                    exception = enabled;
                else
                    disabledComponents.Add(enabled);
            }

            enabledComponents.Clear();
            if (exception != null) enabledComponents.Add(exception);
            else UnityEngine.Debug.Log("The exception component was not found");
        }

        #endregion

        private int i = 0;
    }
}