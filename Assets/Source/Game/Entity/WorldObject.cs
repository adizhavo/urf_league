using System.Collections.Generic;
using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public abstract class WorldObject<CustomData> : IComplexEntity where CustomData : IEntityData
    {   
        public CustomData objectStats;
        public ClassDataMap[] componentDataMap;
        private List<IAttachableEntity> enabledComponents = new List<IAttachableEntity>();
        private List<IAttachableEntity> disabledComponents = new List<IAttachableEntity>();

        #region IComplexEntity implementation

        public IEntityData data 
        {
            get { return objectStats; }
        }

        public void Boot()
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
            IAttachableEntity aEnt = DataProvider.RequestAttachableEntityFromDataMap(map);
            aEnt.Boot();
            aEnt.AttachTo(this);
            if (awake) aEnt.Awake();
            enabledComponents.Add(aEnt);
        }

        public T GetComponent<T>() where T : IAttachableEntity
        {
            foreach(IAttachableEntity enabled in enabledComponents)
                if (enabled is T)
                    return (T)enabled;

            foreach(IAttachableEntity disabled in disabledComponents)
                if (disabled is T)
                    return (T)disabled;

            UnityEngine.Debug.Log("The selected " + typeof(T) + " was not found");
            return default(T);
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

            UnityEngine.Debug.Log("The selected " + typeof(T) + " was not found");
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

            UnityEngine.Debug.Log("The selected " + typeof(T) + " was not found");
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
            else UnityEngine.Debug.Log("The exception " + typeof(T) + " was not found");
        }

        #endregion

        private int i = 0;
    }
}