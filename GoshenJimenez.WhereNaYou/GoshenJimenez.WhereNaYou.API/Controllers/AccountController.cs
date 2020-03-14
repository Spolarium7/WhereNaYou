using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoshenJimenez.WhereNaYou.API.Infrastructure.Domain.Data;
using GoshenJimenez.WhereNaYou.DataTransferObjects.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoshenJimenez.WhereNaYou.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DefaultDbContext _context;

        public AccountController(DefaultDbContext context)
        {
            _context = context;
        }

        //add group
        [HttpPost("/api/account/login")]
        public IActionResult Login([FromBody] LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var user = this._context.Users.FirstOrDefault(
                        u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() 
                );

            if(user == null)
            {
                return BadRequest();
            }
            else
            {
                if(user.Password != model.Password)
                {
                    user.LoginRetries = user.LoginRetries + 1;

                    if(user.LoginRetries > 2)
                    {
                        user.LoginStatus = Infrastructure.Domain.Models.Enums.LoginStatus.Inactive;
                    }
                    this._context.Users.Update(user);
                    this._context.SaveChanges();
                    return BadRequest();
                }
                else
                {
                    if(user.LoginStatus != Infrastructure.Domain.Models.Enums.LoginStatus.Active)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        user.LoginRetries = 0;
                        user.LoginStatus = Infrastructure.Domain.Models.Enums.LoginStatus.Active;
                        this._context.Users.Update(user);
                        this._context.SaveChanges();

                        return Ok(new LoginResponseDto()
                        {
                            Id = user.Id,
                            UserName = user.FirstName + " " + user.LastName
                        });
                    }
                }
            }
           

            this._context.SaveChanges();

            return Ok();
        }
    }
}