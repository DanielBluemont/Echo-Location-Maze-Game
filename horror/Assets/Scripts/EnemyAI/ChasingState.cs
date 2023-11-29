namespace MazeGame.EnemyAI
{
    public class ChasingState : IState
    {
        public IState DoAction(MonsterListener npc)
        {
            if (npc.isPlayerInZone())
            {
                npc.TracePlayerLocation();
                npc.AttackPlayerNearby();
                return StateControl.States[State.STATE_CHASING];
            }
            else
            {
                return StateControl.States[State.STATE_SEARCHING];
            }
        }
    }
}
