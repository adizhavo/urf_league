namespace URFLeague.Game.Entity
{
    public interface IEntity
    {
        IEntityData data {get;}
        void Boot();
        void Awake();
        void FrameFeed();
        void Destroy();
    }
}