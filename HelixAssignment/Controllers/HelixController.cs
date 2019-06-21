using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using System;
using HelixAssignment.ViewModel;
using Microsoft.AspNetCore.Authorization;
using HelixAssignment.BAL;
using HelixAssignment.DAL;
using Microsoft.AspNetCore.Http;

namespace HelixAssignment.Controllers
{
    /// <summary>
    ///     API Controller Class for the assignment
    /// </summary>
    [Route("api/helix")]
    [ApiController]
    public class HelixController : ControllerBase
    {
        private readonly IHelixEventService _eventService;

        public HelixController(IHelixEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/helix/Test
        [HttpGet("Test")]
        public string Test()
        {
            return "Welcome the the Test Page!!!";
        }

        // GET: api/helix/GetEvents
        [HttpGet("GetProducts")]
        public async Task<ActionResult<ICollection<HelixEvent>>> GetProducts()
        {
            ICollection<HelixEvent> list = new List<HelixEvent>();
            try
            {
                list = await _eventService.GetAllEvents();
            }
            catch (Exception ex)
            {
                /** In real application, 
                * Encapsulate the details in the layer-specific log files for further troubleshooting 
                * and wrap it in user-friendly manner
               **/
                throw ex;
            }

            return Ok(list);
        }

        // GET: api/helix/GetEvents
        [HttpGet("GetEventById/{id}")]
        public async Task<ActionResult<HelixEvent>> GetEventById(long id)
        {
            HelixEvent helixEventItem = null;

            try
            {
                helixEventItem = await _eventService.GetEventById(id);
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                **/
                throw ex;
            }

            if (helixEventItem == null)
            {
                return NotFound();
            }

            return Ok(helixEventItem);
        }

        // PUT: api/helix/PutProducts
        [HttpPut("PutProducts")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutProductsToEvent([FromBody]HelixEventViewModel eventRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Update product against DB  
                    await _eventService.Update(eventRequest);
                }
                catch (Exception ex)
                {
                    /** In real application, 
                     * Encapsulate the details in the layer-specific log files for further troubleshooting 
                     * and wrap it in user-friendly manner
                    **/
                    throw ex;
                    /*
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    { 
                        Content = new StringContent(string.Format("Error: {0} {1} ", ex.Message, ex.StackTrace.ToString()) ),
                        ReasonPhrase = ex.Message + ex.StackTrace.ToString()
                    };
                    return resp;*/
                }

                // Do something else here in reality concurrently without waiting for products updates completed
                //await updateTask;

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }       
    }
}