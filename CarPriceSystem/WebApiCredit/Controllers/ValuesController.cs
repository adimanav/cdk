using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WebApiCredit.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/5
        [HttpGet("{ssn}")]
        public CreditResult Get(string ssn)
        {
            return new CreditResult
            {
                SSN = ssn,
                Rating = 671.6
            };
        }
    }
}
