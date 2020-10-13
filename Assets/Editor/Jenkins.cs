﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

class Jenkins
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = "PCG";
    static string TARGET_DIR = "target";

    [MenuItem("Build Windows")]
    static void BuildWindows64()
    {
        string targetDir = APP_NAME + ".app";
        GenericBuild(SCENES, TARGET_DIR + "/" + targetDir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    private static string[] FindEnabledEditorScenes()
    {
        return (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
    }

    static void GenericBuild(string[] scenes, string targetDir, BuildTarget buildTarget, BuildOptions buildOptions)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);
        string res = BuildPipeline.BuildPlayer(scenes, targetDir, buildTarget, buildOptions).ToString();

        if (res.Length > 0)
        {
            throw new Exception("BuildPlayer failure: " + res);
        }
    }
}
