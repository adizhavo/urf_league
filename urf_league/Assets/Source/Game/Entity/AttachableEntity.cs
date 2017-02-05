using System;

namespace URFLeague.Game.Entity.Attachable
{
    public interface IAttachableEntity : IEntity
    {
        IAttachableEntity AttachTo(IComplexEntity parent);
    }

    public abstract class Attachable<CustomData, ParentData> : IAttachableEntity where CustomData : AttachableEntityData
                                                                                 where ParentData : IEntityData
    {
        private CustomData cd;

        public virtual IAttachableEntity AttachTo(IComplexEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");

            if (cd == null) cd = Activator.CreateInstance<CustomData>();

            if (parent.data is ParentData) cd.parentData = parent.data;
            else throw new InvalidCastException("The parent is not compatible with this attachable entity of type: " + this.GetType() + ", check if the class definition matches the parent data structure");

            return this;
        }

        public CustomData customData
        {
            get { return cd; }
        }

        public ParentData parentData
        {
            get { return (ParentData)customData.parentData;  }
        }

        #region IEntity implementation

        public IEntityData data
        {
            get { return customData; }
        }

        public virtual void Boot(IEntityData initData = null)
        {
            if (initData != null && initData is CustomData)
            {
                if (cd != null && cd.parentData != null)
                {
                    IEntityData pData = customData.parentData;
                    cd = (CustomData)initData;
                    cd.parentData = pData;
                }
                else cd = (CustomData)initData;
            }

            if (cd == null) cd = Activator.CreateInstance<CustomData>();
        }

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}