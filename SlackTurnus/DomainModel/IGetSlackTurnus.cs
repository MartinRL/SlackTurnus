using System.Collections.Specialized;

namespace SlackTurnus.DomainModel
{
	public interface IGetSlackTurnus
	{
		IOrderedDictionary Execute();
	}
}