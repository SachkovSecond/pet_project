using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTO.Post;
using Application.Validation;
using Infrastructure.DataBase;
using Domain.Models;
using Infrastructure.Repositories.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPostRepository _repository;

        public ToDoController(ApplicationDbContext dbContext, IPostRepository repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request, CancellationToken ct)
        {
            // var validator = new PostValidator();
            // var post = new Post(request.PostName, request.PostDescription);
            // var result = await validator.ValidateAsync(post, ct);
            // if (result.IsValid)
            // {
            //     await _dbContext.Posts.AddAsync(post, ct);
            //     await _dbContext.SaveChangesAsync(ct);
            //     return Ok(post);
            // }
            // else return BadRequest();
            await _repository.Create(request, ct);
            return Ok();
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
        public async Task<ActionResult<Post>> Update(Guid id, UpdatePostRequest request, CancellationToken ct)
        {
            var isUpdate = await _dbContext.Posts.Where(w => w.PostId == id).ExecuteUpdateAsync(s =>
                s.SetProperty(u => u.PostName, request.PostName)
                    .SetProperty(u => u.PostDescription, request.PostDescription), cancellationToken: ct);
            if (isUpdate < 1)
                return NotFound();
            var validator = new PostValidator();
            var postToValidate = await _dbContext.Posts.FindAsync(id, ct);
            if (postToValidate != null)
            {
                var result = await validator.ValidateAsync(postToValidate, ct);
                if (!result.IsValid)
                {
                    return BadRequest();
                }
            }

            await _dbContext.SaveChangesAsync(ct);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            var isDelete = await _dbContext.Posts.Where(u => u.PostId == id).ExecuteDeleteAsync(cancellationToken: ct);
            if (isDelete < 1)
                return NotFound();
            await _dbContext.SaveChangesAsync(ct);
            return Ok();
        }
    }
}