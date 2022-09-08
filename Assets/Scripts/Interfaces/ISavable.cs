namespace Interfaces
{
    public interface ISavable
    {
        void Save(int uniqueId);

        void Load(int uniqueId);
    }
}