﻿using System;
using System.Collections.Generic;
using URFLeague.Game.Factory;
using URFLeague.Game.Entity.Component;
using URFLeague.Game.Entity.Skill;

namespace URFLeague.Game.Entity
{
    public class Champion : IEntity
    {   
        public ChampionData championStats;

        public string[] componentsId;
        public string[] skillsId;

        private List<EntityComponent> components = new List<EntityComponent>();
        private List<EntitySkill> skills = new List<EntitySkill>();

        #region IEntity implementation

        public IEntityData data 
        {
            get { return championStats; }
        }

        public void Boot()
        {
            foreach(var id in componentsId)
                components.Add(EntityComponentFactory.RequestComponent(id, this));

            foreach(var cmp in components)
                cmp.Boot();
        }

        public void Awake()
        {
            foreach(var cmp in components)
                cmp.Awake();
        }

        public void FrameFeed()
        {
            foreach(var cmp in components)
                cmp.FrameFeed();
        }

        public void Destroy()
        {
            foreach(var cmp in components)
                cmp.Destroy();
        }
        #endregion
    }
}