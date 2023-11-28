using System.Collections.Generic;


namespace StateMachine
{   
    public enum State
    {
        STATE_SEARCHING,
        STATE_CHASING
    }
    public static class StateControl
    {
        public static Dictionary<State, IState> States = new Dictionary<State, IState>
        {
            {State.STATE_SEARCHING, new SearchingState()},
            {State.STATE_CHASING, new ChasingState()}
        };
    }
}
