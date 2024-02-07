using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    /// <summary>
    /// 會員詳細資料
    /// </summary>
    [Table("ConfigMenus")]
    public class ConfigMenus
    {
        /// <summary>
        /// 取得或設定功能編號資料
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 選單名稱
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 選單排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父選單，預設0，代表最上層的
        /// </summary>
        public int ParentID { get; set; }

        /// <summary>
        /// 選單路徑
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 選單狀態
        /// 0: 停用; 1: 啟用(預設)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 選單描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 選單代碼
        /// </summary>
        public string Code { get; set; }

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
