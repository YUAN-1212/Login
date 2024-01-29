using System.Text;
using System.Text.RegularExpressions;

namespace Application.Infrastructure
{
    public static class Common
    {
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMD5(this string str)
        {
            using (var cryptoMD5 = System.Security.Cryptography.MD5.Create())
            {
                //將字串編碼成 UTF8 位元組陣列
                var bytes = Encoding.UTF8.GetBytes(str);

                //取得雜湊值位元組陣列
                var hash = cryptoMD5.ComputeHash(bytes);

                //取得 MD5
                var md5 = BitConverter.ToString(hash)
                  .Replace("-", string.Empty)
                  .ToUpper();

                return md5;
            }
        }

        /// <summary>
        /// 檢查email格式
        /// </summary>
        /// <param name="txtemail"></param>
        /// <returns></returns>
        public static bool checkEmail(string emailInput)
        {
            string email = emailInput;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }

        /// <summary>
        /// 檢查手機格式
        /// </summary>
        /// <param name="phoneInput"></param>
        /// <returns></returns>
        public static bool checkCellPhone(string phoneInput)
        {
            string phone = phoneInput;
            Regex regex = new Regex(@"^09[0-9]{8}$");
            Match match = regex.Match(phoneInput);
            return match.Success;
        }

        /// <summary>
        /// https://blog.uwinfo.com.tw/auth/article/reiko/378
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}
