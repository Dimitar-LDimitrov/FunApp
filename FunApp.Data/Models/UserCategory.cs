namespace FunApp.Data.Models
{
    public class UserCategory
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
