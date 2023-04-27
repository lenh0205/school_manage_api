using DataInfastructure;
using DataInfastructure.Model;
using DataInfastructure.Responsitory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestApi.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private ISchoolUnitOfWork _unitOfWork;
        public SchoolController(ISchoolUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("Class/GetItems")]
        [Authorize(Roles = UserRoles.User)]
        public IActionResult GetClasses([FromBody] List<Guid> ids)
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
            try
            {
                Class c = _unitOfWork.ClassRepository.GetById(id);

                if (c == null) return NotFound();
                return Ok(c);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetClasses
        /// </summary>
        /// <returns></returns>
        [HttpGet("Class/GetAll")]
        public IActionResult GetAll(string page)
        {
            try
            {
                ResponseItems<Class> allClassAndPage = _unitOfWork.ClassRepository.GetAll(page);

                if (allClassAndPage.ClassList == null || allClassAndPage.ClassList.Count == 0) 
                    return NotFound(); // return HTTP 404 if no classes found
                return Ok(allClassAndPage); // return HTTP 200 with classes data
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // return HTTP 500 with error message
            }
        }

        /// <summary>
        /// CreateClass
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Class</returns>
        [HttpPost("Class/Create")]
        public IActionResult CreateClass([FromBody] Class c)
        {
            try
            {
                _unitOfWork.ClassRepository.Add(c);
                _unitOfWork.Save();
                return Ok(c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SchoolController>/5
        [HttpPut("Class/Update/{id}")]
        public IActionResult Put(Guid id,[FromBody] Class c)
        {
            try
            {
                _unitOfWork.ClassRepository.Update(id, c);
                _unitOfWork.Save();
                return Ok(c);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<SchoolController>/5
        [HttpDelete("Class/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                bool status = _unitOfWork.ClassRepository.Delete(id);
                _unitOfWork.Save();

                return Ok(status);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
