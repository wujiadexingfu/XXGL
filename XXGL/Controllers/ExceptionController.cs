﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XXGL.Controllers
{
    public class ExceptionController : Controller
    {
        // GET: Exception
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }

    }
}