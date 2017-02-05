using System.Collections.Generic;
using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {   
        public ChampionData championStats;

        public string[] componentsClassName;
        public ClassDataMap[] skillDataMap;

        private List<IAttachableEntity> components = new List<IAttachableEntity>();
        private List<IAttachableEntity> skills = new List<IAttachableEntity>();

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot(IEntityData initData = null)
        {
            foreach(var cn in componentsClassName)
            {
                IAttachableEntity cmp = DataProvider.RequestObjectInstance<IAttachableEntity>(cn).AttachTo(this);
                cmp.Boot();
                components.Add(cmp);
            }

            foreach(var sdm in skillDataMap)
            {
                IAttachableEntity skill = DataProvider.RequestObjectInstance<IAttachableEntity>(sdm.className).AttachTo(this);
                IEntityData skillData = DataProvider.RequestObjectFromJson<IEntityData>(sdm.dataClassName, sdm.jsonDataPath);
                skill.Boot(skillData);
                skills.Add(skill);
            }
        }

        public void Awake()
        {
            foreach(var cmp in components)
                cmp.Awake();

            foreach(var s in skills)
                s.Awake();
        }

        public void FrameFeed()
        {
            foreach(var cmp in components)
                cmp.FrameFeed();

            foreach(var s in skills)
                s.FrameFeed();
        }

        public void Destroy()
        {
            foreach(var cmp in components)
                cmp.Destroy();

            foreach(var s in skills)
                s.Destroy();
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