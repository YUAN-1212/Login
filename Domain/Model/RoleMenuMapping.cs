using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    /// <summary>
    /// 會員ID對應至哪個選單
    /// </summary>
    [Table("RoleMenuMapping")]
    public class RoleMenuMapping
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 會員ID
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// 腳色
        /// AccountData - Role
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 選單ID
        /// ConfigMenus - ID
        /// </summary>
        public int MenuID { get; set; }

        /// <summary>
        /// 狀態
        /// 0: 停用; 1: 啟用(預設)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 新增的帳號
        /// </summary>
        public string? CreateUser { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 更新的帳號
        /// </summary>
        public string? UpdateUser { get; set; }
    }

}
