using Application.Infrastructure;

namespace Application.Member.Dto
{
    public class LoginDto : MessageResult
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
