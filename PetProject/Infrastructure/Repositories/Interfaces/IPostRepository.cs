using Application.DTO.Post;
using Domain.Models;

namespace Infrastructure.Repositories.Interfaces;

public interface IPostRepository
{
    public Task<Post> Create(CreatePostRequest request, CancellationToken ct);
    public Task<List<Post>> GetAll(CancellationToken ct);
    public Task<Post> GetById(Guid id, CancellationToken ct);
    public Post Update(Guid id, UpdatePostRequest request, CancellationToken ct);
    public void Delete(Guid id, CancellationToken ct);
}