using Application.Infrastructure;
using Application.Member.Dto;
using Domain.Model;

namespace Application.Member
{
    public class EFMemberRepository : IMemberRepository
    {
        private readonly WebDbContext db = new WebDbContext();

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public LoginDto DoLogin(LoginDto loginDto)
        {
            try
            {
                var pw = loginDto.Password.ToMD5();
                var user = db.MemberDatas.Where(p => p.PassWord == pw && p.Email == loginDto.Email).FirstOrDefault();

                if (user != null)
                {
                    loginDto.valid = true;
                }
                else
                {
                    loginDto.valid = false;
                    loginDto.message = "無此會員!";
                }

                return loginDto;
            }
            catch (Exception ex)
            {
                loginDto.valid = false;
                loginDto.message = ex.Message;
                throw ex;
            }
        }
    }
}
