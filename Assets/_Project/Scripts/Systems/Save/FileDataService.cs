using System;
using System.Collections.Generic;
using System.IO;
using _Project.Scripts.GameServices;
using UnityEngine;

namespace _Project.Scripts.Systems.Save {
    public class FileDataService : IDataService {
        private ISerializer serializer;
        string dataPath;
        string fileExtension;

        public FileDataService(ISerializer serializer) {
            this.dataPath = Application.persistentDataPath;
            this.fileExtension = "json";
            this.serializer = serializer;
        }

        private string GetPathToFile(string fileName) {
            return Path.Combine(dataPath, string.Concat(fileName, ".", fileExtension));
        }
        
        public void Save(GameData data, bool overwrite = true) {
            var fileLocation = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(fileLocation)) {
                throw new IOException($"The file '{data.Name}.{fileExtension}' already exists and cannot be overwritten.");
            }
            
            File.WriteAllText(fileLocation, serializer.Serialize(data));
        }

        public GameData Load(string name) {
            var fileLocation = GetPathToFile(name);

            if (!File.Exists(fileLocation)) {
                throw new ArgumentException($"No persisted GameData with name '{name}'");
            }
            
            return serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }

        public void Delete(string name) {
            var fileLocation = GetPathToFile(name);
            
            if(File.Exists(fileLocation))
                File.Delete(fileLocation);
        }

        public void DeleteAll() {
            foreach (var filePath in Directory.GetFiles(dataPath)) {
                File.Delete(filePath);
            }
        }

        public IEnumerable<string> ListSaves() {
            foreach (var path in Directory.EnumerateFiles(dataPath)) {
                if(Path.GetExtension(path) == fileExtension)
                    yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}