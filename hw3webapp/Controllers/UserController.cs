using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hw3webapp;
using hw3webapp.Models;

namespace hw3webapp.Controllers
{
    public class UserController : Controller
    {
        private readonly TodoDbContext _context;

        public UserController(TodoDbContext context)
        {
            _context = context;
        }
        
        
        // GET
        public async Task<IActionResult> Index()
        {
            List<User> allUsers = await _context.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                user.isActive = false;
                _context.Users.Update(user);
            }
            _context.SaveChangesAsync();
            return View();
        }
        
        // GET: Todo/Create
        public IActionResult Register()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //        public async Task<IActionResult> Create([Bind("Id,Text,DateCompleted,DateCreated,UserId,DateDue")] TodoItem todoItem)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username,string password)
        {
            if (username == null || password == null || username.Length == 0 || password.Length == 0)
            {
                return View();
            }
            
            if (ModelState.IsValid)
            {
                User user = new User(username, password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        
        public async Task<IActionResult> LogIn(string username,string password)
        {
            Boolean isLoggedOn = false;
            User user = new User(username, password);
            User inDb = await _context.Users.FirstOrDefaultAsync(u => u.username.Equals(user.username) && u.password.Equals(user.password));
            if (inDb != default(User))
            {
                isLoggedOn = true;
                inDb.isActive = true;
                _context.Update(inDb);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Todo", new {area = ""});
            }
            else
            {
                
            }
            
            return View("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            List<User> allUsers = await _context.Users.ToListAsync();
            foreach (var user in allUsers)
            {
                user.isActive = false;
                _context.Users.Update(user);
            }
            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}