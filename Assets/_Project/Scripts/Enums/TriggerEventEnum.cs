namespace _Project.Scripts.Enums {
    public enum TriggerEventEnum {
        OnInteract,
    }
    
    public enum TriggerPhysicEnum {
        OnCollisionTriggerEnter,
        OnCollisionTriggerExit,
    }
    
    public enum TriggerGlassEnum {
        OnHideReveal,
    }
    
    public enum TriggerTextEnum {
        OnTextHideReveal,
    }
    
    public enum TriggerPuzzleObjectEnum {
        OnInteractFailed,
        OnInteractSuccess,
    }
    
    public enum TriggerStatesEnum {
        OnSetStateOn,
        OnSetStateOff,
    }

    public enum TriggerPickUpEnum { //PickUp + collectable
        
    }

    public enum TriggerSceneEnum
    {
        OnSceneComplete,
        OnAtelierComplete 
    }
}