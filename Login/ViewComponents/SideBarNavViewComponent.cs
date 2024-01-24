using Microsoft.AspNetCore.Mvc;

namespace Login.ViewComponents
{

    // 須繼承ViewComponent
    public class SideBarNavViewComponent : ViewComponent
    {
        //用這個： async Task<IViewComponentResult> InvokeAsync
        //要接的參數隨意
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
