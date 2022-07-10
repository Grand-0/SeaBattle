using Microsoft.AspNetCore.Http;

namespace API.Models.Users
{
    public class UpdatedUser : UserBase
    {
        public IFormFile Logo { get; set; }
    }
}
