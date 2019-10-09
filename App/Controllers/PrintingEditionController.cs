using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using App.BussinesLogicLayer.Services.Interfaces;
using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer;

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

    }
}