using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using SlackTurnus.DomainModel;
using SlackTurnus.ViewModel;

namespace SlackTurnus.Controllers
{
	public class HomeController : Controller
	{
		private const string PRIMARY_SLACKER_TURNUS = "primarySlackerTurnus";
		private const string SECONDARY_SLACKER_TURNUS = "secondarySlackerTurnus";
		private readonly IGetSlackTurnus _getSlackTurnus;
		private readonly IUpdateSlackTurnus _updateSlackTurnus;

		public HomeController(IGetSlackTurnus getSlackTurnus, IUpdateSlackTurnus updateSlackTurnus)
		{
			_getSlackTurnus = getSlackTurnus;
			_updateSlackTurnus = updateSlackTurnus;
		}

		public ActionResult Index()
		{
			var primarySlackerTurnus = _getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS).Cast<DictionaryEntry>();
			var secondarySlackerTurnus = _getSlackTurnus.Execute(SECONDARY_SLACKER_TURNUS).Cast<DictionaryEntry>();
			var slackerNamePairs = primarySlackerTurnus.Zip(secondarySlackerTurnus,
				(primaryDictionaryEntry, secondaryDictionaryEntry) => new Tuple<string, string>((string)primaryDictionaryEntry.Key, (string)secondaryDictionaryEntry.Key));

			return View(new HomeViewModel(slackerNamePairs.Reverse()));
		}

		// 2do: remove dupes
		public ActionResult NextPrimary()
		{
			var slackTurnus = _getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS);

			slackTurnus.Next();

			_updateSlackTurnus.Execute(slackTurnus, PRIMARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}

		public ActionResult SkipPrimary()
		{
			var slackTurnus = _getSlackTurnus.Execute(PRIMARY_SLACKER_TURNUS);

			slackTurnus.Skip();

			_updateSlackTurnus.Execute(slackTurnus, PRIMARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}

		public ActionResult NextSecondary()
		{
			var slackTurnus = _getSlackTurnus.Execute(SECONDARY_SLACKER_TURNUS);

			slackTurnus.Next();

			_updateSlackTurnus.Execute(slackTurnus, SECONDARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}

		public ActionResult SkipSecondary()
		{
			var slackTurnus = _getSlackTurnus.Execute(SECONDARY_SLACKER_TURNUS);

			slackTurnus.Skip();

			_updateSlackTurnus.Execute(slackTurnus, SECONDARY_SLACKER_TURNUS);

			return RedirectToAction("Index");
		}
	}
}
