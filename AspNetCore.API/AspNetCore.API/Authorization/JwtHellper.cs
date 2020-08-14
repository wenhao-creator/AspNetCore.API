using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AspNetCore.API.Model;
using AspNetCore.Common;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.API.Authorization
{
    /// <summary>
    /// Jwt 辅助类
    /// </summary>
    public class JwtHellper
    {
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="token">Token令牌</param>
        /// <returns></returns>
        public static string IssueJwt(Token token)
        {
            List<Claim> claims = new List<Claim>
            {
                /*
                   * 特别重要：
                     1、这里将用户的部分信息，比如 uid 存到了Claim 中，如果你想知道如何在其他地方将这个 uid从 Token 中取出来，
  　　　　　　　　　　　　　　请看下边的SerializeJwt() 方法，或者在整个解决方案，搜索这个方法，看哪里使用了！
                     2、你也可以研究下 HttpContext.User.Claims ，具体的你可以看看 Policys/PermissionHandler.cs 类中是如何使用的。
                   */

                //Jti 编号  Iat 签发时间  Nbf 生效时间  Exp 过期时间

                new Claim(JwtRegisteredClaimNames.Jti,token.Uid),
                new Claim(JwtRegisteredClaimNames.Iss,JwtAppSetting.Issuer()),
                new Claim(JwtRegisteredClaimNames.Aud,JwtAppSetting.Audience()),

                new Claim(JwtRegisteredClaimNames.Iat,$"{new DateTimeOffset( DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),

                //这个就是过期时间，目前是过期1000秒，可自定义，注意JWT有自己的缓冲过期时间
                new Claim(JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Expiration,DateTime.Now.AddSeconds(1000).ToString()),
            };

            // 可以将一个用户的多个角色全部赋予；
            claims.AddRange(token.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

            //秘钥 (SymmetricSecurityKey 对安全性的要求，密钥的长度太短会报出异常)
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAppSetting.SecretKey()));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: JwtAppSetting.Issuer(),
                claims: claims,
                signingCredentials: creds);

            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
            string encodedJwt = jwtHandler.WriteToken(jwt);

            return encodedJwt;
        }

        /// <summary>
        /// 解析Token
        /// </summary>
        /// <param name="jwtStr"></param>
        /// <returns></returns>
        public static Token SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            Token tm = new Token
            {
                Uid = jwtToken.Id.ToString(),
                Role = role?.ToString(),
            };
            return tm;
        }
    }
}