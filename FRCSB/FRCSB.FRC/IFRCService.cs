using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FRCSB.FRC
{
	public interface IFRCService
	{
		 Task<List<Match>> getMatches();
		Task<List<EventGroup>> getMatchesGrouped();
	}
}
