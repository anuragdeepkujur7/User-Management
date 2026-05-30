using Project_5_final.DTO.Education;
using Project_5_final.Models;
using Project_5_final.Repository.Interface;
using Project_5_final.Services.Interface;

namespace Project_5_final.Services.Implementation
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;

        public EducationService(IEducationRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        public async Task<List<Education>> GetEducationByUserIdAsync(int userId)
        {
            return await _educationRepository.GetEducationByUserIdAsync(userId);
        }
        public async Task<bool> AddEducationAsync(int userId, EducationDTO educationDTO)
        {
            var education = new Education
            {
                UserId = userId,
                Institution = educationDTO.Institution,
                Board = educationDTO.Board,
                Degree = educationDTO.Degree,
                FieldOfStudy = educationDTO.FieldOfStudy,
                StartDate = educationDTO.StartDate,
                EndDate = educationDTO.EndDate,
                Grade = educationDTO.Grade,
                
            };

            return await _educationRepository.AddEducationAsync(education);
        }

        public async Task<bool> UpdateEducationAsync(int userId, EducationDTO educationDTO)
        {
            var userEducationRecords = await _educationRepository.GetEducationByUserIdAsync(userId);
            var existingEducation = userEducationRecords.FirstOrDefault(e =>
                e.Institution == educationDTO.Institution &&
                e.Degree == educationDTO.Degree);
            if (existingEducation == null)
            {
                return false; // No matching record
            }

            existingEducation.Institution = educationDTO.Institution;
            existingEducation.Board = educationDTO.Board;
            existingEducation.Degree = educationDTO.Degree;
            existingEducation.FieldOfStudy = educationDTO.FieldOfStudy;
            existingEducation.StartDate = educationDTO.StartDate;
            existingEducation.EndDate = educationDTO.EndDate;
            existingEducation.Grade = educationDTO.Grade;

            return await _educationRepository.UpdateEducationAsync(existingEducation);
        }
    }
}
