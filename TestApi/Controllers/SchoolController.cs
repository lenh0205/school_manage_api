﻿using DataInfastructure;
using DataInfastructure.Model;
using DataInfastructure.Responsitory;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ManageSchoolApi.Realtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TestApi.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private ISchoolUnitOfWork _unitOfWork;
        private readonly IHubContext<ClassHub> _classHubContext;

        public SchoolController(ISchoolUnitOfWork unitOfWork, IHubContext<ClassHub> classHubContext)
        {
            _unitOfWork = unitOfWork;
            _classHubContext = classHubContext;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("Class/GetItems")]
        [Authorize(Roles = UserRoles.User)]
        public IActionResult GetClassWithListId([FromBody] List<Guid> ids)
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
        public IActionResult GetOneClass(Guid id)
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
        public IActionResult GetWithPagination(string page)
        {
            try
            {
                ResponseItems<Class> allClassAndPage = _unitOfWork.ClassRepository.GetWithPagination(page);

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

        [HttpPost("Class/CreateR")]
        public async Task<IActionResult> CreateClassR([FromBody] Class c)
        {
            try
            {
                _unitOfWork.ClassRepository.Add(c);
                _unitOfWork.Save();

                ResponseItems<Class> finalClassAndPage = _unitOfWork.ClassRepository.GetFinalClassAndPage();

                // Send a SignalR message to all connected clients
                await _classHubContext.Clients.All.SendAsync("ReceiveNewClass", finalClassAndPage);

                return Ok(c);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SchoolController>/5
        [HttpPut("Class/Update/{id}")]
        public IActionResult UpdateCurrentClass(Guid id, [FromBody] Class c)
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Class/Upload")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);

            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                return BadRequest("Excel file does not contain any worksheet.");
            }

            var classes = new List<Class>();
            for (var row = 2; row <= worksheet.Dimension.Rows; row++)
            {
                var name = worksheet.Cells[row, 1].Value?.ToString();
                var description = worksheet.Cells[row, 2].Value?.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    var @class = new Class
                    {
                        Id = Guid.NewGuid(),
                        Name = name,
                        Description = description,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        CreatedByUserId = Guid.Empty,
                        UpdatedByUserId = Guid.Empty,
                    };
                    classes.Add(@class);
                }
            }

            await _unitOfWork.ClassRepository.AddList(classes);
            _unitOfWork.Save();

            return Ok();
        }

        [HttpGet("Class/pdf-class-list")]
        public async Task<IActionResult> ExportPdfClassList()
        {
            // Get the class data from the database
            //var classes = await _context.Classes.ToListAsync();
            List<Class> classes = await _unitOfWork.ClassRepository.GetAll();

            // Create a new PDF document
            var document = new Document();
            var memoryStream = new MemoryStream();
            var writer = PdfWriter.GetInstance(document, memoryStream);

            // Add content to the PDF document
            document.Open();
            var table = new PdfPTable(2);
            table.AddCell("Name");
            table.AddCell("Description");
            foreach (var cls in classes)
            {
                table.AddCell(cls.Name);
                table.AddCell(cls.Description);
            }
            document.Add(table);
            document.Close();

            // Set the content type and file name for the response
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=classlist.pdf");

            // Return the PDF file as a byte array to the client
            return new FileContentResult(memoryStream.ToArray(), "application/pdf");
        }

        [HttpGet("Class/excel-class-list")]
        public async Task<IActionResult> ExportExcelClassList()
        {
            // Get the class data from the database
            var classes = await _unitOfWork.ClassRepository.GetAll();

            // Create a new Excel workbook
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Class_List");

            // Add headers to the worksheet
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Description";

            // Add data to the worksheet
            var row = 2;
            foreach (var cls in classes)
            {
                worksheet.Cells[row, 1].Value = cls.Name;
                worksheet.Cells[row, 2].Value = cls.Description;
                row++;
            }

            // Convert the Excel workbook to a byte array
            var bytes = package.GetAsByteArray();

            // Set the content type and file name for the response
            HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=classlist.xlsx");
            //HttpContext.Response.Headers["Content-Disposition"] = "attachment; filename*=UTF-8''classlist.xlsx";

            // Return the Excel file as a byte array to the client
            return new FileContentResult(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
