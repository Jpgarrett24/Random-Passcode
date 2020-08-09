using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 1);
            }
            string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new StringBuilder();
            Random random = new Random();
            for (int i = 1; i <= 14; i++)
            {
                result.Append(pool[random.Next(0, pool.Length)]);
            }
            HttpContext.Session.SetString("Passcode", result.ToString());
            ViewBag.Passcode = HttpContext.Session.GetString("Passcode");
            ViewBag.Count = HttpContext.Session.GetInt32("Count");
            return View("Index");
        }

        [HttpPost("")]
        public IActionResult Generate()
        {
            int? Count = HttpContext.Session.GetInt32("Count");
            HttpContext.Session.SetInt32("Count", (int)Count + 1);
            return RedirectToAction("Index");
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}