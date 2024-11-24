using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisEngeman.Controllers
{
    public class ListaOSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
