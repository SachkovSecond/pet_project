using Microsoft.AspNetCore.Mvc;
using PetProject.DTO;
using PetProject.DataBase;
using PetProject.Models;
using Microsoft.EntityFrameworkCore;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public ToDoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostRequest request)
        {
            var post = new Post(request.PostId, request.PostName, request.PostDescription);
            await _dbContext.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return Ok(post);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll()
        {
            var posts = await _dbContext.posts.ToListAsync();
            if (!posts.Any())
                return NotFound();
            return Ok(posts);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetById(int id)
        {
            if (id == 0)
                return NotFound();
            var post = await _dbContext.FindAsync<Post>(id);
            return Ok(post);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Post>> Update(int id, CreatePostRequest request)
        {
            if (id == 0)
                return NotFound();
            var post = await _dbContext.FindAsync<Post>(id);
            post.PostName = request.PostName;
            post.PostDescription = request.PostDescription;
            await _dbContext.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == 0)
                return NotFound();
            var post = _dbContext.Remove(id);
            return Ok();
        }
    }
}