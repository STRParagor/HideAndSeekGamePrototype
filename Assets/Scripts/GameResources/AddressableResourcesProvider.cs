using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CHorseGames.ToiletWar.GameResources
{
    public class AddressableResourcesProvider<T> : IResourcesProvider<T> where T : class
    {
        public Action<string> Error { get; set; }
        
        private readonly Dictionary<string, AsyncOperationHandle<T>> AsyncOperationsHandles;

        private readonly string GroupName;

        public AddressableResourcesProvider(string groupName)
        {
            AsyncOperationsHandles = new Dictionary<string, AsyncOperationHandle<T>>();
            GroupName = groupName;
        }
        
        public AsyncOperationHandle<T> MakeLoadOperation(string key)
        {
            var keyInGroup = $"{GroupName}/{key}";
            AsyncOperationHandle<T> loadOperationHandler;
            if (AsyncOperationsHandles.TryGetValue(keyInGroup, out var handle))
            {
                loadOperationHandler = handle;
            }
            else
            {
                loadOperationHandler = GetLoadOperationAsync(keyInGroup);
                AsyncOperationsHandles.Add(keyInGroup, loadOperationHandler);
            }

            return loadOperationHandler;
        }
        
        public async UniTask<T> Load(string key)
        {
            var keyInGroup = $"{GroupName}/{key}";
            AsyncOperationHandle<T> loadOperationHandler;
            if (AsyncOperationsHandles.TryGetValue(keyInGroup, out var handle))
            {
                loadOperationHandler = handle;
                //Debug.Log("[AddressableResourcesProvider] From cache by "+keyInGroup);
                if(loadOperationHandler.Status == AsyncOperationStatus.Succeeded)
                    return loadOperationHandler.Result;
            }
            else
            {
                //Debug.Log("[AddressableResourcesProvider] To cache "+keyInGroup);
                loadOperationHandler = GetLoadOperationAsync(keyInGroup);
                AsyncOperationsHandles.Add(keyInGroup, loadOperationHandler);
            }

            T loadedObject = default(T);
            try
            {
                loadedObject = await loadOperationHandler;
            }
            catch (InvalidKeyException)
            {
                Error("InvalidKey: "+ keyInGroup);
                return null;
            }

            if (loadOperationHandler.Status != AsyncOperationStatus.Succeeded)
            {
                Error("Fail download: "+keyInGroup);
                return null;
            }
            
            if (loadedObject == null)
            {
                Error("Is null: "+keyInGroup);
                return null;
            }

            return loadedObject;
        }
        
        public void Release(string key)
        {
            var keyInGroup = $"{GroupName}/{key}";
            var objectIsNotNull = AsyncOperationsHandles.TryGetValue(keyInGroup, out var objectToRelease);
            if (objectIsNotNull)
            {
                if (objectToRelease.IsValid())
                {
                    Addressables.Release(objectToRelease);
                    //Debug.Log($"[AddressableResourcesProvider] Release {key} done");
                }
                AsyncOperationsHandles.Remove(keyInGroup);
            }
            else
            {
                Debug.LogWarning($"[AddressableResourcesProvider] Release Fail: Wrong {keyInGroup} or object already released");
            }
        }

        private AsyncOperationHandle<T> GetLoadOperationAsync(string keyInGroup)
        {
            var loadOperationHandler = Addressables.LoadAssetAsync<T>(keyInGroup);
            Assert.IsTrue(loadOperationHandler.IsValid(), $"[AddressableResourcesProvider] Not Valid operation for load - {keyInGroup}");
            return loadOperationHandler;
        }
    }
}