﻿using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository.Interface;
using DataAccessLayer.Repository.Interface;

namespace BusinessLayer.Services.Services
{
    public class FeedbackService : IFeedBackService
    {
        private readonly IGenericRepository<Feedback> _context;
        private readonly ILogger<FeedbackService> _logger;
        private readonly IFeedBackRepository _repository;
        public FeedbackService(IGenericRepository<Feedback> context, ILogger<FeedbackService> logger, IFeedBackRepository repository)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
        }
        public async Task<ServiceResponse> AddFeedBack(FeedbackDTO feedback)
        {
            try {
                var data = await _repository.AddData(feedback.MemberId,feedback.Comment);
                if (data== 1)
                {
                    _logger.LogInformation("Your feedback has beed submitted");
                    return new ServiceResponse(true, "Feedback has beed added Successfully");
                }
                _logger.LogInformation("Your feedback can not be submitted");
                return new ServiceResponse(false, "Feedback can not be added Successfully");
            } catch (Exception ex) {
                _logger.LogError("Something went Wrong");
                return new ServiceResponse(false, ex.Message);
            }
        }

        public async Task<ServiceResponse> DeleteFeedBack(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    _logger.LogError("Please Enter the valid Id");
                    return new ServiceResponse(false, "Please Enter the Valid Id");
                }
                var data = await _context.GetDataById(id);
                if (data ==null)
                {
                    _logger.LogInformation($"Feedback is not found with this {id}");
                    return new ServiceResponse(false, $"Feedback is not found with this {id}");
                }
                await _context.DeleteData(data);
                return new ServiceResponse(true,"Feedback is deleted Sucessfully");

            } catch (Exception ex) {
                _logger.LogError("An Error Occured while Deleteing Feedback");
                return new ServiceResponse(false,ex.Message);
            }
        }

        public async Task<List<FeedbackData>> GetAllFeedBacks()
        {
            try {
                var data = await _repository.GetAllData();
                if (!data.Any())
                {
                    throw new Exception("Data of Feedback is not found");
                }
                return data;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FeedbackData> GetFeedBack(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    _logger.LogWarning("Please Enter the Valid id");
                    throw new Exception("Please Enter the Valid Id");
                }
                var data = await _repository.GetData(id);
                return data;
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse> UpdateFeedBack(int? id , FeedbackDTO feedback)
        {
            try {
                var data= await _repository.GetData(id?? 0);
                if (data == null)
                {
                    _logger.LogInformation("Feedback data is not found with this Id ");
                    return new ServiceResponse(false, "Feedback not found");
                }
                var update = await _repository.UpdateData(id, feedback.MemberId,feedback.Comment);
                if (update== 1)
                {
                    _logger.LogInformation("Feedback updated Successfully");
                    return new ServiceResponse(true, "Feedback updated Successfully");
                }
                else
                {
                    _logger.LogInformation("Feedback can not update Successfully");
                    return new ServiceResponse(true, "Feedback can not update Successfully");
                }
            } 
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
