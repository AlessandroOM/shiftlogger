using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DTOS;
using Microsoft.AspNetCore.Mvc;
using shiftlogger.Interface;

namespace shiftlogger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggerController : ControllerBase
    {
        private IBaseService _service;
        
        public LoggerController(IBaseService service)
        {
            _service = service;
        }

        private string prepareErrors(List<string> messages)
        {
            string errors = "Errors: ";
            foreach (var item in messages)
            {
                errors += item + "\n";
            }
            return errors;
        }

        [HttpGet]
        public async Task<ActionResult<List<LoggerDto>>> Get()
        {
            try
            {
                var result = await _service.GetAll();
                if (result != null){
                    if (result.Success){
                        return Ok(result.Data);
                    } else {
                        string errors = prepareErrors(result.Messages);
                        return BadRequest(errors);
                    }
                } else {
                    return BadRequest();
                }

            }
            catch (ArgumentException e) //ideal para erros de http em controllers
            {

                    ObjectResult objectResult = StatusCode(
                                    statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                    return objectResult;
            } 

        }

        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
         public async Task<ActionResult> Get(int id)
        {
          if (!ModelState.IsValid)  
          {
             
              return BadRequest(ModelState);// devolve codigo 400
          }
          try
          {
              var result = await _service.FindById(id);
              if (result != null){
                if (result.Success){
                    return Ok(result.Data);
                } else {
                    string errors = prepareErrors(result.Messages);
                    return BadRequest(errors);
                }
                } else {
                    return BadRequest();
                }
              
          }
          catch (ArgumentException e) //ideal para erros de http em controllers
          {

                ObjectResult objectResult = StatusCode(
                                  statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                return objectResult;
          }
        }

         [HttpPost]
        public async Task<ActionResult> Post ([FromBody] LoggerInsertDto dataIn)
        {
          if (!ModelState.IsValid)  
          {
             
              return BadRequest(ModelState);
          }
          try
          {
              var result = await _service.InitLogger(dataIn);
              if (result != null){
                if (result.Success){
                    return Ok(result.Data);
                } else {
                   string errors = prepareErrors(result.Messages);
                   return BadRequest(errors);
                }
                } else {
                    return BadRequest();
                }

          }
          catch (ArgumentException e) //ideal para erros de http em controllers
          {

                ObjectResult objectResult = StatusCode(
                                  statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                return objectResult;
          } 
        }

        [HttpPut]
         [Route("{id}/{endTime}")]
        public async Task<ActionResult> Put (int id, DateTime endTime)
        {
          if (!ModelState.IsValid)  
          {
             
              return BadRequest(ModelState);
          }
          try
          {
              var result = await _service.FinalizeLogger(id, endTime);
              if (result != null){
                 if (result.Success){
                    return Ok(result.Data);
                 } else {
                    string errors = prepareErrors(result.Messages);
                    return BadRequest(errors);
                 }
                 } else {
                    return BadRequest();
                 }

          }
          catch (ArgumentException e) //ideal para erros de http em controllers
          {

                ObjectResult objectResult = StatusCode(
                                  statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                return objectResult;
          } 
        }

        [HttpDelete]
        [Route("{id}")]
         public async Task<ActionResult> Delete(int id)
        {
          if (!ModelState.IsValid)  
          {
             
              return BadRequest(ModelState);// devolve codigo 400
          }
          try
          {
              var result = await _service.Delete(id);
              if (result != null){
                 if (result.Success){
                    return Ok(result.Data);
                 } else {
                    string errors = prepareErrors(result.Messages);
                    return BadRequest(errors);
                 }
                 } else {
                    return BadRequest();
                 }
          }
          catch (ArgumentException e) //ideal para erros de http em controllers
          {

                ObjectResult objectResult = StatusCode(
                                  statusCode: (int)HttpStatusCode.InternalServerError, value: e.Message);
                return objectResult;
          }
        }
 
    }
}