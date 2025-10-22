using _Project.Scripts.Systems.StateMachine;

public interface ITransition
{
    IState To { get; }
    IPredicate Condition { get; }
}
