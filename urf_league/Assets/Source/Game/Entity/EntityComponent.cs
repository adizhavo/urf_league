using System;

namespace URFLeague.Game.Entity.Component
{
    public abstract class EntityComponent : IEntity 
    {
        private IEntity parent;

        public EntityComponent(IEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cennot be null");

            this.parent = parent;
        }

        #region IEntity implementation

        public IEntityData data
        {
            get { return parent.data; }
        }

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}