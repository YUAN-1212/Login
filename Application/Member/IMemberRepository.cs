using Application.Member.Dto;

namespace Application.Member
{
    public interface IMemberRepository
    {
        LoginDto DoLogin(LoginDto loginDto);
    }
}
