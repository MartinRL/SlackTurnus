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
			return View(_getSlackTurnus.Execute().Cast<DictionaryEntry>().Reverse());
		}

		public ActionResult Next()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			slackTurnus.Next();

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}

		public ActionResult Skip()
		{
			var slackTurnus = _getSlackTurnus.Execute();

			var firstInLineSlacker = slackTurnus.Cast<DictionaryEntry>().First();

			var numberOfSkips = (long) slackTurnus[0] + 1;
			slackTurnus[0] = numberOfSkips;

			var index = 0;
			while ((long)slackTurnus[index] != 0)
			{
				index++;
			}

			slackTurnus.Remove(firstInLineSlacker.Key);

			slackTurnus.Insert(index, firstInLineSlacker.Key, numberOfSkips);

			_updateSlackTurnus.Execute(slackTurnus);

			return RedirectToAction("Index");
		}
	}
}
