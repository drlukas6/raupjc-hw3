using System;
using System.Collections.Generic;
using System.Linq;
using hw3webapp.Models;

namespace hw3webapp
{
    public class TodoSqlRepository : ITodoRepository
    { 
        private readonly TodoDbContext _context;
        
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (_context.TodoItems.FirstOrDefault(g => g.Id == todoItem.Id) != null)
            {
                throw new DuplicateTodoItemException($"duplicate id: {todoItem.Id}");
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem tmp = _context.TodoItems.FirstOrDefault(g => g.Id == todoId);
            if (tmp == null)
            {
                return false;
            }
            if (!tmp.UserId.Equals(userId))
            {
                throw new TodoAccessDeniedException("User does not have access.");
            }
            _context.TodoItems.Remove(tmp);
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            TodoItem tmp = _context.TodoItems.FirstOrDefault(g => g.Id == todoItem.Id);
            if (tmp == null)
            {
                if (todoItem.UserId.Equals(userId)) _context.TodoItems.Add(todoItem);
                else throw new TodoAccessDeniedException("User does not have access.");
            }
            else
            {
                _context.TodoItems.Remove(tmp);
                _context.TodoItems.Add(todoItem);
            }

            _context.SaveChanges();

        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem tmp = _context.TodoItems.FirstOrDefault(g => g.Id == todoId);
            if (tmp == null) return false;
            else if (tmp.UserId.Equals(userId))
            {
                tmp.DateCompleted = DateTime.Now;
                Update(tmp, userId);
                return true;
            }
            return false;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            return _context.TodoItems.Where(g => g.UserId == userId).OrderByDescending(g => g.DateCreated).ToList();
            }

        public List<TodoItem> GetActive(Guid userId)
        {
            return _context.TodoItems.Where(g => g.UserId == userId && !g.IsCompleted).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            return _context.TodoItems.Where(g => g.UserId == userId && g.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            return _context.TodoItems.Where(g => g.UserId == userId && filterFunction(g)).ToList();
        }
        
        
    }
}