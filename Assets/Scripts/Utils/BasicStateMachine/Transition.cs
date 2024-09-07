using System;

namespace BasicStateMachine
{
    public abstract class Transition
    {
        private readonly State _nextState;

        private bool _isOpen;

        public Transition(State nextState) =>
            _nextState = nextState ?? throw new ArgumentNullException(nameof(nextState));

        public abstract void Update();

        public bool TryGetNextState(out State nextState)
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
