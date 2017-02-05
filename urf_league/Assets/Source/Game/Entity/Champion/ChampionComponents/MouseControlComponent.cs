using System;
using UnityEngine;
using URFLeague.Config;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Game.Entity.Attachable.Component
{
    public class MouseControlComponent : Attachable<AttachableEntityData>
    {
        private ChampionData cd;

        #region implemented abstract members of Attachable
        public override void Boot()
        {
            cd = ((ChampionData)adata.parentData);

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