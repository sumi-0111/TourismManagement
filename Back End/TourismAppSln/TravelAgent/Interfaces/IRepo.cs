﻿using Microsoft.AspNetCore.Mvc;

namespace TourPackage.Interfaces
{
    public interface IRepo<K,T>
    {
        public Task<T?> Add ( T item );
        Task<T?> Update(T item);
        public Task<T?> Delete(K key);
        public Task<T?> Get(K key);
        public Task<ICollection<T>?> GetAll();
    }
}
