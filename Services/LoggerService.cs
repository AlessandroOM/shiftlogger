using AutoMapper;
using DTOS;
using FluentValidation.Results;
using Models;
using shiftlogger.Interface;
using shiftlogger.Validation;
using shiftlogger.Model;

namespace shiftlogger.Services
{
    public class LoggerService : IBaseService
    {
        private readonly IBaseRepository _repository;
        private readonly IMapper _mapper;
        public LoggerService(IMapper mapper, IBaseRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomResult> Delete(int Id)
        {
            var result = new CustomResult();

            try
            {
                var response = await _repository.Delete(Id);
                result.Success = true;
                result.Data = _mapper.Map<Logger, LoggerDto>(response);
                result.Messages.Add("Log deleted successfully.");
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Messages.Add($"Error deleting log: {ex.Message}");
            }

            return result;
        }


         public async Task<CustomResult> FinalizeLogger(int Id, DateTime endTime)
        {
            var result = new CustomResult();

            try
            {
                var prevData = await _repository.FindById(Id);
                if (prevData != null)
                {
                    if (endTime == null || endTime < prevData.Inicio)
                    {
                        result.Success = false;
                        result.Messages.Add("End time must be greater than start time.");
                    } else{
                        var response = await _repository.FinalizeLogger(Id, endTime);
                        result.Success = true;
                        result.Data = _mapper.Map<Logger, LoggerDto>(response);
                        result.Messages.Add("Log finalized successfully.");
                    }
                    
                } else{
                    result.Success = false;
                    result.Messages.Add("Log not found.");
                }
                   
               
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Messages.Add($"Error finalizing log: {ex.Message}");
            }

            return result;
        }


        public async Task<CustomResult> FindById(int Id)
        {
            var result = new CustomResult();
             try
            {
                var response = await _repository.FindById(Id);
                result.Success = true;
                result.Data = _mapper.Map<Logger, LoggerDto>(response);
                result.Messages.Add("Log finalized successfully.");
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Messages.Add($"Error finalizing log: {ex.Message}");
            }

            return result;
        }

        public async Task<CustomResult> GetAll()
        {
            var result = new CustomResult();

            try
            {
                var data = await _repository.GetAll();
                var dataList = _mapper.Map<List<Logger>, List<LoggerDto>>(data);
                result.Success = true;
                result.Data = dataList;
                result.Messages.Add("Logs retrieved successfully.");
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Messages.Add($"Error retrieving logs: {ex.Message}");
            }

            return result;
        }


        public async Task<CustomResult> InitLogger(LoggerInsertDto newLog)
        {
            var result = new CustomResult();

            try
            {
                var data = _mapper.Map<LoggerInsertDto, Logger>(newLog);

                LoggerValidator validator = new LoggerValidator();
                ValidationResult results = validator.Validate(data);
                if(! results.IsValid)
                {
                    transferErrors(result, results);
                    result.Success = false;
                    return result;
                }

                var response = await _repository.InitLogger(data);
                result.Success = true;
                result.Data = _mapper.Map<Logger, LoggerDto>(response);
                result.Messages.Add("Log initialized successfully.");
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Messages.Add($"Error initializing log: {ex.Message}");
            }

            return result;
        }

        private static void transferErrors(CustomResult result, ValidationResult results)
        {
            foreach (var failure in results.Errors)
            {
                result.Messages.Add($"Validation error: {failure.ErrorMessage}");
            }
        }
    }
}