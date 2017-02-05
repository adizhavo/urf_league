using System;
using UnityEngine;

namespace URFLeague.Game.Entity.Attachable.Skill
{
    [Serializable]
    public class DashData : AttachableEntityData
    {
        public string[] disableComponentId;
        public float distance;
        public float speed;
        public float preparationTime;
        public float recoveryTime;
    }

    public class Dash : Attachable<DashData, ChampionData> 
    {
        private IComplexEntity complexEntity;

        #region implemented abstract members of Attachable

        public override IAttachableEntity AttachTo(IComplexEntity parent)
        {
            complexEntity = parent;
            return base.AttachTo(parent);
        }

        public override void Awake() { }

        public override void FrameFeed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                complexEntity.DisableAllComponentsExcept<Dash>();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                complexEntity.EnableAllComponents();
            }
        }

        public override void Destroy() { }

        #endregion
    }
}