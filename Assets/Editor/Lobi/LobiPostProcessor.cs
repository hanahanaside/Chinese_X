using UnityEngine;
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using Lobi.XCodeEditor;

/// <summary>
/// Unityのビルド後に実行する処理（XCodeのプロジェクト設定など）
/// </summary>
public static class LobiPostProcessor
{
	private const string URL_SCHEME_PREFIX_LOBI = "nakamapapp-";

	/// <summary>
	/// Raises the post process build event.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="path">出力先のパス（ビルド時に指定するパス）</param>
	[PostProcessBuild (100)]
	public static void OnPostProcessBuild (BuildTarget target, string path)
	{
		LobiSettings settings = (LobiSettings)Resources.Load (LobiSettingsEditor.settingsFile);

		if (settings == null) {
			Debug.Log ("Lobi SDK auto build settings will be disabled because client id or base name was not valid.\nLobi SDK Settings: [Edit]-[Lobi Settings]");
			return;
		}

		if (settings == null || settings.IsEnabled == false) {
			return;
		}

		if (settings.IsValid) {
			if (target == BuildTarget.iPhone) {
				PostProcessBuild_iOS (path, settings);
			} else if (target == BuildTarget.Android) {
				// TODO: Android
				//PostProcessBuild_Android(path, settings.clientId);
			}
		} else {
			Debug.LogError ("Lobi will be disabled because client id or base name was not valid.");
		}
	}

	/// <summary>
	/// iOSのビルド設定自動化処理
	/// </summary>
	/// <param name="path">出力先のパス（ビルド時に指定するパス）</param>
	/// <param name="settings">Lobiの設定</param>
	private static void PostProcessBuild_iOS (string path, LobiSettings settings)
	{
		// ObjC ファイルを書き換える
		ReWriteObjCFiles (path, settings);

		// XCode プロジェクトファイルの設定をする
		CreateModFile (path, settings);
		ProcessXCodeProject (path);

		// Info.plist ファイルを書き換える
		ReWriteInfoPList (path, settings.clientId);
	}

	/// <summary>
	/// ObjC ファイルを書き換える
	/// </summary>
	/// <param name="path">出力先のパス（ビルド時に指定するパス）</param>
	/// <param name="settings">Lobiの設定</param>
	private static void ReWriteObjCFiles (string path, LobiSettings settings)
	{
		// LobiUnityAppController.mm ファイルを書き換える
		string filePath = System.IO.Path.Combine (path, "Libraries/LobiUnityAppController.mm");
		if (File.Exists (filePath)) {
			UpdateStringInFile (filePath, "[LobiCore setupClientId:@\"\"", "[LobiCore setupClientId:@\"" + settings.clientId + "\"");
			UpdateStringInFile (filePath, "accountBaseName:@\"\"];", "accountBaseName:@\"" + settings.baseName + "\"];");

			if (settings.chatEnabled == false) {
				UpdateStringInFile (filePath, "#define LOBI_CHAT", "");
			}

			if (settings.recEnabled == false) {
				UpdateStringInFile (filePath, "#define LOBI_REC", "");
			}
		}	
	}

