using System;

namespace BasicStateMachine
{
    public class StateMachine<TState, TTransition> where TState : State<TState, TTransition>
        where TTransition : Transition<TState, TTransition>
    {
        private readonly TState _startState;

        private TState _currentState;

        public StateMachine(TState startState)
        {
            _startState = startState ?? throw new ArgumentNullException(nameof(startState));
            Reset();
        }

        public void Update()
        {
            _currentState.Update();
            GetNextState();
        }

        private void GetNextState()
        {
            if (_currentState.TryGetNext(out TState nextState))
                ChangeState(nextState);
        }

        private void Reset() =>
            ChangeState(_startState);

        private void ChangeState(TState state)
        {
            _currentState?.Exit();
            _currentState = state ?? throw new ArgumentNullException(nameof(state));
            _currentState.Enter();
        }
    }
}
