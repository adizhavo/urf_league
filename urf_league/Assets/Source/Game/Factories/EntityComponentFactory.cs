using URFLeague.Config;
using URFLeague.Game.Entity;
using URFLeague.Game.Entity.Component;

namespace URFLeague.Game.Factory
{
    public static class EntityComponentFactory 
    {
        public static EntityComponent RequestComponent(string componentId, IEntity attachedTo)
        {
            switch (componentId)
            {
                case DataConfig.CHAMP_REND_COMP : 
                    return new PositionComponent(attachedTo);

                case DataConfig.MOUSE_CONTR_COMP : 
                    return new MouseControlComponent(attachedTo);

                case DataConfig.MOVEMENT_COMP : 
                    return new MovementComponent(attachedTo);
                
                default :
                    UnityEngine.Debug.LogError(componentId + " component requested is not registered in the factory, will return null.");
                    return null;
            }
        }
    }
}