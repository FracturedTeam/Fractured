using System.Collections.Generic;
using _Project.Scripts.GameServices;
using _Project.Scripts.GameServices.Services;

namespace _Project.Scripts.Systems.Save {
    public interface IDataService {
        void Save<T>(T data, string fileLocation, bool overwrite = true);
        T Load<T>(string name);
        void Delete(string name);
        void DeleteAll();
        IEnumerable<string> ListSaves();
        
        bool FileDoesExist(string name);
    }
}