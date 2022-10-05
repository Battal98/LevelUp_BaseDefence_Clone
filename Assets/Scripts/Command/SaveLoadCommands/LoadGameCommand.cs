using Interfaces;

namespace Commands
{
    public class LoadGameCommand
    {
        public T Execute<T>(string key,
            int uniqueId) where T : ISavableEntity
        {
            var _path = key + uniqueId + ".es3";
            if (ES3.FileExists(_path))
            {
                if (ES3.KeyExists(key,
                        _path))
                {
                    var objectToReturn = ES3.Load<T>(key,
                        _path);
                    return objectToReturn;
                }

                return default;
            }

            return default;
        }
    }
}