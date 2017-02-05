namespace URFLeague.Game.Entity
{
    public interface IEntity
    {
        IEntityData data {get;}
        void Boot(IEntityData initData = null);
        void Awake();
        void FrameFeed();
        void Destroy();
    }
}