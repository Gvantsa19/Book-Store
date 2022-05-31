using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;

namespace ZBS.Infrastructure.GenericRepositoryP
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private DataBaseContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository(DataBaseContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        { 
            await table.AddAsync(entity);
        }


        public async Task DeleteByIdAsync(int id)
        {
             T findEntity = await  table.FindAsync(id);
            table.Remove(findEntity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task UpdateByIdAsync(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
