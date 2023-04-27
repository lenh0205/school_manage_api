using DataInfastructure.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using DataInfastructure.Interface;

namespace DataInfastructure.Responsitory
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly SchoolDBContext _context;
        public RepositoryBase(SchoolDBContext context)
        {
            _context = context;
        }


        public T Add(T c)
        {
            if (c == null)
                return null;

            _context.Set<T>().Add(c);
            return c;
        }

        public bool Delete(Guid id)
        {
            var item = GetById(id);
            if (item == null) return false;

            _context.Set<T>().Remove(item);
            return true;
        }

        public T GetById(Guid id)
        {
            //var c = _context.Set<T>().FirstOrDefault(c => c.Id == id);
            var c = _context.Set<T>().SingleOrDefault(c => c.Id == id);
            return c;
        } 

        public List<T> GetByIds(List<Guid> ids)
        {
            var cList = _context.Set<T>().Where(x => ids.Contains(x.Id)).ToList();
            return cList;
        }

        public ResponseItems<T> GetAll(string page)
        {
            int.TryParse(page, out int pageNumber);
            const int pageSize = 3;

            List<T> cList = _context.Set<T>()
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalItems = _context.Set<T>().ToList().Count();
            int totalPages = (int) Math.Ceiling((double) totalItems / pageSize);

            var data = new ResponseItems<T> { ClassList = cList, TotalPages = totalPages};
            return data;
        }

        public T Update(Guid id, T c)
        {
            var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(c);
            }
            return c;
        }
    }


    public class ResponseItems<T>
    {
        public int TotalPages { get; set; }
        public List<T> ClassList { get; set; }
    }
}
