namespace PetProject.Models
{
    public class Post
    {
        public Post(){}
        public Post(string postName, string postDescription)
        {
            PostName = postName;
            PostDescription = postDescription;
        }
        public Guid PostId { get;  private set; }
        public string PostName { get;  set; } = string.Empty;
        public string PostDescription { get;  set; } = string.Empty ;
    }
}