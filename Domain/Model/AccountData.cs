using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    /// <summary>
    /// 會員詳細資料
    /// </summary>
    [Table("AccountData")]
    public class AccountData
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 對應至 MemberData 的 ID 欄位
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 生日 YYYY/MM/DD
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 手機
        /// </summary>
        public string CellPhone { get; set; }

        /// <summary>
        /// 個人照片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 腳色
        /// 對應至 CodeTable 的 RoleID
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 新增的帳號
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 更新的帳號
        /// </summary>
        public string UpdateUser { get; set; }
    }
}
