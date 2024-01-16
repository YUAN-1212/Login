
namespace Application.Infrastructure
{
    /// <summary>
    /// 共用Dto
    /// </summary>

    public class MessageResult
    {
        /// <summary>
        /// 回傳給前端
        /// true; false
        /// </summary>
        public bool valid { get; set; }

        /// <summary>
        /// 回傳給前端的訊息
        /// </summary>
        public string message { get; set; }
    }
}
