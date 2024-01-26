using Application.Member.Dto;

namespace Application.Member
{
    public interface IMemberRepository
    {
        LoginDto DoLogin(LoginDto loginDto);

        /// <summary>
        /// 取得帳號資料
        /// </summary>
        /// <returns></returns>
        MemberDataDto GetMemberData(string Email);
    }
}
