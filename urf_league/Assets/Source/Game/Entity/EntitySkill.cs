using System;

namespace URFLeague.Game.Entity.Skill
{
    public interface ISkillData : IEntityData
    {
        string id {set; get;}
        string[] disableComponentId {set; get;}
    }

    public abstract class EntitySkill : IEntity 
    {
        protected IEntity parent;

        public EntitySkill(IEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");

            this.parent = parent;
        }

        #region IEntity implementation

        public IEntityData data {get;}

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}