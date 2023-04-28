﻿using DataInfastructure.Model;
using System.Collections.Generic;
using System;
using DataInfastructure.Responsitory;
using System.Threading.Tasks;

namespace DataInfastructure.Interface
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        public T GetById(Guid id);
        public List<T> GetByIds(List<Guid> ids);
        public ResponseItems<T> GetWithPagination(string page);
        public Task<List<T>> GetAll();
        public T Add(T c);
        public T Update(Guid id, T c);
        public bool Delete(Guid id);
    }
}