using Core.Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultBlazor.Api.Controllers
{
    [Route("salvas")]
    [ApiController]
    public class SalvasController : ApiBaseController<Salvas>
    {
        public SalvasController(ApplicationDbContext context):base(context)
        {

        }
    }
}
