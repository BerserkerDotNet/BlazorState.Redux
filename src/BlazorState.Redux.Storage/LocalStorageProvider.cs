﻿using System.Text.Json;
using System.Threading.Tasks;
using Blazor.Extensions.Storage;
using BlazorState.Redux.Interfaces;
using Newtonsoft.Json;

namespace BlazorState.Redux.Storage
{
    public class LocalStorageProvider : IStateStorage
    {
        private readonly string _key;
        private readonly LocalStorage _storage;

        public LocalStorageProvider(string key, LocalStorage storage)
        {
            _key = key;
            _storage = storage;
        }

        public async ValueTask<T> Get<T>()
        {
            var stateJsonMemory = await _storage.GetItem<JsonElement>(_key);
            return JsonConvert.DeserializeObject<T>(stateJsonMemory.ToString());
        }

        public async ValueTask Save<T>(T state)
        {
            await _storage.SetItem(_key, state);
        }
    }
}
