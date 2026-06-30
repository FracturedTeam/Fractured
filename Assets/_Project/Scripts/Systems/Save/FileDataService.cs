using System;
using System.Collections.Generic;
using System.IO;
using _Project.Scripts.GameServices.Services;
using UnityEngine;

namespace _Project.Scripts.Systems.Save {
    public class FileDataService : IDataService {
        private ISerializer serializer;
        private string dataPath;
        private string saveFolder;
        private string fileExtension;

        public FileDataService(ISerializer serializer) {
            dataPath = Application.persistentDataPath;
            saveFolder = Path.Combine(dataPath, "Saves");
            fileExtension = "json";
            this.serializer = serializer;

            if (!Directory.Exists(saveFolder)) {
                Directory.CreateDirectory(saveFolder);
            }
        }

        private string GetPathToFile(string fileName) {
            return Path.Combine(saveFolder, string.Concat(fileName, ".", fileExtension));
        }
        
        public void Save<T>(T data, string fileName, bool overwrite = true)
        {
            var fileLocation = GetPathToFile(fileName);
            if (!overwrite && File.Exists(fileLocation)) {
                throw new IOException($"The file at {fileLocation} already exists and cannot be overwritten.");
            }
            
            File.WriteAllText(fileLocation, serializer.Serialize(data));
        }
        

        public T Load<T>(string name) {
            var fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation)) {
                throw new ArgumentException($"No persisted GameData with name '{name}'");
            }
            
            return serializer.Deserialize<T>(File.ReadAllText(fileLocation));
        }

        public void Delete(string name) {
            var fileLocation = GetPathToFile(name);
            
            if(File.Exists(fileLocation))
                File.Delete(fileLocation);
        }

        public void DeleteAll() {
            foreach (var filePath in Directory.GetFiles(saveFolder)) {
                Debug.Log("Delete All Files " + filePath);
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListSaves() {
            foreach (var path in Directory.EnumerateFiles(saveFolder)) {
                if(Path.GetExtension(path) == fileExtension)
                    yield return Path.GetFileNameWithoutExtension(path);
            }
        }

        public bool FileDoesExist(string name) {
            var fileLocation = GetPathToFile(name);
            return File.Exists(fileLocation);
        }
    }
}