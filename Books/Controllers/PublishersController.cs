using Books.Data.Services;
using Books.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Books.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publisherService;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publisherService, ILogger<PublishersController> logger)
        {
            _publisherService = publisherService;
            _logger = logger;
        }

        [HttpPost]
        [Route("addPublisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                return Created("created successfuly ", _publisherService.AddPublisher(publisher));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            throw new Exception("Thrown from get all but why isn't it loggin the errors ????");
            try
            {
                _logger.LogInformation("Heloo Fileee!");

                var publishers = _publisherService.GetAll(sortBy, searchString, pageNumber);
                return Ok(publishers);
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("id")]
        [Route("getPublData")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publisherService.GetPublisherData(id);
            if (response != null)
                return Ok(response);
            return NotFound();
        }

        [HttpGet("id")]
        [Route("getPublById")]
        public IActionResult GetPublisherById(int id)
        {
            var publ = _publisherService.GetPublisherById(id);
            if (publ != null)
                return Ok(publ);
            return NotFound();
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult DeletePublisherById(int id)
        {
            try
            {
                _publisherService.DeletePublisherById(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}