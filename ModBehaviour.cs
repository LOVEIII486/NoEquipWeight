using HarmonyLib;
using UnityEngine;
using System;

namespace NoEquipWeight
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        public static ModBehaviour Instance { get; private set; }
        
        private const string HarmonyId = "com.LOVEIII486.NoEquipWeight"; 
        private const string LogTag = "[NoEquipWeight]";
        private Harmony _harmony;
        private bool _isPatched = false;

        private void OnEnable()
        {
            Instance = this;
            
            if (HarmonyLoad.LoadHarmony() == null)
            {
                Debug.LogError($"{LogTag} 模组启动失败: 缺少 Harmony 依赖。");
                return;
            }
            
            InitializeHarmonyPatches();
            Debug.Log($"{LogTag} 模组已启用");
        }

        protected override void OnAfterSetup()
        {
            base.OnAfterSetup();
            // 如果以后需要增加配置菜单，可以在这里初始化
        }

        private void OnDisable()
        {
            CleanupHarmonyPatches();
            Instance = null;
            Debug.Log($"{LogTag} 模组已禁用");
        }

        #region Harmony

        private void InitializeHarmonyPatches()
        {
            if (_isPatched) return;
            
            try
            {
                if (_harmony == null)
                {
                    _harmony = new Harmony(HarmonyId);
                }
                _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
                _isPatched = true;
                Debug.Log($"{LogTag} Harmony 补丁应用成功");
            }
            catch (Exception ex)
            {
                Debug.LogError($"{LogTag} Harmony 补丁应用失败: {ex}");
            }
        }

        private void CleanupHarmonyPatches()
        {
            if (!_isPatched || _harmony == null) return;

            try
            {
                _harmony.UnpatchAll(HarmonyId);
                _isPatched = false;
                Debug.Log($"{LogTag} Harmony 补丁已移除");
            }
            catch (Exception ex)
            {
                Debug.LogError($"{LogTag} 移除 Harmony 补丁时发生错误: {ex}");
            }
        }

        #endregion
    }
}