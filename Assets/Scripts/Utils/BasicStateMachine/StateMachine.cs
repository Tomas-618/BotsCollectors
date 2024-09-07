using System;

namespace BasicStateMachine
{
    public class StateMachine
    {
        private readonly State _startState;

        private State _currentState;

        public StateMachine(State startState)
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
            if (_currentState.TryGetNext(out State nextState))
                ChangeState(nextState);
        }

        private void Reset() =>
            ChangeState(_startState);

        private void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state ?? throw new ArgumentNullException(nameof(state));
            _currentState.Enter();
        }
    }
}
