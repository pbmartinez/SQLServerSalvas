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
    [Route("conexiones")]
    [ApiController]
    public class ConexionesController : ApiBaseController<Conexion>
    {
        public ConexionesController(ApplicationDbContext _context):base(_context)
        {

        }
    }
}
