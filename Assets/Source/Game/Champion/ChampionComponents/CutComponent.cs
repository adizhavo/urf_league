using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using URFLeague.Util;
using URFLeague.Game;
using URFLeague.Config;
using URFLeague.Game.Entity.Attachable.Component;

namespace URFLeague.Game.Entity.Attachable.Skill
{
    [Serializable]
    public class CutData : AttachableEntityData
    {
        [JsonConverter(typeof(StringEnumConverter))] 
        public PooledObject FXname;
        public WorldCoordinate FXlocalPosition;
        public WorldCoordinate FXrotationOffset;
        public float disableTime;
        public float speedScale;

        public float timeCounter;
        public GameObject fxObject;
    }

    public class CutComponent : Attachable<CutData, ChampionData>
    {
        #region implemented abstract members of Attachable

        public override void Boot() { }

        public override void Awake() 
        {
            customData.timeCounter = 0f;
        }

        public override void FrameFeed() 
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(GameConfig.TERRAIN_TAG))
                    {
                        Cut(hit.point);
                    }
                }
            }
        }

        public override void Destroy() { }

        #endregion

        private void Cut(Vector3 hitPoint)
        {
            Vector3 dashFinalPos = parentData.currentPosition.toVector3() + (hitPoint - parentData.currentPosition.toVector3()).normalized * GameConfig.MIN_TARGET_POS_DISTANCE;
            parentData.targetPosition = new WorldCoordinate 
                                        {
                                            x = dashFinalPos.x,
                                            y = dashFinalPos.y,
                                            z = dashFinalPos.z
                                        };
            parentComplexEntity.GetComponent<MovementComponent>().FrameFeed();
            parentComplexEntity.GetComponent<RenderOrientationComponent>().FrameFeed();

            ShowCutFX();
        }

        private void ShowCutFX()
        {
            customData.fxObject = PoolProvider.Instance.RequestGameObject(customData.FXname);
            Vector3 localPositionOfFX = customData.FXlocalPosition.toVector3();
            customData.fxObject.transform.position = parentData.gameObject.transform.TransformPoint(localPositionOfFX);
            customData.fxObject.transform.eulerAngles = parentData.gameObject.transform.eulerAngles + customData.FXrotationOffset.toVector3();
            AutoDisable ad = customData.fxObject.GetComponent<AutoDisable>();
            if (ad != null)
                ad.lifeTime = customData.disableTime;
            else
                customData.fxObject.AddComponent<AutoDisable>().lifeTime = customData.disableTime;
        }
    }
}