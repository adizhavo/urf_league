using URFLeague.Util.Data;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity
{
    public interface IComplexEntity : IEntity
    {
        void AddComponent(ClassDataMap map, bool awake = false);
        void EnableComponent<T>() where T : IAttachableEntity;
        void EnableAllComponents();
        void DisableComponent<T>() where T : IAttachableEntity; 
        void DisableAllComponents();
        void DisableAllComponentsExcept<T>() where T : IAttachableEntity;
    }
}