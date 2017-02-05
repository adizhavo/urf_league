using System;
using UnityEngine;
using URFLeague.Config;

namespace URFLeague.Game.Entity.Component
{
    public class MouseControlComponent : EntityComponent
    {
        private ChampionData cd;

        public MouseControlComponent(string cmpId, IEntity parent) : base(cmpId, parent) { }

        #region implemented abstract members of EntityComponent
        public override void Boot()
        {
            cd = ((ChampionData)componentData.parentData);

            if (cd == null)
                throw new InvalidCastException("IEntity data is not a champion data");
        }

        public override void Awake() { }

        public override void FrameFeed()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(GameConfig.TERRAIN_TAG))
                    {
                        cd.targetPosition = new WorldCoordinate
                                                {
                                                    x = hit.point.x, 
                                                    y = hit.point.y, 
                                                    z = hit.point.z
                                                };
                    }
                }
            }
        }

        public override void Destroy() { }
        #endregion
    }
}