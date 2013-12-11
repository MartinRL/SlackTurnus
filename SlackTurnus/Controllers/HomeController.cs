using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new OrderedDictionary());
		}
	}
}
