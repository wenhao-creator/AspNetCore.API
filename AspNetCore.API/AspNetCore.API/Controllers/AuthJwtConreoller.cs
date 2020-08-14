using AspNetCore.API.Authorization;
using AspNetCore.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.API.Controllers
{
    /// <summary>
    /// Auth 控制器
    /// </summary>
    public class AuthJwtConreoller : BaseController
    {
        public AuthJwtConreoller()
        {
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        //[ApiExplorerSettings(IgnoreApi =true)]//隐藏特性
        public IActionResult Get() => Ok("您有访问权限");

        /// <summary>
        /// 账号登陆
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns>token</returns>
        [HttpPost("LoginValidate")]
        public IActionResult LoginValidate(LoginModelView loginModel)
        {
            string token = string.Empty;
            bool success = false;

            if(loginModel != null)
            {
                if(loginModel.UserName == "admin" && loginModel.Password == "123456")
                {
                    token = JwtHellper.IssueJwt(new Token
                    {
                        Uid = loginModel.UserName,
                        Role = loginModel.Role
                    });
                    success = true;
                }
            }

            return Ok(new
            {
                token,
                success
            });
        }

        /// <summary>
        /// token 解析
        /// </summary>
        /// <returns></returns>
        [HttpPost("ParseToken")]
        [Authorize]
        public IActionResult ParseToken()
        {
            //截取 http header {Bearer} token
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            return Ok(JwtHellper.SerializeJwt(token));
        }
    }
}