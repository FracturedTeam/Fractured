using System;
using _Project.Scripts.ECS;
using _Project.Scripts.ECS.BaseObjects;
using _Project.Scripts.Enums;
using _Project.Scripts.GameServices;
using _Project.Scripts.Player;
using _Project.Scripts.Systems.HashSetUtil;
using DG.Tweening;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

[RequireComponent(typeof(BaseObject))] 
public class GlassText : MonoBehaviour
{
    [Header("Visibility Settings")]
    [SerializeField] private bool isVisibleFromStart;
    [SerializeField] private bool disappearDefinitively; 

    [Header("Global Settings")]
    [SerializeField] private GlassTextScriptableObject currentTextScriptableObject;
    [SerializeField] private GlassTextLink baseText;
    [SerializeField] private GlassTextLink fragAText;
    [SerializeField] private GlassTextLink fragBText;
    [SerializeField] private GlassTextLink bothText;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private GameObject mistColorA;
    [SerializeField] private GameObject mistColorB;
    [SerializeField] private GameObject mistColorBoth;

    [HideInInspector]
    public bool showMistColorA,showMistColorB,showMistColorBoth;
    
    private EventInstance soundInstance; 
    
    // private ObservableHashSet<Glass> shardsOnTop;
    private bool isInitialized;

    private bool canAppearAgain = true;

    internal void Initialize()
    {
        baseText.Initialize(this);
        fragAText.Initialize(this);
        fragBText.Initialize(this);
        bothText.Initialize(this);
        if(meshRenderer) meshRenderer?.material.DOFade(0, 0);
        
        if (!isInitialized) {
            // shardsOnTop = new ObservableHashSet<Glass>();
            // shardsOnTop.onUpdate += UpdateShards;

            isInitialized = true;
            
            soundInstance = GameInitializer.Instance.CreateInstance(GameInitializer.Instance.GetBank().environmentalText_Loop);
            RuntimeManager.AttachInstanceToGameObject(soundInstance, gameObject);
        }

        ForceSet();
        SetAlpha(isVisibleFromStart ? 1 : 0);
    }

    [ContextMenu("Set Texts")]
    internal void ForceSet()
    {
        if (currentTextScriptableObject)
            Setup(currentTextScriptableObject, false);
    }

    private void SetAlpha(float alpha, float time = 0)
    {
        baseText.SetAlpha(alpha, time);
        fragAText.SetAlpha(alpha, time);
        fragBText.SetAlpha(alpha, time);
        bothText.SetAlpha(alpha, time);
        if(meshRenderer) meshRenderer?.material.DOFade(alpha, time);
    }

    private void UpdateShards()
    { }

    private void OnDestroy() {
        // if (shardsOnTop == null) 
        //     return;
        
        // shardsOnTop.onUpdate -= UpdateShards;
        // shardsOnTop.Clear();

        soundInstance.stop(STOP_MODE.IMMEDIATE);
        soundInstance.release();
    }

    [ContextMenu("Manually Appear")]
    public void Appear() {
        if(!canAppearAgain) return;

        soundInstance.getPlaybackState(out var playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED)) {
            soundInstance.start();
        }
        
        GameInitializer.Instance.PlaySound3D(GameInitializer.Instance.GetBank().environmentText_Appear, transform.position);
        
        SetAlpha( 1, 1);
        if(meshRenderer) meshRenderer?.material.DOFade(0.5f, 1);
        
