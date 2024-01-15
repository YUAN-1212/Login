using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    /// <summary>
    /// 共用代碼
    /// </summary>
    [Table("CodeTable")]
    public class CodeTable
    {
        /// <summary>
        /// 流水號
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 代碼英文名稱
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// 代碼中文名稱
        /// </summary>
        public string CodeType { get; set; }

        /// <summary>
        /// 代碼
        /// </summary>
        public string CodeValue { get; set; }

        /// <summary>
        /// 代碼備註
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 代碼使用狀態
        /// 1:使用中
        /// 0:停用
        /// </summary>
        public string CodeStatus { get; set; }

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
