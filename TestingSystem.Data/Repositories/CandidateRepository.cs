using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Models;

namespace TestingSystem.Data.Repositories
{
	public interface ICandidateRepository : IRepository<Candidate>
	{
		int AddCandidate(Candidate candidate);
		bool CheckExistCandidatesByID(int candidateID);
	}
	public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		public CandidateRepository(IDbFactory dbFactory) : base(dbFactory)
		{

		}
		public int AddCandidate(Candidate candidate)
		{
			try
			{
				DbContext.Candidates.Add(candidate);
				DbContext.SaveChanges();
				return candidate.CandidateID;
			}
			catch (Exception e)
			{
				log.Debug(e.Message);
				return 0;
			}
		}

		public bool CheckExistCandidatesByID(int candidateID)
		{
			try
			{
				var candidate = DbContext.Candidates.SingleOrDefault(x => x.CandidateID == candidateID);
				if (candidate!= null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception e)
			{
				return false;
			}

		}
	}
}