        if(showMistColorA) mistColorA.SetActive(true);
        else if(showMistColorB) mistColorB.SetActive(true);
        else if(showMistColorBoth) mistColorBoth.SetActive(true);
    }    
    
    [ContextMenu("Manually Disappear")]
    public void Disappear() {
        soundInstance.stop(STOP_MODE.ALLOWFADEOUT);
        SetAlpha( 0, 1);
        if(meshRenderer) meshRenderer?.material.DOFade(0.5f, 1);

        if (disappearDefinitively) canAppearAgain = false;
        
        mistColorA.SetActive(false);
        mistColorB.SetActive(false);
        mistColorBoth.SetActive(false);
    }

    // internal void OnInteract(bool isColliding, Glass shard) {
    //     fragAText.OnInteract(isColliding, shard);
    //     fragBText.OnInteract(isColliding, shard);
    //     bothText.OnInteract(isColliding, shard);
    // }

    public void Setup(GlassTextScriptableObject newData, bool dontUseShader)
    {
        if (!isInitialized) {
            baseText.Initialize(this);
            fragAText.Initialize(this);
            fragBText.Initialize(this);
            bothText.Initialize(this);

            isInitialized = true;
        }
        
        currentTextScriptableObject = newData;
        //Case 0XXX
        if (currentTextScriptableObject.baseText == "")
        {
            //Case 00XX
            if (currentTextScriptableObject.fragAText == "")
            {
                //Case 000X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 0000 - 
                    if (currentTextScriptableObject.bothText == "")
                    {
                        Error();
                        return; 
                    }
                    
                    //Case 0001 - Both
                    {
                        baseText.SetText("");
                        fragAText.SetText("");
                        fragBText.SetText("");
                        bothText.SetText(currentTextScriptableObject.bothText);
                        return;
                    }
                }
                //Case 001X
                {
                    //Case 0010 - B
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB, dontUseShader);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.fragBText, ColorEnum.ColorB, dontUseShader);
                        return;
                    }

                    //Case 0011 - BBoth
                    {
                        Error();
                        return;
                    }
                }
            }
            //Case 01XX
            {
                //Case 010X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 0100 - A
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA, dontUseShader);
                        bothText.SetText(currentTextScriptableObject.fragAText, ColorEnum.ColorA, dontUseShader);
                        return;
                    }
                    
                    //Case 0101 - ABoth
                    {
                        Error();
                        return;
                    }
                }
                //Case 011X
                {
                    //Case 0110 - AB
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both, dontUseShader);
                        return;
                    }
                    //Case 0111 -ABBOTH
                    {
                        Error();
                        return;
                    }
                   
                }
            }
        }
        // Case 1XXX
        {
             //Case 10XX
            if (currentTextScriptableObject.fragAText == "")
            {
                //Case 100X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 1000 - 
                    if (currentTextScriptableObject.bothText.Length == 0)
                    {
                        baseText.SetText(currentTextScriptableObject.baseText);
                        fragAText.SetText(currentTextScriptableObject.baseText);
                        fragBText.SetText(currentTextScriptableObject.baseText);
                        bothText.SetText(currentTextScriptableObject.baseText);
                        return;
                    }
                    //Case 1001 - Both
                    {
                        Error();
                        return;
                    }
                }
                //Case 101X
                {
                    //Case 1010 - B
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB, dontUseShader);
                        fragBText.SetText(currentTextScriptableObject.fragBText);
                        bothText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorB, dontUseShader);
                        return;
                    }
                    //Case 1011 - BBoth
                    {
                        Error();
                        return;
                    }
                }
            }
            //Case 11XX
            {
                //Case 110X
                if (currentTextScriptableObject.fragBText == "")
                {
                    //Case 1100 - A
                    if (currentTextScriptableObject.bothText == "")
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.fragAText);
                        fragBText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA, dontUseShader);
                        bothText.SetText(currentTextScriptableObject.baseText, ColorEnum.ColorA, dontUseShader);
                        return;
                    }
                    //Case 1101 - ABoth
                    {
                        Error();
                        return;
                    }
                }
                //Case 111X
                {
                    //Case 1110 - AB
                    if (currentTextScriptableObject.bothText == "")
                    {
                        Error();
                        return;
                    }
                    //Case 1111 -ABBOTH
                    {
                        baseText.SetText(currentTextScriptableObject.baseText, ColorEnum.Both, dontUseShader);
                        fragAText.SetText(currentTextScriptableObject.fragAText, ColorEnum.Both, dontUseShader);
                        fragBText.SetText(currentTextScriptableObject.fragBText, ColorEnum.Both, dontUseShader);
                        bothText.SetText(currentTextScriptableObject.bothText, ColorEnum.Both, dontUseShader);
                        return;
                    }
                   
                }
            }
        }
    }

    private void Error()
    {
        Debug.LogError("Error on Text Scriptable Object, Case not supported, see the documentation for more information " +
                       "<a href=\"https://docs.google.com/document/d/1IGWeNeqUure2vZgyoxgXxlqOPQhoYTO_nbDbxLvGX8A/edit?tab=t.0#heading=h.16tvc46zf4qe\">Case Table</a> and " +
                       "SetUp Document", transform);
    }
}
