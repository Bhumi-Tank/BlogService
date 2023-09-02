using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogServices.Data;
using BlogServices.Models;
using BlogServices.Services;

namespace BlogServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly BlogServicesContext _context;
        private readonly IUserService userService;

        public UsersController(BlogServicesContext context, IUserService userService)
        {
            _context = context;
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            this.userService = userService;
        }

        // POST: api/Users
        [HttpPost("/login")]
        public ActionResult<JwtToken> StartLogin(Users user)
        {
            if ( user.UserEmail == "firstuser@gmail.com" && user.Password == "password")
            {
                try
                {
                    var jwt = userService.loginRoute(user);
                    var jwtAsString = jwt.Token;

                    //var userid = userService.findUserIdForEmail(user.UserEmail);
                    var userid = 1;

                    // set the authentication tocken into cookies
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(1);
                    option.Path = "/";
                    option.IsEssential = true;

                    // clear out all cookies before sending response 
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }

                    Response.Cookies.Append("BlogAppToken", jwtAsString, option);
                    Response.Cookies.Append("uid", userid.ToString(), option);

                    return jwt;
                }
                catch(Exception e)
                {
                    return BadRequest("unable to creat jwt" + e);
                } 
            }
            else
            {
                return BadRequest("Username or password is incorrect");
            }

        }

        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Users != null ? 
        //                  View(await _context.Users.ToListAsync()) :
        //                  Problem("Entity set 'BlogServicesContext.Users'  is null.");
        //}

        //// GET: Users/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Users == null)
        //    {
        //        return NotFound();
        //    }

        //    var users = await _context.Users
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (users == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(users);
        //}

        //// GET: Users/Create
        //public IActionResult Create()
    //    {
    //        return View();
    //    }

    //    // POST: Users/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,UserEmail,Password")] Users users)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(users);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(users);
    //    }

    //    // GET: Users/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null || _context.Users == null)
    //        {
    //            return NotFound();
    //        }

    //        var users = await _context.Users.FindAsync(id);
    //        if (users == null)
    //        {
    //            return NotFound();
    //        }
    //        return View(users);
    //    }

    //    // POST: Users/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Id,UserEmail,Password")] Users users)
    //    {
    //        if (id != users.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(users);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!UsersExists(users.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        return View(users);
    //    }

    //    // GET: Users/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null || _context.Users == null)
    //        {
    //            return NotFound();
    //        }

    //        var users = await _context.Users
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (users == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(users);
    //    }

    //    // POST: Users/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        if (_context.Users == null)
    //        {
    //            return Problem("Entity set 'BlogServicesContext.Users'  is null.");
    //        }
    //        var users = await _context.Users.FindAsync(id);
    //        if (users != null)
    //        {
    //            _context.Users.Remove(users);
    //        }
            
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool UsersExists(int id)
    //    {
    //      return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    }
}
