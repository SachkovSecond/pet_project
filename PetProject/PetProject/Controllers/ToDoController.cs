using Microsoft.AspNetCore.Mvc;
using PetProject.DTO;
using PetProject.DataBase;
using PetProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
        public async Task<IActionResult> Create(CreatePostRequest request, CancellationToken ct)
        {
            var post = new Post(request.PostName, request.PostDescription);
            await _dbContext.Posts.AddAsync(post, ct);
            await _dbContext.SaveChangesAsync(ct);
            return Ok(post);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Post>>> GetAll(CancellationToken ct)
        {
            var posts = await _dbContext.Posts.AsNoTracking().ToListAsync(cancellationToken: ct);
            return Ok(posts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Post>> GetById(Guid id, CancellationToken ct)
        {
            var post = await _dbContext.Posts.FindAsync(new object?[] { id }, cancellationToken: ct);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Post>> Update(Guid id, CreatePostRequest request, CancellationToken ct)
        {
            var post = await _dbContext.Posts.FindAsync(new object?[] { id }, cancellationToken: ct);
            if (post == null)
                return NotFound();
            await _dbContext.Posts.Where(w => w.PostId == id).ExecuteUpdateAsync(s=>
                s.SetProperty(u=>u.PostName, u=>u.PostDescription), cancellationToken: ct);
            await _dbContext.SaveChangesAsync(ct);
            return Ok(post);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            //new object?[] { id } - предложил rider. Нормально ли так делать?
            var postToDelete = await _dbContext.Posts.FindAsync(new object?[] { id }, cancellationToken: ct);
            if (postToDelete == null)
                return NotFound();
            await _dbContext.Posts.Where(u => u.PostId == id).ExecuteDeleteAsync(cancellationToken: ct);
            await _dbContext.SaveChangesAsync(ct);
            return Ok();
        }
    }   
}