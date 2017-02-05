using System;

namespace URFLeague.Game.Entity.Attachable
{
    public interface IAttachableEntity : IEntity
    {
        IAttachableEntity AttachTo(IEntity parent);
    }

    public abstract class Attachable<Data> : IAttachableEntity where Data : AttachableEntityData
    {
        public Data adata;

        public IAttachableEntity AttachTo(IEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");

            if (adata == null) adata = Activator.CreateInstance<Data>();

            adata.parentData = parent.data;
            return this;
        }

        #region IEntity implementation

        public IEntityData data
        {
            get { return data; }
        }

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}