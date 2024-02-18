using Login.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [AuthorizationFilter]
    public class HeadController : Controller
    {
        #region : : : 側邊選單 : : :

        public IActionResult SubMenu()
        {
            return View();
        }

        /// <summary>
        /// 取得側邊選單
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSideBar()
        {
            string code = Request.Cookies["code"].ToString();


            //return Json(new { vaild = false, msg = "無法辨識的操作" });
            return Json(new { vaild = false, msg = "" });
        }

        #endregion
    }
}
