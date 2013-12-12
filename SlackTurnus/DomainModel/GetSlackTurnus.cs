using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace SlackTurnus.DomainModel
{
	public interface IGetSlackTurnus
	{
		IOrderedDictionary Execute();
	}

	public class GetSlackTurnus : IGetSlackTurnus
	{
		public IOrderedDictionary Execute()
		{
			string slackersAsJson;

			// 2do: DI http://stackoverflow.com/questions/6390608/structuremap-is-not-disposing-data-context-when-using-httpcontextscoped
			using (var streamReader = new StreamReader(HostingEnvironment.MapPath("~/DomainModel/slackTurnus.json")))
			{
				slackersAsJson = streamReader.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<OrderedDictionary>(slackersAsJson);
		}
	}
}
