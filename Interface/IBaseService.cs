using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOS;
using shiftlogger.Model;

namespace shiftlogger.Interface
{
    public interface IBaseService
    {
          Task<CustomResult> GetAll();

    Task<CustomResult> InitLogger(LoggerInsertDto newLog);

    Task<CustomResult> FindById(int Id);

    Task<CustomResult> Delete(int Id);

    Task<CustomResult> FinalizeLogger(int Id, DateTime endTime);
    }
}