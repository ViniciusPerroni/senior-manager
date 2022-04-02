using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorManager.WebMvc.Controllers
{
    public class AcessoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NaoAutorizado()
        {
            return View();
        }
    }
}
