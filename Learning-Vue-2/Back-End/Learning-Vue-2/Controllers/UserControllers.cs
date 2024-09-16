using Learning_Vue_2.Data;
using Learning_Vue_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Learning_Vue_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers() //retorna array de objetos
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);  //devolver los usuarios en formato JSON
        }

        // GET: api/user/UserId
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId) //retorna un objeto
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null){
                return NotFound();
            }
            return Ok(user);
        }
        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Users newUser)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { userId = newUser.Id }, newUser); //deberia retornar 201
        }
        // PUT: api/user
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] Users updateUser)
        {
            //que exista el usuario con ese id
            //que exista
            //que tenga la estructura
            if(updateUser.Id!=userId){
                return BadRequest("id no coincide");
            }
            if(!ModelState.IsValid){
                return BadRequest(ModelState.ToString());
            }
            var user = await _context.Users.FindAsync(userId);
            if(user==null){
                return NotFound();//404
            }

            user.Email = updateUser.Email;
            user.Name = updateUser.Name;

            await _context.SaveChangesAsync();
            return NoContent();//204
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user==null){
                return NotFound();//404
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();//204
        }
    }
}
