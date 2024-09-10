// Boilerplate from: https://game.courses/bots-ai-statemachines/

namespace FWGameLib.Common.StateMachine
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}