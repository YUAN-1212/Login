using Application.Infrastructure;
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

        /// <summary>
        /// 修改 帳號資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action">
        /// 1: 新增
        /// 2: 修改
        /// 3: 刪除
        /// </param>
        /// <param name="email">新增/更新 使用者的 email</param>
        MessageResult crudMemberData(MemberDataDto model,int action, string email);

        /// <summary>
        /// 註冊帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        MessageResult registerData(RegisterDto model);
    }
}
