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

        /// <summary>
        /// 取得帳號資料
        /// </summary>
        /// <returns></returns>
        public MemberDataDto GetMemberData(string Email)
        {
            var member = new MemberDataDto();

            try
            {
                var m = db.MemberDatas.Where(p => p.Email == Email).FirstOrDefault();

                if (m != null)
                {
                    var rrr = db.AccountDatas.ToList();
                    var a = db.AccountDatas.Where(p => p.MemberID == m.ID).FirstOrDefault();

                    if(a != null)
                    {
                        member.MemberID = a.ID;
                        member.Account = m.Account;
                        member.Name = m.Name;
                        member.Email = m.Email;
                        member.SexId = m.Sex;

                        string sex = m.Sex.ToString();
                        member.Sex = db.CodeTables.Where(p => p.CodeName == "Sex" && p.CodeValue == sex).Select(p => p.Memo).FirstOrDefault();

                        member.BirthDay = a.BirthDay.HasValue ? a.BirthDay?.ToString("yyyy-MM-dd") : null;                        
                        member.CellPhone = a.CellPhone;
                        member.Photo = (a.Photo != null || a.Photo != "") ? a.Photo : "~/img/profile_empty.jpeg";
                        member.RoleId = a.Role;

                        string role = a.Role.ToString();
                        member.Role = db.CodeTables.Where(p => p.CodeName == "RoleID" && p.CodeValue == role).Select(p => p.Memo).FirstOrDefault();

                        member.Status = a.Status;
                        member.valid = true;
                    }
                    else
                    {
                        #region 新帳號
                        
                        #endregion
                    }
                }
                else
                {
                    #region
                    member.valid = false;
                    member.message = "發生錯誤，無此帳號資料 !!";
                    //throw new Exception("發生錯誤，無此帳號資料 !!");
                    #endregion
                }

                return member;
            }
            catch(Exception ex)
            {
                member.valid = false;
                member.message = ex.Message;
                throw ex;
            }
        }
    }
}
