﻿using System.Collections;
using System.Linq;
using System.Web.Mvc;
using SlackTurnus.DomainModel;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGetSlackTurnus _getSlackTurnus;
		private readonly IUpdateSlackTurnus _updateSlackTurnus;

		public HomeController(IGetSlackTurnus getSlackTurnus, IUpdateSlackTurnus updateSlackTurnus)
		{
			_getSlackTurnus = getSlackTurnus;
			_updateSlackTurnus = updateSlackTurnus;
		}

		public ActionResult Index()
		{
			return View(_getSlackTurnus.Execute());
		}

		public ActionResult Next()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var firstInLineSlacker = slackTurnus.Cast<DictionaryEntry>().Last();

			slackTurnus.Remove(firstInLineSlacker.Key);

			slackTurnus.Insert(unchecked((int)(long)firstInLineSlacker.Value), firstInLineSlacker.Key, 0);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}

		public ActionResult Skip()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var firstInLineSlacker = slackTurnus.Cast<DictionaryEntry>().Last();

			firstInLineSlacker.Value = (long)firstInLineSlacker.Value + 1;

			slackTurnus.Remove(firstInLineSlacker.Key);

			slackTurnus.Insert(slackTurnus.Count - 1, firstInLineSlacker.Key, firstInLineSlacker.Value);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}
	}
}
