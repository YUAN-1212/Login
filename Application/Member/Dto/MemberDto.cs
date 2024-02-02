using Application.Infrastructure;

namespace Application.Member.Dto
{
    public class LoginDto : MessageResult
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class MemberDataDto : MessageResult
    {
        public int MemberID { get; set; }

        /// <summary>
        /// 帳號名稱，不可修改
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string Name { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 性別代碼
        /// </summary>
        public int SexId { get; set; }

        /// <summary>
        /// 性別中文名稱
        /// </summary>
        public string Sex { get; set; }

        public string BirthDay { get; set; }

        public string CellPhone { get; set; }

        /// <summary>
        /// 圖片檔名，如無，則用預設圖片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 圖片(base64)
        /// </summary>
        public string base64String { get; set; }

        /// <summary>
        /// 權限代碼
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 權限名稱
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 帳號啟用狀態
        /// </summary>
        public int Status { get; set; }
    }
}
