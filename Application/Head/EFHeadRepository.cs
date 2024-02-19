using Application.Head.Dto;
using Application.Member.Dto;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Head
{
    public class EFHeadRepository
    {
        private readonly WebDbContext db = new WebDbContext();

        //        select
        //*
        //from RoleMenuMapping as a
        //inner join ConfigMenus as b
        //on a.MenuID = b.id
        //where a.AccountID = 1 --lily
        //and ParentID = 0

        public HeaderDto GetHeader(int AccountID)
        {
            var header = new HeaderDto();

            try
            {
                // 先找出父選單 (ParentID == 0)
                var ParentDto = (from a in db.RoleMenuMappings
                                 join b in db.ConfigMenuss on a.MenuID equals b.ID
                                 where a.MenuID == AccountID && b.ParentID == 0
                                 orderby b.Order
                                 select new
                                 {
                                     a.ID,
                                     b.Name,
                                     b.ParentID,
                                     b.URL,
                                     b.Status,

                                 }
                                ).Select(a => new ParentDto()
                                {


                                }).ToList();

                return header;
            }
            catch(Exception  ex)
            {
                throw ex;
            }
        }
    }
}
