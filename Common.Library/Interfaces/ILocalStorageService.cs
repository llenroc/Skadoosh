using System;
namespace Common.Library.Interfaces
{
    interface ILocalStorageService
    {
        T GetIsolatedStorage<T>(string contentName) where T : new();
        void SaveIsolatedStorage<T>(string contentName, object obj);
    }
}
