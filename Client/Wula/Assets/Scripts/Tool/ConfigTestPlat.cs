using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ConfigTestPlat 
{
    [MenuItem("选择平台操控模式/PC")]
    public static void SetPC_Mode()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "PC_CTRL_MODE");
    }

    [MenuItem("选择平台操控模式/Phone")]
    public static void SetPhone_Mode()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "PHONE_CTRL_MODE");

    }
}
