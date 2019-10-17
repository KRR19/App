﻿using App.BussinesLogicLayer;
using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<BaseResponseModel> Post([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.Create(printingEdition);

            return report;
        }

        [HttpDelete]
        public async Task<BaseResponseModel> Delete([FromBody]PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = await _service.Delete(printingEdition.Id);

            return report;
        }
        [HttpGet]
        public async Task<PrintingEditionModel> Get(Guid Id)
        {
            PrintingEditionModel printingEdition = await _service.GetById(Id);
            
            return printingEdition;
        }

        [HttpPut]
        public BaseResponseModel Put([FromBody]PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = _service.Update(newPrintingEdition);
            
            return report;
        }

    }
}