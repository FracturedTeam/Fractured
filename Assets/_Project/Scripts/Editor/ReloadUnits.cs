using System;
using System.Collections.Generic;
using System.IO;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditor : EditorWindow
{
    private string emplacement = "Assets/DialogueSheet.csv";
    private string outputA1 = "Assets/";
    private string outputA2 = "Assets/";
    private string outputA3 = "Assets/";
    private string outputA4 = "Assets/";
    private string outputA5 = "Assets/";
    private string outputT1 = "Assets/";
    private string outputT2 = "Assets/";
    private string outputT3 = "Assets/";
    private string outputT4 = "Assets/";
    
    private string otherEmplacement = "Assets/";
    [MenuItem("Window/Reload Dialogue")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<DialogueEditor>();
        wnd.titleContent = new GUIContent("DialogueEditor"); 
    }

  public void OnGUI()
  {
      GUILayout.Label("Path to the datatable : ");
      emplacement = GUILayout.TextField(emplacement, 128);
      
      GUILayout.Label("Path to the output folder Atelier 1 :");
      outputA1 = GUILayout.TextField(outputA1, 128);
      GUILayout.Label("Path to the output folder Atelier 2 :");
      outputA2 = GUILayout.TextField(outputA2, 128);
      GUILayout.Label("Path to the output folder Atelier 3 :");
      outputA3 = GUILayout.TextField(outputA3, 128);
      GUILayout.Label("Path to the output folder Atelier 4 :");
      outputA4 = GUILayout.TextField(outputA4, 128);
      GUILayout.Label("Path to the output folder Atelier 5 :");
      outputA5 = GUILayout.TextField(outputA5, 128);
      
      GUILayout.Label("Path to the output folder Transition 1-2 :");
      outputT1 = GUILayout.TextField(outputT1, 128);
      GUILayout.Label("Path to the output folder Transition 2-3 :");
      outputT2 = GUILayout.TextField(outputT2, 128);
      GUILayout.Label("Path to the output folder Transition 3-4 :");
      outputT3 = GUILayout.TextField(outputT3, 128);
      GUILayout.Label("Path to the output folder Transition 4-5 :");
      outputT4 = GUILayout.TextField(outputT4, 128);
      
      GUILayout.Label("Path to the output folder other :");
      otherEmplacement = GUILayout.TextField(otherEmplacement, 128);
      
      if (GUILayout.Button("Reload"))
      {
          if(!AssetDatabase.LoadAssetAtPath(emplacement, typeof(TextAsset)))
          {
              Debug.LogError("ERROR : no unit sheet found");
              return;
          }
         
          var dataset = (TextAsset)AssetDatabase.LoadAssetAtPath(emplacement, typeof(TextAsset)) ;
          var dataLines = dataset.text.Split('\n');
          var dataCol =dataset.text.Split(";");
          
          for(var i = 1; i < dataLines.Length; i++) {
              if(dataLines[i] == null) return;
              var data = dataLines[i].Split(";");
              if(data[0] == null ||data[0] == " ") return;
              
              if(dataLines[i].Split(";").Length < 5) return;

              var soName = "SO";

              soName += (dataLines[i].Split(";")[0]) switch //Atelier
              {
                  "Atelier 1" => "_1",
                  "Atelier 2" => "_2",
                  "Atelier 3" => "_3",
                  "Atelier 4" => "_4",
                  "Atelier 5" => "_5",
                  
                  "Transition 1-2" => "_1T2",
                  "Transition 2-3" => "_2T3",
                  "Transition 3-4" => "_3T4",
                  "Transition 4-5" => "_4T5",
                  _ => throw new ArgumentOutOfRangeException()
              };
              
              var output= (dataLines[i].Split(";")[0]) switch //Atelier
              {
                  "Atelier 1" => Directory.Exists(outputA1) ? outputA1 : otherEmplacement,
                  "Atelier 2" => Directory.Exists(outputA2) ? outputA2 : otherEmplacement,
                  "Atelier 3" => Directory.Exists(outputA3) ? outputA3 : otherEmplacement,
                  "Atelier 4" => Directory.Exists(outputA4) ? outputA4 : otherEmplacement,
                  "Atelier 5" => Directory.Exists(outputA5) ? outputA5 : otherEmplacement,
                  
                  "Transition 1-2" => Directory.Exists(outputT1) ? outputT1 : otherEmplacement,
                  "Transition 2-3" => Directory.Exists(outputT2) ? outputT2 : otherEmplacement,
                  "Transition 3-4" => Directory.Exists(outputT3) ? outputT3 : otherEmplacement,
                  "Transition 4-5" => Directory.Exists(outputT4) ? outputT4 : otherEmplacement,
                  _ => otherEmplacement
              };
              
              soName += dataLines[i].Split(";")[1] == "Scene 1" ? "_1" :  dataLines[i].Split(";")[1] == "Scene 2" ? "_2" : "_3"; //Scene
              
              if(dataLines[i].Split(";")[5] == "Thought")
              {
                  soName += "_Thought" + $"_{dataLines[i].Split(";")[4]}";
                  
                  if(File.Exists($"{output}{soName}.asset"))
                  {
                      Debug.LogWarning($"Asset Modified at {output}{soName}.asset, beware of type mismatch on scripts");
                      
                      var currentData = (GlassTextScriptableObject)AssetDatabase.LoadAssetAtPath($"{output}{soName}.asset", typeof(GlassTextScriptableObject)) ;
                      
                      currentData.baseText = dataLines[i].Split(";")[8]; //Normal Text
                      currentData.fragAText = dataLines[i].Split(";")[9]; //A Text
                      currentData.fragBText = dataLines[i].Split(";")[10]; //B Text
                      currentData.bothText = dataLines[i].Split(";")[11]; //AB Text
                      
                      EditorUtility.SetDirty(currentData);
                      AssetDatabase.SaveAssets();
                      AssetDatabase.Refresh();
                      return;
                  }
                  
                  AssetDatabase.CreateAsset(CreateGlassTextScriptableObject(dataLines, i, data), $"{output}{soName}.asset");
                  AssetDatabase.SaveAssets();
                  AssetDatabase.Refresh();
              }
              
              if(dataLines[i].Split(";")[5] == "Inspect")
              {
                  soName += "_Inspect"+ $"_{dataLines[i].Split(";")[4]}";
                  
                  if(File.Exists($"{output}{soName}.asset"))
                  {
                      Debug.LogWarning($"Asset Modified at {output}{soName}.asset, beware of type mismatch on scripts");
                      
                      var currentData = (GlassDocumentScriptableObject)AssetDatabase.LoadAssetAtPath($"{output}{soName}.asset", typeof(GlassDocumentScriptableObject)) ;
                      
                      currentData.baseText = dataLines[i].Split(";")[8]; //Normal Text
                      currentData.fragAText = dataLines[i].Split(";")[9]; //A Text
                      currentData.fragBText = dataLines[i].Split(";")[10]; //B Text
                      currentData.bothText = dataLines[i].Split(";")[11]; //AB Text
                      
                      currentData.type = dataLines[i].Split(";")[7] == "Portrait" ? DocumentTypes.portrait :
                          dataLines[i].Split(";")[7] == "Landscape" ? DocumentTypes.landscape :
                          DocumentTypes.square; //FORMAT
                      
                      EditorUtility.SetDirty(currentData);
                      AssetDatabase.SaveAssets();
                      AssetDatabase.Refresh();
                      return;
                  }
                  
                  AssetDatabase.CreateAsset(CreateGlassDocumentScriptableObjectElement(dataLines, i, data), $"{output}{soName}.asset");
                  AssetDatabase.SaveAssets();
          
                  AssetDatabase.SaveAssets();
                  AssetDatabase.Refresh();
              }
              
              if(dataLines[i].Split(";")[5] == "Dialogue")
              {
                  soName += "_Dialogue" + $"_{dataLines[i].Split(";")[4]}";
                  
                  if(File.Exists($"{output}{soName}.asset"))
                  {
                      Debug.LogWarning($"Asset Modified at {output}{soName}.asset, beware of type mismatch on scripts");
                      
                      var currentData = (DialogueScriptableObject)AssetDatabase.LoadAssetAtPath($"{output}{soName}.asset", typeof(DialogueScriptableObject)) ;
                      
                      currentData.dialogue = dataLines[i].Split(";")[8];
                      currentData.time = int.Parse(dataLines[i].Split(";")[6]);
                      
                      EditorUtility.SetDirty(currentData);
                      AssetDatabase.SaveAssets();
                      AssetDatabase.Refresh();
                      return;
                  }
                  
                  AssetDatabase.CreateAsset(CreateBasicTextScriptableObject(dataLines, i, data), $"{output}{soName}.asset");
                  AssetDatabase.SaveAssets();
                  AssetDatabase.Refresh();
              }
          }
      }
  }

  private GlassTextScriptableObject CreateGlassTextScriptableObject(string[] dataLines, int i, string[] data)
  {
      GlassTextScriptableObject newElement = CreateInstance<GlassTextScriptableObject>();
      for (var y = 8; y < dataLines[i].Split(";").Length-1; y++)
      {
          if (y == 8) newElement.baseText = dataLines[i].Split(";")[y]; //Normal Text
          if (y == 9) newElement.fragAText = dataLines[i].Split(";")[y]; //A Text
          if (y == 10) newElement.fragBText = dataLines[i].Split(";")[y]; //B Text
          if (y == 11) newElement.bothText = dataLines[i].Split(";")[y]; //AB Text
      }
      return newElement;
  }
  
  private GlassDocumentScriptableObject CreateGlassDocumentScriptableObjectElement(string[] dataLines, int i, string[] data)
  {
      GlassDocumentScriptableObject newElement = CreateInstance<GlassDocumentScriptableObject>();
      for (var y = 7; y < dataLines[i].Split(";").Length-1; y++)
      {
          if (y == 7) //FORMAT
              newElement.type = dataLines[i].Split(";")[y] == "Portrait" ? DocumentTypes.portrait :
                  dataLines[i].Split(";")[y] == "Landscape" ? DocumentTypes.landscape : DocumentTypes.square;

          if (y == 8) newElement.baseText = dataLines[i].Split(";")[y]; //Normal Text
          if (y == 9) newElement.fragAText = dataLines[i].Split(";")[y]; //A Text
          if (y == 10) newElement.fragBText = dataLines[i].Split(";")[y]; //B Text
          if (y == 11) newElement.bothText = dataLines[i].Split(";")[y]; //AB Text
      }
      return newElement;
  }
  private DialogueScriptableObject CreateBasicTextScriptableObject(string[] dataLines, int i, string[] data)
  {
      DialogueScriptableObject newElement = CreateInstance<DialogueScriptableObject>();

      newElement.dialogue = dataLines[i].Split(";")[8];
      newElement.time = int.Parse(dataLines[i].Split(";")[6]);
      return newElement;
  }

}