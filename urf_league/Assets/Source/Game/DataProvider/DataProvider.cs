﻿using System;
using Newtonsoft;
using UnityEngine;
using System.Reflection;

namespace URFLeague.Util.Data
{
    public static class DataProvider
    {
        public static T RequestObjectFromJson<T>(string jsonPath)
        {
            return JsonUtility.FromJson<T>(Resources.Load<TextAsset>(jsonPath).text);
        }

        public static T RequestObjectInstance<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            if (type != null)
                return (T)Activator.CreateInstance(type);
            
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(className);
                if (type != null)
                    return (T)Activator.CreateInstance(type);
            }

            return null;
        }
    }
}