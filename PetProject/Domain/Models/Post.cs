using Domain.Common;
using CSharpFunctionalExtensions;


namespace Domain.Models
{
    public class Post
    {
        public Post(){}
        public Post(string postName, string postDescription)
        {
            PostName = postName;
            PostDescription = postDescription;
        }
        public Guid PostId { get;  init; }
        public string PostName { get;  init; } = string.Empty;
        public string PostDescription { get;  init; } = string.Empty ;

        public static Result<Post, Error> Create(string name, string description)
        {
            if (name == null)
                return Errors.General.ValueIsInvalid(name);
            if(description == null)
                return Errors.General.ValueIsInvalid(description);

            return new Post(name, description);
        }
    }
}