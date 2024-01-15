using Application.Member.Dto;

namespace Application.Member
{
    public interface IMemberRepository
    {
        bool DoLogin(LoginDto loginDto);
    }
}
