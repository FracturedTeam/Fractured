using System.Collections.Generic;
using System.IO;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditor : EditorWindow
{
    private string emplacement = "Assets/DialogueSheet.csv";
    private string output = "Assets/";
    [MenuItem("Window/Reload Dialogue")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<DialogueEditor>();
        wnd.titleContent = new GUIContent("DialogueEditor"); 
    }

  public void OnGUI()
  {
      GUILayout.Label("Path to the datatable : ");
      emplacement = GUILayout.TextField(emplacement, 25);
      GUILayout.Label("Path to the output folder :");
      output = GUILayout.TextField(output, 25);
      
      if (GUILayout.Button("Reload"))
      {
          if(!AssetDatabase.LoadAssetAtPath(emplacement, typeof(TextAsset)))
          {
              Debug.LogError("ERROR : no unit sheet found");
              return;
          }
          if(!Directory.Exists(output))
          {
              Debug.LogError("ERROR : output folder does not exist or is incorrect");
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
              soName += dataLines[i].Split(";")[0] == "Atelier 1" ? "_1" :  dataLines[i].Split(";")[0] == "Atelier 2" ? "_2" : "_3"; //Atelier
              soName += dataLines[i].Split(";")[1] == "Scene 1" ? "_1" :  dataLines[i].Split(";")[1] == "Scene 2" ? "_2" : "_3"; //Scene
              
              if(dataLines[i].Split(";")[5] == "Dialogue")
              {
                  soName += "_Dialogue" + $"_{dataLines[i].Split(";")[4]}";
                  
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
          }
          
          //EditorUtility.SetDirty(profil);
          
          
         
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
}