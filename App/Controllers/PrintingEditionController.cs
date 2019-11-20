﻿using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        public async Task<PrintingEditionModel> Get(Guid Id)
        {
            PrintingEditionModel printingEdition = await _service.GetById(Id);

            return printingEdition;
        }

        [HttpGet("GetAll")]
        public List<PrintingEdition> GetAll()
        {
            List<PrintingEdition> printingEdition = _service.GetAll();

            return printingEdition;
        }

        [HttpPost("Post")]
        [Authorize]
        public async Task<BaseResponseModel> Post([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.Create(printingEdition);

            return report;
        }
        
        [HttpPut("Update")]
        [Authorize]
        public async Task<BaseResponseModel> Update([FromBody]PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = await _service.Update(newPrintingEdition);

            return report;
        }

        [HttpPost("Delete")]
        [Authorize]
        public async Task<BaseResponseModel> Delete([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.Delete(printingEdition.Id);

            return report;
        }
    }
}