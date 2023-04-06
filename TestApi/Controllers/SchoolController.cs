using DataInfastructure;
using DataInfastructure.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private ISchoolUnitOfWork _unitOfWork;
        public SchoolController(ISchoolUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GetClasses
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("Class/GetItems")]
        public IActionResult GetClasses([FromBody]List<Guid> ids)
        {
            List<Class> classes = _unitOfWork.ClassRepository.GetByIds(ids);
            return Ok(classes);
        }
         
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Class/GetItem/{id}")]
        public IActionResult Get(Guid id)
        {
            Class c = _unitOfWork.ClassRepository.GetById(id);
            return Ok(c);
        }

        /// <summary>
        /// CreateClass
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Class</returns>
        [HttpPost("Class/Create")]
        public IActionResult CreateClass([FromBody] Class c)
        {
            _unitOfWork.ClassRepository.Add(c);
            _unitOfWork.Save();
            return Ok(c);
        }

        // PUT api/<SchoolController>/5
        [HttpPut("Class/Update/{id}")]
        public IActionResult Put([FromBody] Class c)
        {
            Class updatedClass = _unitOfWork.ClassRepository.Update(c);w
            return Ok(updatedClass);
        }

        // DELETE api/<SchoolController>/5
        [HttpDelete("Class/Delelte/{id}")]
        public IActionResult Delete(Guid id)
        {
            bool status = _unitOfWork.ClassRepository.Delete(id);
            return Ok(status);
        }
    }
}
