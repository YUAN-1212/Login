using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class HeadController : Controller
    {
        #region : : : 側邊選單 : : :

        public IActionResult SubMenu()
        {
            return View();
        }

        #endregion
    }
}
