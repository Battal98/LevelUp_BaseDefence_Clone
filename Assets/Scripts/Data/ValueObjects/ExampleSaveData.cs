using Interfaces;

namespace Data.ValueObject
{
    public class ExampleSaveData:ISavableEntity
    {
        public int ExampleData;
        public string Key = "ExampleSaveData";


        public ExampleSaveData(int exampleData)
        {
            ExampleData = exampleData;
        }
        
        
        public string GetKey()
        {
            return Key;
        }
        
        
    }
}