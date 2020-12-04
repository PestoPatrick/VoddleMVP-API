using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        [HttpPost]
        public async Task<ActionResult<string>> Login(Tbluser tbluser)
        {
            Tbluser username = await _context.Tblusers.FindAsync(tbluser.Userid);
            if (username == null)
            {
                return NotFound();
            }

            if (username.Passwordhash != tbluser.Passwordhash)
            {
                return Unauthorized();
            }

            var payload = new Dictionary<string, object>
            {
                {"iss", "Voddle.net"},
                {"aud", "Voddle.net"},
                {"jti", Guid.NewGuid()},
                {"exp", DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeSeconds()},
                {"username", username.Username},
            };

            string secret = _configuration.GetValue<string>("Jwt:Key");
            
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var token = encoder.Encode(payload, secret);
            Console.WriteLine(token);
            return token;

        }
    }
}