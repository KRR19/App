using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models;
using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _service;

        public PrintingEditionController(IPrintingEditionService service)
        {
            _service = service;
        }
        [HttpGet("Get/{Id}")]
        public async Task<PrintingEditionModel> GetAsync(Guid Id)
        {
            PrintingEditionModel printingEdition = await _service.GetByIdAsync(Id);

            return printingEdition;
        }

        [HttpGet("GetAll")]
        public async Task<List<PrintingEditionModel>> GetAllAsync()
        {
            List<PrintingEditionModel> printingEdition = await _service.GetAllAsync();

            return printingEdition;
        }

        [HttpPost("Filter")]
        public async Task<List<PrintingEditionModel>> FilterAsync([FromBody] FilterModel filterModel)
        {
            List<PrintingEditionModel> PrintingEditionModels = await _service.FilterAsync(filterModel);
            return PrintingEditionModels;
        }

        [HttpPost("Post")]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponseModel> PostAsync([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.CreateAsync(printingEdition);

            return report;
        }

        [HttpPost("Update")]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponseModel> UpdateAsync([FromBody]PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = await _service.UpdateAsync(newPrintingEdition);

            return report;
        }

        [HttpPost("Delete")]
        [Authorize(Roles = "ADMIN")]
        public async Task<BaseResponseModel> DeleteAsync([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.DeleteAsync(printingEdition.Id);

            return report;
        }

    }
}