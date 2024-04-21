using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetProject.Models;

namespace PetProject.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.PostId);
        builder.Property(p => p.PostName).IsRequired();
        builder.Property(p => p.PostDescription).IsRequired();
    }
}