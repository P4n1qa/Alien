namespace Root.Enemy.Scripts.Data
{
    public interface IEnemyCharacterData
    {
        EnemyCharacteristics CurrentCharacteristics { get;}

        void Initialize();
    }
}