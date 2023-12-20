using System;
using Cysharp.Threading.Tasks;

namespace ParagorGames.GameResources
{
    public interface IResourcesProvider<T>
    {
        Action<string> Error { get; set; }
        UniTask<T> Load(string key);
        void Release(string key);
    }
}