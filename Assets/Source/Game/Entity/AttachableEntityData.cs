using Newtonsoft.Json;

namespace URFLeague.Game.Entity.Attachable
{
    [System.Serializable]
    public class AttachableEntityData : IEntityData
    {
        [JsonIgnore]
        public IEntityData parentData;
    }
}