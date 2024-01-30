﻿using Application.Infrastructure;
using Application.Member.Dto;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

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
                        member.Photo = (a.Photo != null && a.Photo != "") ? a.Photo : "~/assets/img/profile_empty.jpeg";
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
        public MessageResult crudMemberData(MemberDataDto model, int action, string email)
        {
            var message = new MessageResult();
            var addDate = DateTime.Now;

            message.valid = false;

            try
            {
                var m = db.MemberDatas.Where(p => p.ID == model.MemberID).FirstOrDefault();
                var account = db.AccountDatas.Where(p => p.MemberID == model.MemberID).FirstOrDefault();
                var user = db.MemberDatas.Where(p => p.Email == email).Select(p => p.Account).FirstOrDefault();

                if (m == null)
                {
                    message.valid = false;
                    message.message = "發生錯誤，無此帳號";
                    return message;
                }
                else
                {
                    if (action == 1)
                    {
                        #region 新增
                        if (db.MemberDatas.Where(p => p.Email == model.Email).Count() > 1)
                        {
                            // 信箱不可重複
                            message.valid = false;
                            message.message = "已有相同的信箱";
                            return message;
                        }

                        // 新增 AccountData 會員明細資料表
                        account = new AccountData()
                        {
                            MemberID = model.MemberID,
                            BirthDay = Convert.ToDateTime(model.BirthDay),
                            CellPhone = model.CellPhone,
                            Photo = model.Photo,
                            Role = model.RoleId,
                            Status = model.Status,
                            CreateDate = addDate,
                            CreateUser = user,
                            UpdateDate = addDate,
                            UpdateUser = user,
                        };

                        db.AccountDatas.Add(account);

                        // 修改 MemberData 主資料表
                        m.Name = model.Name;
                        m.Sex = model.SexId;
                        m.Email = model.Email;
                        m.UpdateDate = addDate;
                        db.Entry(m).State = EntityState.Modified;

                        message.valid = true;
                        message.message = "新增完成";
                        #endregion
                    }
                    else if (action == 2)
                    {
                        #region 修改
                        account = db.AccountDatas.Where(p => p.MemberID == model.MemberID).FirstOrDefault();
                        if (account == null)
                        {
                            message.valid = false;
                            message.message = "系統找不到指定資料";
                            return message;
                        }
                        else if (db.MemberDatas.Where(p => p.Email == model.Email).Count() > 1)
                        {
                            // 信箱不可重複
                            message.valid = false;
                            message.message = "已有相同的信箱";
                            return message;
                        }

                        // 修改 AccountData 會員明細資料表
                        account.BirthDay = Convert.ToDateTime(model.BirthDay);
                        account.CellPhone = model.CellPhone;
                        account.Photo = model.Photo;
                        account.Role = model.RoleId;
                        account.Status = model.Status;
                        account.UpdateUser = user;
                        account.UpdateDate = addDate;
                        db.Entry(account).State = EntityState.Modified;

                        // 修改 MemberData 主資料表
                        m.Name = model.Name;
                        m.Sex = model.SexId;
                        m.Email = model.Email;
                        m.UpdateDate = addDate;
                        db.Entry(m).State = EntityState.Modified;

                        message.valid = true;
                        message.message = "修改完成";
                        #endregion
                    }
                    else
                    {
                        #region 刪除
                        account = db.AccountDatas.Where(p => p.MemberID == model.MemberID).FirstOrDefault();
                        if (account == null)
                        {
                            message.valid = false;
                            message.message = "系統找不到指定資料";
                            return message;
                        }

                        // 刪除 AccountData 會員明細資料表
                        db.AccountDatas.Remove(account);

                        // 刪除 MemberData 主資料表
                        db.MemberDatas.Remove(m);

                        message.valid = true;
                        message.message = "刪除成功";

                        #endregion
                    }

                    db.SaveChanges();
                }

                return message;
            }
            catch(Exception ex)
            {
                message.valid = false;
                message.message = "發生錯誤，ex=" + ex.ToString();
                return message;
            }
        }
    }
}