	/// <summary>
	/// Mod ファイルの作成
	/// </summary>
	/// <param name="path">出力先のパス（ビルド時に指定するパス）</param>
	/// <param name="settings">Lobiの設定</param>
	private static void CreateModFile (string path, LobiSettings settings)
	{
		Dictionary<string, object> mod = new Dictionary<string, object> ();
		
		List<string> patches = new List<string> ();
		List<string> librarysearchpaths = new List<string> ();
		List<string> folders = new List<string> ();
		List<string> excludes = new List<string> ();

		// libs
		List<string> libs = new List<string> ();
		libs.Add ("libsqlite3.0.dylib");

		// フレームワークサーチパス
		List<string> frameworksearchpaths = new List<string> ();
		frameworksearchpaths.Add ("Plugins/Lobi/iOS");

		// フレームワーク
		List<string> frameworks = new List<string> ();
		frameworks.Add ("OpenGLES.framework");
		frameworks.Add ("QuartzCore.framework");
		frameworks.Add ("MediaPlayer.framework");
		frameworks.Add ("MessageUI.framework");
		frameworks.Add ("CoreData.framework");
		frameworks.Add ("CoreMedia.framework");

		frameworks.Add ("CoreImage.framework:weak");
		frameworks.Add ("StoreKit.framework:weak");
		frameworks.Add ("AVFoundation.framework:weak");
		frameworks.Add ("Foundation.framework:weak");
		frameworks.Add ("AudioToolbox.framework:weak");
		frameworks.Add ("AssetsLibrary.framework:weak");
		frameworks.Add ("UIKit.framework:weak");

		// files
		List<string> files = new List<string> ();
		AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiCore.framework");
		AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiCore.bundle");
		if (settings.chatEnabled == true) {
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiChat.framework");
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiChat.bundle");
		}
		if (settings.recEnabled == true) {
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiRec.framework");
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiRec.bundle");
		}
		if (settings.rankingEnabled == true) {
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiRanking.framework");
			AddFrameworkFile (ref files, "Plugins/Lobi/iOS/LobiRanking.bundle");
		}

		// headerpaths
		List<string> headerpaths = new List<string> ();
		headerpaths.Add ("Plugins/Lobi/iOS");

		// ビルド設定
		Dictionary<string,List<string>> buildSettings = new Dictionary<string,List<string>> ();
		List<string> otherLinkerFlags = new List<string> ();
		otherLinkerFlags.Add ("-ObjC");
		buildSettings.Add ("OTHER_LDFLAGS", otherLinkerFlags);
		
		mod.Add ("group", "Lobi");
		mod.Add ("patches", patches);
		mod.Add ("libs", libs);
		mod.Add ("librarysearchpaths", librarysearchpaths);
		mod.Add ("frameworksearchpaths", frameworksearchpaths);
		mod.Add ("frameworks", frameworks);
		mod.Add ("headerpaths", headerpaths);
		mod.Add ("files", files);
		mod.Add ("folders", folders);
		mod.Add ("excludes", excludes);
		mod.Add ("buildSettings", buildSettings);

		// mod から projmods ファイルを生成する
		string jsonMod = LobiMiniJSON.Json.Serialize (mod);
		
		string modPath = System.IO.Path.Combine (path, "Lobi");
		string file = System.IO.Path.Combine (modPath, "LobiXCode.projmods");
		
		if (!Directory.Exists (modPath)) {
			Directory.CreateDirectory (modPath);
		}
		if (File.Exists (file)) {
			File.Delete (file);
		}
		
		using (StreamWriter streamWriter = File.CreateText (file)) {
			streamWriter.Write (jsonMod);
		}
	}

	private static void AddFrameworkFile (ref List<string> files, String filePath)
	{
		string fileFullPath = System.IO.Path.Combine (Application.dataPath, filePath);
		if (Directory.Exists (fileFullPath)) {
			files.Add (filePath);
		} else {
			//			Debug.LogWarning ("Lobi SDK: file not found \"" + filePath + "\"");
		}
	}

	/// <summary>
	/// projmods ファイルの設定値を XCode プロジェクト設定へ反映する
	/// </summary>
	/// <param name="path">出力先のパス（ビルド時に指定するパス）</param>
	private static void ProcessXCodeProject (string path)
	{
		XCProject project = new XCProject (path);
		
		string modsPath = System.IO.Path.Combine (path, "Lobi");
		string[] files = System.IO.Directory.GetFiles (modsPath, "*.projmods", System.IO.SearchOption.AllDirectories);
		
		foreach (string file in files) {
			project.ApplyMod (Application.dataPath, file);
		}
		
		project.Save ();
	}

	// Info.plist を書き換える
	private static void ReWriteInfoPList (string path, string clientId)
	{
		try {
			string file = System.IO.Path.Combine (path, "Info.plist");
			
			if (!File.Exists (file)) {
				return;
			}
			
			XmlDocument xmlDocument = new XmlDocument ();
			
			xmlDocument.Load (file);
			
			XmlNode dict = xmlDocument.SelectSingleNode ("plist/dict");
			
			if (dict != null) {
				
				// Add url schemes				
				PListItem bundleUrlTypes = GetPlistItem (dict, "CFBundleURLTypes");
				
				if (bundleUrlTypes == null) {
					XmlElement key = xmlDocument.CreateElement ("key");
					key.InnerText = "CFBundleURLTypes";
					
					XmlElement array = xmlDocument.CreateElement ("array");
					
					bundleUrlTypes = new PListItem (dict.AppendChild (key), dict.AppendChild (array));
				}
				
				AddUrlScheme (xmlDocument, bundleUrlTypes.itemValueNode, URL_SCHEME_PREFIX_LOBI + clientId);
				
				xmlDocument.Save (file);
				
				// Remove extra gargabe added by the XmlDocument save
				UpdateStringInFile (file, "dtd\"[]>", "dtd\">");
			} else {
				Debug.Log ("Info.plist is not valid");
			}
		} catch (Exception e) {
			Debug.Log ("Unable to update Info.plist: " + e);
		}
	}

