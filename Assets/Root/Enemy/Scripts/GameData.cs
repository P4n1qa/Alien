using Root.Enemy.Scripts.Data;

namespace Root.Enemy.Scripts
{
    public static class GameData
    {
        public static EnemyCharacteristics GetEnemyData()
        {
            var enemyData = new EnemyCharacteristics( 100f,0f);
            return enemyData;
        }
    }
}