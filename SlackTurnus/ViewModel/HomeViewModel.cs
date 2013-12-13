using System;
using System.Collections.Generic;

namespace SlackTurnus.ViewModel
{
	public class HomeViewModel
	{
		public readonly IEnumerable<Tuple<string, string>> PrimaryAndSecondarySlackers;

		public HomeViewModel(IEnumerable<Tuple<string, string>> primaryAndSecondarySlackers)
		{
			PrimaryAndSecondarySlackers = primaryAndSecondarySlackers;
		}
	}
}
