using System.Collections.Generic;
using _Project.Scripts.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueEditor : EditorWindow
{
    private string emplacement = "Assets/DialogueSheet.csv";
    [MenuItem("Window/Reload Dialogue")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<DialogueEditor>();
        wnd.titleContent = new GUIContent("DialogueEditor"); 
    }

  public void OnGUI()
  {
      emplacement = GUILayout.TextField(emplacement, 25);
      if (GUILayout.Button("Reload"))
      {
          if(!AssetDatabase.LoadAssetAtPath(emplacement, typeof(TextAsset)))
          {
              Debug.Log("ERROR : no unit sheet found");
              return;
          }
          var dataset = (TextAsset)AssetDatabase.LoadAssetAtPath(emplacement, typeof(TextAsset)) ;
          Debug.Log(dataset);
          var dataLines = dataset.text.Split('\n');
          var dataCol =dataset.text.Split(";");
          
          for(var i = 1; i < dataLines.Length; i++) {
              var data = dataLines[i].Split(";");
              if(data[0] == null ||data[0] == " ") return;
              
              if(dataLines[i].Split(";")[5] == "Dialogue")
                  CreateGlassTextScriptableObject(dataLines, i, data);
              
              if(dataLines[i].Split(";")[5] == "Thought")
                  CreateGlassDocumentScriptableObjectElement(dataLines, i, data);
              
          }
          
          //EditorUtility.SetDirty(profil);
          AssetDatabase.SaveAssets();
          AssetDatabase.Refresh();
      }
  }

  private GlassTextScriptableObject CreateGlassTextScriptableObject(string[] dataLines, int i, string[] data)
  {
      GlassTextScriptableObject newElement = CreateInstance<GlassTextScriptableObject>();
      string elementName = "";
      for (var y = 0; y < dataLines[i].Split(";").Length-1; y++)
      {
          if (y == 0) elementName += dataLines[i].Split(";")[y]; //ATELIER
          if (y == 1) elementName += dataLines[i].Split(";")[y]; //SCENE
          if (y == 2) elementName += dataLines[i].Split(";")[y]; //ROOM
          if (y == 3) elementName += dataLines[i].Split(";")[y]; //ELEMENT
          if (y == 4) elementName += dataLines[i].Split(";")[y]; //NAME
          
          

      }
      Debug.Log(elementName);

      return newElement;
  }
  
  private GlassDocumentScriptableObject CreateGlassDocumentScriptableObjectElement(string[] dataLines, int i, string[] data)
  {
      GlassDocumentScriptableObject newElement = CreateInstance<GlassDocumentScriptableObject>();
      string elementName = "";
      for (var y = 0; y < dataLines[i].Split(";").Length-1; y++)
      {
          if (y == 0) elementName += dataLines[i].Split(";")[y]; //ATELIER
          if (y == 1) elementName += dataLines[i].Split(";")[y]; //SCENE
          if (y == 2) elementName += dataLines[i].Split(";")[y]; //ROOM
          if (y == 3) elementName += dataLines[i].Split(";")[y]; //ELEMENT
          if (y == 4) elementName += dataLines[i].Split(";")[y]; //NAME

          if (y == 6) //FORMAT
              newElement.type = dataLines[i].Split(";")[y] == "Portrait" ? DocumentTypes.portrait :
                  dataLines[i].Split(";")[y] == "Landscape" ? DocumentTypes.landscape : DocumentTypes.square;

          if (y == 7) newElement.baseText = dataLines[i].Split(";")[y]; //Normal Text
          if (y == 8) newElement.fragAText = dataLines[i].Split(";")[y]; //A Text
          if (y == 9) newElement.fragBText = dataLines[i].Split(";")[y]; //B Text
          if (y == 10) newElement.bothText = dataLines[i].Split(";")[y]; //AB Text
      }
      Debug.Log(elementName);
      return newElement;
  }
}