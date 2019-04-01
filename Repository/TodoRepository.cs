using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TodoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;

namespace TodoAPI.Repository
{
    public class TodoRepository : ITodoRepository, IDisposable
    {
        private TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            this._context = context;
        }

        public IEnumerable<TodoItem> GetItems(int pagenum, int pagesize)
        {
            return _context.TodoItems.ToList().Skip((pagenum - 1) * pagesize).Take(pagesize);
        }

        public async Task<List<TodoItem>> GetItemsSearch(int pagenum, int pagesize, string term)
        {
            string searchTerm = term ?? "";
            if (pagenum < 1) pagenum = 1;
            if (pagesize < 1) pagesize = 1;
            if (pagesize > 100) pagesize = 100;
            var test = await _context.TodoItems
                .Where(item => item.Name.ToLower().Contains(searchTerm.ToLower()))
                .Skip((pagenum - 1) * pagesize)
                .Take(pagesize)
                .ToListAsync();
            return test;
        }

        public TodoItem GetByID(long id)
        {
            return _context.TodoItems.Find(id);
        }

        public async Task InsertItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(long id)
        {
            TodoItem entity = await _context.TodoItems.FindAsync(id);
            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> UpdateItem(long id, TodoItem item)
        {
            TodoItem entity = await _context.TodoItems.FindAsync(id);
            entity.Name = item.Name;

            await _context.SaveChangesAsync();
            return item;
        
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}