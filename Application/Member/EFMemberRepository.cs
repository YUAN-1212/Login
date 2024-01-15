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
        public bool DoLogin(LoginDto loginDto)
        {
            var isok = false;

            try
            {
                var pw = loginDto.Password.ToMD5();
                var user = db.MemberDatas.Where(p => p.PassWord == pw && p.Email == loginDto.Email).FirstOrDefault();

                if (user != null)
                {
                    isok = true;
                }

                return isok;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
