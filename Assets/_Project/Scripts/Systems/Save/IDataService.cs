using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;

namespace _Project.Scripts.Systems.Save {
    public interface IDataService {
        void Save(GameData data, bool overwrite = true);
        GameData Load(string name);
        void Delete(string name);
        void DeleteAll();
        IEnumerable<string> ListSaves();
        
        bool FileDoesExist(string name);
    }
}