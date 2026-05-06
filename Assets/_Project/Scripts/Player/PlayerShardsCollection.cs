using System;
using _Project.Scripts.ECS;
using _Project.Scripts.GameServices;
using _Project.Scripts.Systems.Singletons;
using UnityEngine;

namespace _Project.Scripts.Player {
    public class PlayerShardsCollection : Singleton<PlayerShardsCollection> {
        [SerializeField] private Glass[] glassShards;

        public void UnlockShardAtIndex(int index) {
            GameInitializer.Instance.AddSingleShard(glassShards[index]);
        }
    }
}