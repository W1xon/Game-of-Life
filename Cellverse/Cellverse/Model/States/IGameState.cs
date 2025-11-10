namespace Cellverse.Model;

public interface IGameState
{
    void Enter();
    void Exit();
    void Update();
    bool CanTransitionTo(IGameState newState);
}