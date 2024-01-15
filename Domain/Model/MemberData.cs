using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    /// <summary>
    /// 會員資料
    /// </summary>
    [Table("MemberData")]
    public class MemberData
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 中文名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密碼，以 MD5 加密
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 性別
        /// 對應至 CodeTable 的 Sex
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 信箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 新增時間
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
