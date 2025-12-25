using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NoEquipWeight
{
    /// <summary>
    /// 负责检测环境是否已加载 Harmony 依赖
    /// </summary>
    public static class HarmonyLoad
    {
        private static Assembly _harmonyAssembly;

        public static Assembly LoadHarmony()
        {
            if (_harmonyAssembly != null) return _harmonyAssembly;

            _harmonyAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a =>
                {
                    string name = a.GetName().Name;
                    return name.Equals("0Harmony", StringComparison.OrdinalIgnoreCase) ||
                           name.Equals("HarmonyLib", StringComparison.OrdinalIgnoreCase);
                });

            if (_harmonyAssembly == null)
            {
                Debug.LogError("严重错误: 环境中未找到 Harmony 库！");
            }
            
            return _harmonyAssembly;
        }
    }
}