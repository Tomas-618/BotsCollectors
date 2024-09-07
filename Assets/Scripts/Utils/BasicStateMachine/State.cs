using System.Collections.Generic;

namespace BasicStateMachine
{
    public abstract class State
    {
        private readonly List<Transition> _transitions;

        protected State() =>
            _transitions = new List<Transition>();

        public void AddTransition(Transition transition)
        {
            if (_transitions.Contains(transition) == false)
                _transitions.Add(transition);
        }

        public bool TryGetNext(out State next)
        {
            next = null;

            foreach (Transition transition in _transitions)
            {
                if (transition.TryGetNextState(out State nextState))
                {
                    next = nextState;

                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
            foreach (Transition transition in _transitions)
                transition.Update();

            Work();
        }

        public virtual void Enter()
        {
            foreach (Transition transition in _transitions)
                transition.Close();
        }

        public virtual void Exit() { }

        protected virtual void Work() { }
    }
}
