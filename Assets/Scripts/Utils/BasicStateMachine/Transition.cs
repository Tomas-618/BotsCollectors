using System;

namespace BasicStateMachine
{
    public abstract class Transition<TState, TTransition> where TState : State<TState, TTransition>
        where TTransition : Transition<TState, TTransition>
    {
        private readonly TState _nextState;

        private bool _isOpen;

        public Transition(TState nextState) =>
            _nextState = nextState ?? throw new ArgumentNullException(nameof(nextState));

        public virtual void Update() { }

        public bool TryGetNextState(out TState nextState)
        {
            nextState = _isOpen ? _nextState : null;

            return _isOpen;
        }

        public void Close() =>
            _isOpen = false;

        protected void Open() =>
            _isOpen = true;
    }
}
