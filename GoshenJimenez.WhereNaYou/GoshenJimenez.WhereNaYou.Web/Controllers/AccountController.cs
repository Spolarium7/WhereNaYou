using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoshenJimenez.WhereNaYou.Web.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace GoshenJimenez.WhereNaYou.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = new RestClient("http://localhost:5000/api/account/login");
            var request = new RestRequest();
            request.Method = Method.POST;
            request.AddJsonBody(model);

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                var result = JsonConvert.DeserializeObject<object>(response.Content);
                return Ok(result);
            }
            else
            {
                ModelState.AddModelError("","Invalid login");
                return View();
            }
        }
    }
}