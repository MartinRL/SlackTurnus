using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

		private ActionResult Next(string turnus)
		{
			var slackTurnus = _getSlackTurnus.Execute(turnus);

			slackTurnus.Next();

			_updateSlackTurnus.Execute(slackTurnus, turnus);

			return RedirectToAction("Index");
		}
		
		public ActionResult NextPrimary()
		{
			return Next(PRIMARY_SLACKER_TURNUS);
		}

		public ActionResult NextSecondary()
		{
			return Next(SECONDARY_SLACKER_TURNUS);
		}

		private ActionResult Skip(string turnus)
		{
			var slackTurnus = _getSlackTurnus.Execute(turnus);

			slackTurnus.Skip();

			_updateSlackTurnus.Execute(slackTurnus, turnus);

			return RedirectToAction("Index");
		}

		public ActionResult SkipPrimary()
		{
			return Skip(PRIMARY_SLACKER_TURNUS);
		}

		public ActionResult SkipSecondary()
		{
			return Skip(SECONDARY_SLACKER_TURNUS);
		}
	}
}
