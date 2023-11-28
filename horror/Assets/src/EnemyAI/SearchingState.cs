using StateMachine;

public class SearchingState : IState
{
    public IState DoAction(MonsterListener npc)
    {
        if (npc.IsPathCompleted())
        {
            npc.WalkToRandomPoint();
        }
        return this;
    }

    
}
