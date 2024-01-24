using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Login.Filter
{
    public class AuthorizationFilter : Attribute , IActionFilter
    {
        /// <summary>
        /// Do something before the action executes.
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 在這裡進行自訂的登入檢查邏輯
            string? Email = context.HttpContext.Request.Cookies["Email"];
            string? UserLogin = context.HttpContext.Request.Cookies["UserLogin"];

            // 檢查 action 是否有標註屬性 [ByCheck]，若有，則代表要檢查
            var byCheckAttribute = context.ActionDescriptor.EndpointMetadata.OfType<ByCheck>();

            if (Email == null && (UserLogin == "N" || UserLogin == null) && byCheckAttribute.Any())
            {
                // 使用者未登入，確保不是登入頁面，然後進行適當的處理
                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                if (descriptor != null &&  descriptor.ActionName != "Login")
                {
                    // 不是登入頁面，重新導向到登入頁面
                    context.Result = new RedirectToActionResult("Login", "Member", null);
                }
            }

           
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }
    }
}