	/// <summary>
	/// Info.plist に URL Scheme を設定する
	/// </summary>
	/// <param name="xmlDocument">Xml document.</param>
	/// <param name="dictContainer">Dict container.</param>
	/// <param name="urlScheme">URL scheme.</param>
	private static void AddUrlScheme (XmlDocument xmlDocument, XmlNode dictContainer, string urlScheme)
	{
		if (!CheckIfUrlSchemeExists (dictContainer, urlScheme)) {
			XmlElement dict = xmlDocument.CreateElement ("dict");
			
			XmlElement str = xmlDocument.CreateElement ("string");
			str.InnerText = urlScheme;
			
			XmlElement key = xmlDocument.CreateElement ("key");
			key.InnerText = "CFBundleURLSchemes";
			
			XmlElement array = xmlDocument.CreateElement ("array");
			array.AppendChild (str);
			
			dict.AppendChild (key);
			dict.AppendChild (array);
			
			dictContainer.AppendChild (dict);
		}
	}

	/// <summary>
	/// URL Scheme が設定済みか判定する
	/// </summary>
	/// <returns><c>true</c>, if if URL scheme exists was checked, <c>false</c> otherwise.</returns>
	/// <param name="dictContainer">Dict container.</param>
	/// <param name="urlScheme">URL scheme.</param>
	private static bool CheckIfUrlSchemeExists (XmlNode dictContainer, string urlScheme)
	{
		foreach (XmlNode dict in dictContainer.ChildNodes) {
			if (dict.Name.ToLower ().Equals ("dict")) {
				PListItem bundleUrlSchemes = GetPlistItem (dict, "CFBundleURLSchemes");
				
				if (bundleUrlSchemes != null) {
					if (bundleUrlSchemes.itemValueNode.Name.Equals ("array")) {
						foreach (XmlNode str in bundleUrlSchemes.itemValueNode.ChildNodes) {
							if (str.Name.Equals ("string")) {
								if (str.InnerText.Equals (urlScheme)) {
									return true;
								}
							} else {
								Debug.Log ("CFBundleURLSchemes array contains illegal elements.");
							}
						}
					} else {
						Debug.Log ("CFBundleURLSchemes contains illegal elements.");
					}
				}
			} else {
				Debug.Log ("CFBundleURLTypes contains illegal elements.");
			}
		}
		
		return false;
	}

	private class PListItem
	{
		public XmlNode itemKeyNode;
		public XmlNode itemValueNode;

		public PListItem (XmlNode keyNode, XmlNode valueNode)
		{
			itemKeyNode = keyNode;
			itemValueNode = valueNode;
		}
	}

	private static PListItem GetPlistItem (XmlNode dict, string name)
	{
		for (int i = 0; i < dict.ChildNodes.Count - 1; i++) {
			XmlNode node = dict.ChildNodes.Item (i);
			
			if (node.Name.ToLower ().Equals ("key") && node.InnerText.ToLower ().Equals (name.Trim ().ToLower ())) {
				XmlNode valueNode = dict.ChildNodes.Item (i + 1);
				
				if (!valueNode.Name.ToLower ().Equals ("key")) {
					return new PListItem (node, valueNode);
				} else {
					Debug.Log ("Value for key missing in Info.plist");
				}
			}
		}
		
		return null;
	}

	private static void UpdateStringInFile (string file, string subject, string replacement)
	{
		try {
			if (!File.Exists (file)) {
				return;
			}
			
			string processedContents = "";
			
			using (StreamReader sr = new StreamReader (file)) {
				while (sr.Peek () >= 0) {
					string line = sr.ReadLine ();
					processedContents += line.Replace (subject, replacement) + "\n";
				}
			}
			
			File.Delete (file);
			
			using (StreamWriter streamWriter = File.CreateText (file)) {
				streamWriter.Write (processedContents);
			}
		} catch (Exception e) {
			Debug.Log ("Unable to update string in file: " + e);
		}
	}

}
