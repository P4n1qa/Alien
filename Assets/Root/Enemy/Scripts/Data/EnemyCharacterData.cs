namespace Root.Enemy.Scripts.Data
{
    public class EnemyCharacterData : IEnemyCharacterData
    {
        public EnemyCharacteristics CurrentCharacteristics { get; private set; }

        public void Initialize()
        {
            CurrentCharacteristics = GameData.GetEnemyData();
        }
    }
}