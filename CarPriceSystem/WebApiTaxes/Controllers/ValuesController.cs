using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiTaxes.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public TaxResult Get(int id)
        {
            return new TaxResult
            {
                ID = id,
                TaxPercent = 15.75,
                Fees = 3000
            };
        }
    }
}
