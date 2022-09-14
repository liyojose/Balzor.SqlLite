using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using DemoApp.Shared;
using Microsoft.JSInterop;

namespace DemoApp.Client.Data
{
    // To support offline use, we use this simple local data repository
    // instead of performing data access directly against the server.
    // This would not be needed if we assumed that network access was always
    // available. Updated to GIT

    public class LocalUserStore
    {
        private readonly HttpClient httpClient;
        private readonly IJSRuntime js;

        public LocalUserStore(HttpClient httpClient, IJSRuntime js)
        {
            this.httpClient = httpClient;
            this.js = js;
        }

        public ValueTask<User[]> GetOutstandingLocalEditsAsync()
        {
            return js.InvokeAsync<User[]>(
                "localUserStore.getAll", "localedits");
        }

        public async Task SynchronizeAsync()
        {
            // If there are local edits, always send them first
            foreach (var editedUser in await GetOutstandingLocalEditsAsync())
            {
                (await httpClient.PutAsJsonAsync("api/UsersData/Details", editedUser)).EnsureSuccessStatusCode();
                await DeleteAsync("localedits", editedUser.Id);
            }

            await FetchChangesAsync();
        }



        // If there's an outstanding local edit, use that. If not, use the server data.
        public async Task<User> GetUser(int id)
            => await GetAsync<User>("localedits", id)
            ?? await GetAsync<User>("serverdata", id);

        // If there's an outstanding local edit, use that. If not, use the server data.
        public async Task<IEnumerable<Localuser>> GetUsers()
        {
            var localedited = await GetAllAsync<IEnumerable<Localuser>>("localedits");
            var serverdata = await GetAllAsync<IEnumerable<Localuser>>("serverdata");
            return localedited.Union(serverdata).Where(e=> e.IsDeleted == false);
        }

        public async ValueTask<DateTime?> GetLastUpdateDateAsync()
        {
            var value = await GetAsync<string>("metadata", "lastUpdateDate");
            return value == null ? (DateTime?)null : DateTime.Parse(value);
        }

        public ValueTask SaveUserAsync(Localuser User)
        {
            User.LastUpdated = DateTime.Now;
            return PutAsync("localedits", null, User);
        }
        public ValueTask RemoveUserAsync(Localuser User)
        {
            User.LastUpdated = DateTime.Now;
            User.IsDeleted = true;
            return PutAsync("localedits", User.Id>0?User.Id : Guid.NewGuid(),User);
        }
        async Task FetchChangesAsync()
        {
            var mostRecentlyUpdated = await js.InvokeAsync<Localuser>("localUserStore.getFirstFromIndex", "serverdata", "lastUpdated", "prev");
            var since = mostRecentlyUpdated?.LastUpdated ?? DateTime.MinValue;
            var json = await httpClient.GetStringAsync($"api/UsersData/ChangedUsers?since={since:o}");
            await js.InvokeVoidAsync("localUserStore.putAllFromJson", "serverdata", json);
            await PutAsync("metadata", "lastUpdateDate", DateTime.Now.ToString("o"));
        }
        ValueTask<T> GetAllAsync<T>(string storeName)
            => js.InvokeAsync<T>("localUserStore.getAll", storeName);
        ValueTask<T> GetAsync<T>(string storeName, object key)
            => js.InvokeAsync<T>("localUserStore.get", storeName, key);

        ValueTask PutAsync<T>(string storeName, object key, T value)
            => js.InvokeVoidAsync("localUserStore.put", storeName, key, value);

        ValueTask DeleteAsync(string storeName, object key)
            => js.InvokeVoidAsync("localUserStore.delete", storeName, key);

    }
}
