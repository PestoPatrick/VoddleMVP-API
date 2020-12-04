using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace VoddleMVP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly voddlemvpContext _context;
        private IConfiguration _configuration;
        

        public authController(voddlemvpContext context, IConfiguration iconfig)
        {
            _context = context;
            _configuration = iconfig;
        }

        // Post the username and password to this route to get jwt token
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody]Tbluser tbluser)
        {
            Tbluser user = await _context.Tblusers.FindAsync(tbluser.Userid);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Passwordhash != tbluser.Passwordhash)
            {
                return Unauthorized();
            }

            var token = CreateJWT(user);
            Console.WriteLine(token);
            return token;

        }

        private string CreateJWT(Tbluser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var claims = new[]
            {
                new Claim("Username", user.Username),
                new Claim("jti", Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        
    }
}