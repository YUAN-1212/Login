using Application.Member;
using Application.Member.Dto;
using Login.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Policy;
using Microsoft.AspNetCore.Http; // for IFormFile and IFormFileCollection


namespace Login.Controllers
{
    [AuthorizationFilter]
    public class MemberController : Controller
    {
        private readonly IMemberRepository _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string root = "wwwroot";
        //private readonly UserManager<IdentityUser> userManager;
        //private readonly SignInManager<IdentityUser> signInManager;

        // session 使用的參數
        const string Email = "Email";
        const string userName = "_UserName";

        public MemberController(IMemberRepository service
            , IWebHostEnvironment webHostEnvironment
            //, UserManager<IdentityUser> userManager
            //, SignInManager<IdentityUser> signInManager
            )
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
            //this.userManager = userManager;
            //this.signInManager = signInManager;
        }

        //[AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 頁面一載入，先檢查有無登入過
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult checkLogin() 
        {
            bool vaild = false;
            string url = "";
            string message = "";

            string test = HttpContext.Session.GetString("Email");
            string test2 = HttpContext.Session.GetString("UserLogin");

            string? test3 = Request.Cookies[Email];
            string? test4 = Request.Cookies["UserLogin"];

            // 檢查會員 Cookies 是否存在
            if (Request.Cookies[Email] == null && 
                (Request.Cookies["UserLogin"] == "N" || Request.Cookies["UserLogin"] == null))
            {
                message = "無會員登入記錄";                
            }
            else
            {
                vaild = true;
                message = "已登入";
                url = "/Member/Blank";
            }

            return Json(new { vaild = vaild, url = url, msg = message });
        }

        /// <summary>
        /// 進行登入
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DoLogin([FromBody] LoginDto login)
        {
            if (string.IsNullOrEmpty(login.Email))
            {
                return Json(false, "請輸入 Email !!");
            }

            if (string.IsNullOrEmpty(login.Password))
            {
                return Json(false, "請輸入 密碼 !!");
            }

            var isok = _service.DoLogin(login);
            var url = "";

            if (isok.valid) 
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(Email, login.Email, options);
                Response.Cookies.Append("UserLogin", "Y", options);

                // 將登入帳號記錄在 Session 內
                HttpContext.Session.SetString(Email, login.Email); 
                HttpContext.Session.SetString("UserLogin", "Y"); //Y: 使用者已登入; N:使用者未登入OR登入失敗


                url = "/Member/Blank";
            }
            else
            {
                HttpContext.Session.SetString("UserLogin", "N");
            }            

            return Json(new { vaild = isok.valid, url = url, msg = isok.message });
        }

        #region ::: 註冊 :::
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <returns></returns>
        public IActionResult DoRegister(RegisterDto model)
        {
            var isok = _service.registerData(model);
            var url = "";

            if (isok.valid) 
            {
                url = "/Member/Blank";

                // 將登入帳號記錄在 Cookies 內
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Append(Email, model.Email);
                Response.Cookies.Append("UserLogin", "Y");

                return Json(new { vaild = isok.valid, msg = isok.message, url = url });
            }
            else
            {
                return Json(new { vaild = false, msg = isok.message });
            }
            //return Json(new { vaild = isok.valid, url = url, msg = isok.message });            
        }

        #endregion

        #region ::: 個人資料頁 :::
        /// <summary>
        /// 個人資料頁
        /// </summary>
        /// <returns></returns>
        [ByCheck]
        public IActionResult Blank()
        {
            return View();
        }

        /// <summary>
        /// 個人資料頁 - 取得個人資料
        /// </summary>
        /// <returns></returns>
        [ByCheck, HttpPost]
        public IActionResult GetMemberData()
        {
            string Email = Request.Cookies["Email"].ToString();

            if (!string.IsNullOrWhiteSpace(Email))
            {
                var obj = _service.GetMemberData(Email);

                return Json(new { vaild = obj.valid, result = obj });
            }
            else
            {
                return Json(new { vaild = false, msg = "已被登出，請重新登入!", url= "~/Member/Login" });
            }
        }

        /// <summary>
        /// 個人資料頁 - 進行CRUD
        /// </summary>
        /// <param name="action">
        /// 1: 新增
        /// 2: 修改
        /// 3: 刪除
        /// </param>
        /// <returns></returns>
        [ByCheck, HttpPost]
        public IActionResult crudMemberData(MemberDataDto model, string imgPhoto, IFormFile fileInfo, int action)//[FromBody]
        {
            if(action == 1 || action == 2 || action == 3)
            {   
                var obj = _service.crudMemberData(model, action, Request.Cookies["Email"].ToString());

                if (obj.valid)
                {
                    // 修改名為 "Email" 的 Cookie 的值
                    Response.Cookies.Append("Email", model.Email);

                    this.UploadFile(model.Photo, fileInfo);
                }
                return Json(new { vaild = obj.valid, msg = obj.message });

                //return Json(new { vaild = false, msg = "無法辨識的操作" });
            }
            else
            {
                return Json(new { vaild = false, msg = "無法辨識的操作" });
            }
        }

        /// <summary>
        /// 上傳照片
        /// </summary>
        /// <param name="newFileName">新檔名稱</param>
        /// <param name="fileInfo">檔案內容</param>
        private void UploadFile(string newFileName, IFormFile fileInfo)
        {
            if (fileInfo != null)
            {
                var folderPath = @$"{root}\img\photo";

                // 上傳新檔
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //string fileExt = Path.GetExtension(fileInfo.FileName).ToLower();
                string name = fileInfo.FileName.Split('.')[0];
                var savePath = Path.Combine(folderPath, $"{name}.jpg");
                if (System.IO.File.Exists(savePath))
                {
                    FileInfo file = new FileInfo(savePath);
                    file.Delete();
                }
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    fileInfo.CopyTo(stream);
                };
            }
        }

        #endregion



        public IActionResult Blank2()
        {


            return View();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Logut()
        {
            //登出要清除
            //HttpContext.Session.Clear();
            return View();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            // 登出要清除
            //HttpContext.Session.Clear();

            Response.Cookies.Delete(Email);
            Response.Cookies.Delete("UserLogin");

            //await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
