using Application.DTO.Post;
using Application.Validation;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.DataBase;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post> Create(CreatePostRequest request, CancellationToken ct)
    {
        var validator = new PostValidator();
        var post = new Post(request.PostName, request.PostDescription);
        var result = await validator.ValidateAsync(post, ct);
        if (result.IsValid)
        {
            await _dbContext.Posts.AddAsync(post, ct);
            await _dbContext.SaveChangesAsync(ct);
        }
        return post;
    }

    public async Task<List<Post>> GetAll(CancellationToken ct)
    {
        return await _dbContext.Posts.AsNoTracking().ToListAsync(cancellationToken: ct);
    }

    public async Task<Post> GetById(Guid id, CancellationToken ct)
    {
        return await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.PostId == id, cancellationToken: ct);
        // await _dbContext.Posts.FindAsync(new object?[] { id }, cancellationToken: ct);
    }

    public Post Update(Guid id, UpdatePostRequest request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}