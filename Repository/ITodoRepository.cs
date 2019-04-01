using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Models;

namespace TodoAPI.Repository
{
    public interface ITodoRepository : IDisposable
    {
        IEnumerable<TodoItem> GetItems(int pagenum, int pagesize);
        Task<List<TodoItem>> GetItemsSearch(int pagenum, int pagesize, string term);
        TodoItem GetByID(long id);
        Task InsertItem(TodoItem item);
        Task DeleteItem(long id);
        Task<TodoItem> UpdateItem(long id, TodoItem item);
        void Save();
    }
}