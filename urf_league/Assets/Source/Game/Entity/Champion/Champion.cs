namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {
        private ChampionData championStats;

        private EntityComponent[] championComponents;

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot()
        {
            foreach(var cmp in championComponents)
                cmp.Boot();
        }

        public void Awake()
        {
            foreach(var cmp in championComponents)
                cmp.Awake();
        }

        public void FrameFeed()
        {
            foreach(var cmp in championComponents)
                cmp.FrameFeed();
        }

        public void Destroy()
        {
            foreach(var cmp in championComponents)
                cmp.Destroy();
        }
        #endregion
    }
}