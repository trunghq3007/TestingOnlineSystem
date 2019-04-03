using System.Collections.Generic;
using TestingSystem.Data.Infrastructure;
using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
	public interface ICandidatesTestService
	{
		IEnumerable<Candidate> GetAllCandidatesByTestID(int testID);
		int AddCandidatesIntoTest(int candidatesID, int testID);
		int RemoveCadidatesFromTest(int cadidatesID,int testID);
        List<int> GetAllTestIdByCandidateID(int candidateID);
        string GetNameTestByID(int testID);
        bool checkExistCandidateInCandidatesTest(int candidateID, int testID);
	}
	public class CandidatesTestService : ICandidatesTestService
	{
		private readonly ICandidatesTestRepository _candidatesTestRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CandidatesTestService(ICandidatesTestRepository candidatesTestRepository, IUnitOfWork unitOfWork)
		{
			_candidatesTestRepository = candidatesTestRepository;
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Candidate> GetAllCandidatesByTestID(int testID)
		{
			return _candidatesTestRepository.GetAllCandidatesByTestID(testID);
		}

		public int AddCandidatesIntoTest(int candidatesID, int testID)
		{
			return _candidatesTestRepository.AddCandidatesIntoTest(candidatesID, testID);
		}

		public int RemoveCadidatesFromTest(int cadidatesID,int testID)
		{
			return _candidatesTestRepository.RemoveCadidatesFromTest(cadidatesID,testID);
		}

        public List<int> GetAllTestIdByCandidateID(int candidateID)
        {
            return _candidatesTestRepository.GetAllTestIdByCandidateID(candidateID);
        }

        public string GetNameTestByID(int testID)
        {
	        return _candidatesTestRepository.GetNameTestByID(testID);
        }

        public bool checkExistCandidateInCandidatesTest(int candidateID, int testID)
        {
	        return _candidatesTestRepository.checkExistCandidateInCandidatesTest(candidateID, testID);
        }
	}
}
