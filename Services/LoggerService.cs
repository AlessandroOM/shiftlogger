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
                    prevData.Fim = endTime;
                     LoggerValidator validator = new LoggerValidator();
                     ValidationResult results = validator.Validate(prevData);
                     if(! results.IsValid)
                        {
                            transferErrors(result, results);
                            result.Success = false;
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

        public async Task<CustomResult> GetAll(int pagenumber, int quantity)
        {
            var result = new CustomResult();

            try
            {
                IEnumerable<Logger> data = await _repository.GetAll(pagenumber,quantity);
                List<Logger> loggerList = data.ToList();
                var dataList = _mapper.Map<List<Logger>, List<LoggerDto>>(loggerList);
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

                var LoggerExists = (await _repository.FindByActivity(newLog.Atividade) != null);
                if (!LoggerExists)    
                {
                    var response = await _repository.InitLogger(data);
                    result.Success = true;
                    result.Data = _mapper.Map<Logger, LoggerDto>(response);
                    result.Messages.Add("Log initialized successfully.");
                } else 
                {
                    result.Success = false;
                    result.Messages.Add("Log already exists.");
                }
                
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