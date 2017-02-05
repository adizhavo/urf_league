using System;

namespace URFLeague.Game.Entity.Skill
{
    public interface ISkillData : IEntityData
    {
        string id {set; get;}
        string[] disableComponentId {set; get;}
        IEntityData parentData {set; get;}
    }

    public abstract class EntitySkill : IEntity 
    {
        protected ISkillData skillData;

        public EntitySkill AttachTo(IEntity parent)
        {
            if (parent == null) 
                throw new ArgumentNullException("Parent entity", "Parent cannot be null");

            skillData.parentData = parent.data;
            return this;
        }

        #region IEntity implementation

        public IEntityData data
        {
            get { return skillData; }
        }

        public abstract void Boot();

        public abstract void Awake();

        public abstract void FrameFeed();

        public abstract void Destroy();

        #endregion
    }
}