using TestingSystem.Data.Repositories;
using TestingSystem.Models;

namespace TestingSystem.Sevice
{
    public interface ICandidateService
    {
        int AddCandidate(Candidate candidate);
        bool CheckExistCandidatesByID(int candidateID);
	}
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }
        public int AddCandidate(Candidate candidate)
        {
            return candidateRepository.AddCandidate(candidate);
        }

        public bool CheckExistCandidatesByID(int candidateID)
        {
	        return candidateRepository.CheckExistCandidatesByID(candidateID);
        }
    }
}
