using System;
using Newtonsoft;
using UnityEngine;
using URFLeague.Game.Entity;
using URFLeague.Game.Entity.Attachable;

namespace URFLeague.Util.Data
{
    public static class DataProvider
    {
        public static IAttachableEntity RequestAttachableEntityFromDataMap(ClassDataMap map)
        {
            return string.IsNullOrEmpty(map.jsonDataPath) ?
                DataProvider.RequestObjectInstance<IAttachableEntity>(map.className) :
                DataProvider.RequestObjectFromJson<IAttachableEntity>(map.className, map.jsonDataPath);
        }

        public static T RequestObjectFromJson<T>(string jsonPath)
        {
            return JsonUtility.FromJson<T>(Resources.Load<TextAsset>(jsonPath).text);
        }

        public static T RequestObjectFromJson<T>(string className, string jsonPath)
        {
            Type type = Type.GetType(className);
            return (T)JsonUtility.FromJson(Resources.Load<TextAsset>(jsonPath).text, type);
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

    [System.Serializable]
    public struct ClassDataMap
    {
        public string className;
        public string dataClassName;
        public string jsonDataPath;
    }
}