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
        public CustomData customData;
        protected IComplexEntity parent;

        public virtual IAttachableEntity AttachTo(IComplexEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");
            else
                this.parent = parent;

            if (customData == null) customData = Activator.CreateInstance<CustomData>();

            if (parent.data is ParentData) customData.parentData = parent.data;
            else throw new InvalidCastException("The parent is not compatible with this attachable entity of type: " + this.GetType() 
                                                + ", check if the class definition matches the parent data structure. The defined parent data type is" 
                                                + typeof(ParentData));

            return this;
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

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}