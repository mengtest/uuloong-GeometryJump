using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UULoog.iOS.Xcode;
using UnityEngine;

public class XcodePostProcess {
    #region PostProcessBuild

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path) {

        if (buildTarget == BuildTarget.iOS) {

            string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
            PBXProject proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string target = proj.TargetGuidByName("Unity-iPhone");

            // 1. CocoaPods support.
            proj.AddBuildProperty(target, "HEADER_SEARCH_PATHS", "$(inherited)");
            proj.AddBuildProperty(target, "HEADER_SEARCH_PATHS", "\"$(SRCROOT)/Pods/Headers/\"");
            proj.AddBuildProperty(target, "FRAMEWORK_SEARCH_PATHS", "$(inherited)");
            proj.AddBuildProperty(target, "OTHER_CFLAGS", "$(inherited)");
            proj.AddBuildProperty(target, "OTHER_LDFLAGS", "$(inherited)");

            // DISABLE BITCODE
            proj.AddBuildProperty(target, "ENABLE_BITCODE", "NO");
            proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");

            // ADD OBJC LINKER
            proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");


            // 3. Include the final AppController file.
            foreach (string appFilePath in AppControllerFilePaths) {
                CopyAndReplaceProjectFile(target, proj, appFilePath, Path.Combine(Path.Combine(path, "Classes/"), Path.GetFileName(appFilePath)));
            }

            // 4. Include Podfile into the project root folder.
            foreach (string podFilePath in PodFilePaths)
                CopyAndReplaceFile(podFilePath, Path.Combine(path, Path.GetFileName(podFilePath)));

            File.WriteAllText(projPath, proj.WriteToString());

            // PLIST
            string plistPath = path + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            // Get root
            PlistElementDict rootDict = plist.root;

            PlistElementArray LSApplicationQueriesSchemesArray = rootDict.CreateArray("LSApplicationQueriesSchemes");
            LSApplicationQueriesSchemesArray.AddString("fbauth2");
            LSApplicationQueriesSchemesArray.AddString("fbauth");
            LSApplicationQueriesSchemesArray.AddString("fbapi");
            LSApplicationQueriesSchemesArray.AddString("wechat");
            LSApplicationQueriesSchemesArray.AddString("weixin");
            LSApplicationQueriesSchemesArray.AddString("sinaweibohd");
            LSApplicationQueriesSchemesArray.AddString("sinaweibo");
            LSApplicationQueriesSchemesArray.AddString("sinaweibosso");
            LSApplicationQueriesSchemesArray.AddString("weibosdk");
            LSApplicationQueriesSchemesArray.AddString("weibosdk2.5");
            LSApplicationQueriesSchemesArray.AddString("mqqapi");
            LSApplicationQueriesSchemesArray.AddString("mqq");
            LSApplicationQueriesSchemesArray.AddString("mqqOpensdkSSoLogin");
            LSApplicationQueriesSchemesArray.AddString("mqqconnect");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkdataline");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkgrouptribeshare");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkfriend");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkapi");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkapiV2");
            LSApplicationQueriesSchemesArray.AddString("mqqopensdkapiV3");
            LSApplicationQueriesSchemesArray.AddString("mqzoneopensdk");
            LSApplicationQueriesSchemesArray.AddString("wtloginmqq");
            LSApplicationQueriesSchemesArray.AddString("wtloginmqq2");
            LSApplicationQueriesSchemesArray.AddString("mqqwpa");
            LSApplicationQueriesSchemesArray.AddString("mqzone");
            LSApplicationQueriesSchemesArray.AddString("mqzonev2");
            LSApplicationQueriesSchemesArray.AddString("mqzoneshare");
            LSApplicationQueriesSchemesArray.AddString("wtloginqzone");
            LSApplicationQueriesSchemesArray.AddString("mqzonewx");
            LSApplicationQueriesSchemesArray.AddString("mqzoneopensdkapiV2");
            LSApplicationQueriesSchemesArray.AddString("mqzoneopensdkapi19");
            LSApplicationQueriesSchemesArray.AddString("mqzoneopensdkapi");
            LSApplicationQueriesSchemesArray.AddString("mqzoneopensdk");
            LSApplicationQueriesSchemesArray.AddString("alipay");
            LSApplicationQueriesSchemesArray.AddString("alipayshare");

            PlistElementArray URLTypesArray = rootDict.CreateArray("CFBundleURLTypes");
            PlistElementDict URLTypeDict = URLTypesArray.AddDict();
            URLTypeDict.SetString("CFBundleTypeRole", "Editor");

            PlistElementArray URLSchemesArray = URLTypeDict.CreateArray("CFBundleURLSchemes");
			URLSchemesArray.AddString("wb1228174909");
            URLSchemesArray.AddString("QQ41E95366");
			URLSchemesArray.AddString("wx1626780eec579027");
			URLSchemesArray.AddString("fb872693642867234");

            rootDict.SetString("NSLocationWhenInUseUsageDescription","YES");
            // Write to file
            File.WriteAllText(plistPath, plist.WriteToString());


            StartPodsProcess(path);

        }
    }

    #endregion

    #region Private methods

    internal static void CopyAndReplaceFile(string srcPath, string dstPath) {
        if (File.Exists(dstPath))
            File.Delete(dstPath);

        File.Copy(srcPath, dstPath);
    }

    internal static void CopyAndReplaceProjectFile(string target, PBXProject proj, string srcPath, string dstPath) {
        bool fileExists = File.Exists(dstPath);

        if (fileExists) {
            File.Delete(dstPath);
            // File.Copy(srcPath, dstPath);
        }

        File.Copy(srcPath, dstPath);

        if (!fileExists) {
            string fileName = Path.Combine("Classes", Path.GetFileName(dstPath));
            proj.AddFileToBuild(target, proj.AddFile(fileName, fileName, PBXSourceTree.Source));

        }
    }

    internal static void CopyAndReplaceDirectory(string srcPath, string dstPath) {
        if (Directory.Exists(dstPath))
            Directory.Delete(dstPath);

        if (File.Exists(dstPath))
            File.Delete(dstPath);

        Directory.CreateDirectory(dstPath);

        foreach (var file in Directory.GetFiles(srcPath))
            File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));

        foreach (var dir in Directory.GetDirectories(srcPath))
            CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
    }

    static void StartPodsProcess(string path) {
        var proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = Path.Combine(path, OpenPodsFileName);
        proc.Start();
    }

    #endregion


    #region Paths

    static string[] PodFilePaths {
        get {
            return new[] {
                Path.Combine (PodFolderPath, "Podfile"),
                Path.Combine (PodFolderPath, "pods.command"),
                Path.Combine (PodFolderPath, OpenPodsFileName)
            };
        }
    }

    static string OpenPodsFileName {
        get {
            return "open_pods.command";
        }
    }

    static string[] AppControllerFilePaths {
        get {
            return new[] {
                Path.Combine (AppControllerFolderPath, "main.mm"),
                Path.Combine (AppControllerFolderPath, "AppProxy/EnterUnityViewController.h"),
                Path.Combine (AppControllerFolderPath, "AppProxy/EnterUnityViewController.m"),
                Path.Combine (AppControllerFolderPath, "AppProxy/EnterUnityViewController.xib"),
                Path.Combine (AppControllerFolderPath, "AppProxy/HomeViewController.h"),
                Path.Combine (AppControllerFolderPath, "AppProxy/HomeViewController.m"),
                Path.Combine (AppControllerFolderPath, "AppProxy/HomeViewController.xib"),
                Path.Combine (AppControllerFolderPath, "AppProxy/MyDataManager.h"),
                Path.Combine (AppControllerFolderPath, "AppProxy/MyDataManager.m"),
                Path.Combine (AppControllerFolderPath, "AppProxy/UnitySubAppDelegate.h"),
                Path.Combine (AppControllerFolderPath, "AppProxy/UnitySubAppDelegate.m"),
                Path.Combine (AppControllerFolderPath, "Load/BYSLoading.h"),
                Path.Combine (AppControllerFolderPath, "Load/BYSLoading.m"),
                Path.Combine (AppControllerFolderPath, "Load/LodingViewController.h"),
                Path.Combine (AppControllerFolderPath, "Load/LodingViewController.m"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load1.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load2.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load3.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load4.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load5.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load6.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load7.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load8.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load9.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load10.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load11.png"),
                Path.Combine (AppControllerFolderPath, "Load/icon_load12.png"),
                Path.Combine (AppControllerFolderPath, "Load/loadingTitle.png"),

                Path.Combine (AppControllerFolderPath, "AdcolonyAdaptor/GADMAdapterAdColonyExtras.h"),
                Path.Combine (AppControllerFolderPath, "AdcolonyAdaptor/GADMAdapterAdColonyInitializer.h"),
                Path.Combine (AppControllerFolderPath, "AdcolonyAdaptor/AdapterSDKAdColony.a")
            };
        }
    }

    static string PodFolderPath {
        get {
            return Path.Combine(XCodeFilesFolderPath, "Pod/");
        }
    }

    static string AppControllerFolderPath {
        get {
            return Path.Combine(XCodeFilesFolderPath, "AppController/");
        }
    }


    static string XCodeFilesFolderPath {
        get {
            return Path.Combine(UnityProjectRootFolder, "XCodeFiles/");
        }
    }

    static string UnityProjectRootFolder {
        get {
            return ".";
        }
    }

    #endregion
}

