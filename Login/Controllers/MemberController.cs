using Application.Member;
using Application.Member.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _service;

        public MemberController(IMemberRepository service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoLogin([FromBody] LoginDto login)
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
                url = "/Member/Blank";
            }

            return Json(new { vaild = isok.valid, url = url, msg = isok.message });
        }

        public IActionResult Blank()
        {
            return View();
        }
    }
}
