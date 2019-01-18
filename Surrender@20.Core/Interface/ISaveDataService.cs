namespace Surrender_20.Core.Interface
{
    public interface ISaveDataService
    {
        void SaveData(string key, string data);
        string GetData(string key);
    }
}
