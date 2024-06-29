using System.Collections.Generic;

namespace BasicStateMachine
{
    public abstract class State<TState, TTransition> where TState : State<TState, TTransition>
        where TTransition : Transition<TState, TTransition>
    {
        private readonly List<TTransition> _transitions;

        protected State() =>
            _transitions = new List<TTransition>();

        public void AddTransition(TTransition transition)
        {
            if (_transitions.Contains(transition) == false)
                _transitions.Add(transition);
        }

        public bool TryGetNext(out TState next)
        {
            next = null;

            foreach (TTransition transition in _transitions)
            {
                if (transition.TryGetNextState(out TState nextState))
                {
                    next = nextState;

                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
            foreach (TTransition transition in _transitions)
                transition.Update();

            Work();
        }

        public virtual void Enter()
        {
            foreach (TTransition transition in _transitions)
                transition.Close();
        }

        public virtual void Exit() { }

        protected virtual void Work() { }
    }
}
