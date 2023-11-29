namespace MazeGame.EnemyAI
{
    public interface IState
    {
        IState DoAction(MonsterListener npc);
    }
}
