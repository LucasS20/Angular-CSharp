﻿using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces;

public interface IGeneralPersist
{
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    void DeleteRange<T>(object[] entityArray) where T : class;
    Task<bool> SaveChangesAsync();
}