using System;
using UnityEngine;
using URFLeague.Config;
using URFLeague.Game.Entity.Attachable.Component;

namespace URFLeague.Game.Entity.Attachable.Skill
{
    [Serializable]
    public class DashData : AttachableEntityData
    {
        public float distance;
        public float speed;
        public float preparationTime;
        public float recoveryTime;

        public float timeCounter;
        public float initialSpeed;
        public bool isPreparingDashing;
        public bool isDashing;
        public bool isRecovering;
    }

    public class DashComponent : Attachable<DashData, ChampionData> 
    {
        #region implemented abstract members of Attachable

        public override void Boot() { }

        public override void Awake() 
        {
            customData.timeCounter = 0f;
            customData.initialSpeed = 0f;
            customData.isDashing = false;
            customData.isPreparingDashing = false;
            customData.isRecovering = false;
        }

        public override void FrameFeed()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !customData.isDashing)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(GameConfig.TERRAIN_TAG))
                    {
                        BuildDashDataAndState(hit.point);
                    }
                }
            }
            else if (customData.isDashing)
            {
                customData.timeCounter += Time.deltaTime;
                if (customData.timeCounter > customData.preparationTime && customData.isPreparingDashing)
                {
                    customData.timeCounter = 0f;
                    customData.isPreparingDashing = false;
                    parentComplexEntity.EnableComponent<MovementComponent>();
                }
                else if ((parentData.currentPosition - parentData.targetPosition).toVector3().sqrMagnitude < GameConfig.MIN_TARGET_POS_DISTANCE && !customData.isRecovering)
                {
                    customData.timeCounter = 0f;
                    customData.isRecovering = true;
                }
                else if (customData.timeCounter > customData.recoveryTime && customData.isRecovering)
                {
                    parentData.movementSpeed = customData.initialSpeed;
                    parentComplexEntity.EnableAllComponents();
                    Destroy();
                }
            }
        }

        public override void Destroy() 
        {
            Awake();
        }

        #endregion

        private void BuildDashDataAndState(Vector3 hitPoint)
        {
            Vector3 dashFinalPos = parentData.currentPosition.toVector3() + (hitPoint - parentData.currentPosition.toVector3()).normalized * customData.distance;
            parentData.targetPosition = new WorldCoordinate 
                                        {
                                            x = dashFinalPos.x,
                                            y = dashFinalPos.y,
                                            z = dashFinalPos.z
                                        };
            customData.initialSpeed = parentData.movementSpeed;
            parentData.movementSpeed = customData.speed;
            
            parentComplexEntity.DisableAllComponents();
            parentComplexEntity.EnableComponent<DashComponent>();
            parentComplexEntity.EnableComponent<PositionComponent>();
            parentComplexEntity.EnableComponent<RenderOrientationComponent>();

            customData.timeCounter = 0f;
            customData.isDashing = true;
            customData.isPreparingDashing = true;
        }
    }
}