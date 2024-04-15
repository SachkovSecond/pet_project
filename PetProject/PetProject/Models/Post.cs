namespace PetProject.Models
{
    public class Post
    {
        public Post(int postId, string postName, string postDescription)
        {
            PostId = postId;
            PostName = postName;
            PostDescription = postDescription;
        }
        public int PostId { get;  set; } = 0;
        public string PostName { get;  set; } = string.Empty;
        public string PostDescription { get;  set; } = string.Empty ;
    }
}