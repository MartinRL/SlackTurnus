using System.Collections.Specialized;
using System.IO;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace SlackTurnus.DomainModel
{
	public interface IUpdateSlackTurnus
	{
		void Execute(IOrderedDictionary slackTurnus);
	}

	public class UpdateSlackTurnus : IUpdateSlackTurnus
	{
		public void Execute(IOrderedDictionary slackTurnus)
		{
			// 2do: DI
			using (var streamWriter = new StreamWriter(HostingEnvironment.MapPath("~/DomainModel/slackTurnus.json")))
			{
				streamWriter.Write(JsonConvert.SerializeObject(slackTurnus));
			}
		}
	}
}
