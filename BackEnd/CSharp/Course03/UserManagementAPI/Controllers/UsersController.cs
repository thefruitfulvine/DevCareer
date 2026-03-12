using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // In-memory list (temporary datanase alternative)
        private static readonly List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Kingsley", Email = "kingsley@techhive.com", Password = "pass123"},
            new User { Id = 2, Name = "David", Email = "david@techhive.com", Password = "pass456"}
        };

        // Get: api/users
        [HttpGet]
        public IActionResult GetUsers() => Ok(users);

        // Get: api/users/{id} (get user by id)
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                return user == null ? NotFound() : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/users add a new user
        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            // Auto-generate Id
            newUser.Id = users.Max(u => u.Id) + 1;
            users.Add(newUser);

            // Return 201 Created with link to the new resource
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/{id} update existing user record
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            return Ok(user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}