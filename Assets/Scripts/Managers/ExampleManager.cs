using Interfaces;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ExampleManager : MonoBehaviour,
        ISavable
    {
        private readonly int _exampleData = 1;
        private ExampleSaveData _data;


        private readonly string _uniqueIdString = "1111";
        private int _uniqID;

        private string _UniqueIdString;
        private int _uniqueId;


        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _data = new ExampleSaveData(_exampleData);
            int.TryParse(_uniqueIdString,
                out _uniqID); //Gelen string Uniq Idyi İnt e çeviriyor; ES3 uyarlaması

            if (!ES3.FileExists($"ExampleData{_uniqueId}.es3"))
                if (!ES3.KeyExists("ExampleData"))
                    //Bu Kısımdan Datanın Derğlerini çek * _data=GetExampleData();
                    Save(_uniqueId);

            Load(_uniqueId);
        }

        public void Save(int uniqueId)
        {
            _data = new ExampleSaveData(_exampleData);
            SaveLoadSignals.Instance.onSaveExampleData.Invoke(_data,
                _uniqueId);
        }

        public void Load(int uniqueId)
        {
            _data = SaveLoadSignals.Instance.onLoadExampleData(_data.Key,
                _uniqueId);
        }
    }
}