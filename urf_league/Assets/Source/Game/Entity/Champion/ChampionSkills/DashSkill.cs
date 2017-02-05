using System;

namespace URFLeague.Game.Entity.Attachable.Skill
{
    [Serializable]
    public class DashData : AttachableEntityData
    {
        public string[] disableComponentId;
        public float distance;
        public float preparationTime;
        public float recoveryTime;
    }

    public class Dash : Attachable<DashData, ChampionData> 
    {
        private ChampionData cd;

        #region implemented abstract members of Attachable

        public override void Awake()
        {
            
        }

        public override void FrameFeed()
        {
            
        }

        public override void Destroy()
        {
            
        }

        #endregion
    }
}