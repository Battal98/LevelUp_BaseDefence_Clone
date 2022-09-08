namespace Interfaces
{
    public interface IBossEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public float AttackRange { get; set; }
        public float AttackSpeed { get; set; }
    } 
}
