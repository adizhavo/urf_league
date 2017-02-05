using System;

namespace URFLeague.Game.Entity.Component
{
    public class ComponentData : IEntityData
    {
        public string id;
        public IEntityData parentData;
    }

    public abstract class EntityComponent : IEntity 
    {
        protected ComponentData componentData;

        public EntityComponent(string cmpId, IEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");

            componentData = new ComponentData
                            {
                                id = cmpId,
                                parentData = parent.data
                            };
        }

        #region IEntity implementation

        public IEntityData data
        {
            get { return componentData; }
        }

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}