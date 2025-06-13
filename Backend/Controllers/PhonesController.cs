using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using System.ComponentModel.DataAnnotations;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonesController : ControllerBase
    {
        private readonly IPhoneService _phoneService;

        public PhonesController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        /// HTTP GET: Retrieves all phones from the collection
        /// Test URL: http://localhost:5218/api/phones
        [HttpGet]
        public ActionResult<IEnumerable<Phone>> GetAllPhones()
        {
            return Ok(_phoneService.GetAllPhones());
        }

        /// HTTP GET: Retrieves a specific phone by ID
        /// Test URL: http://localhost:5218/api/phones/1
        [HttpGet("{id}")]
        public ActionResult<Phone> GetPhone(int id)
        {
            var phone = _phoneService.GetPhoneById(id);
            if (phone == null)
                return NotFound();

            return Ok(phone);
        }

        /// HTTP POST: Creates a new phone
        /// Test URL: http://localhost:5218/api/phones
        /// Example Body:
        /// {
        ///   "name": "IPhone 14",
        ///   "manufacturer": "Apple",
        ///   "phoneNumber": "123123123",
        ///   "price": 1099.99,
        ///   "inStock": true
        /// }
        [HttpPost]
        public ActionResult<Phone> CreatePhone(Phone phone)
        {
            // Validate the model manually to ensure our custom validations are applied
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(phone);
            
            if (!Validator.TryValidateObject(phone, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage ?? string.Empty);
                }
                return BadRequest(ModelState);
            }
            
            var newPhone = _phoneService.AddPhone(phone);
            return CreatedAtAction(nameof(GetPhone), new { id = newPhone.Id }, newPhone);
        }

        /// HTTP PUT: Updates an existing phone
        /// Test URL: http://localhost:5218/api/phones/1
        /// Example Body:
        /// {
        ///   "name": "iPhone 14 Pro",
        ///   "manufacturer": "Apple",
        ///   "phoneNumber": "987987987",
        ///   "price": 1299.99,
        ///   "inStock": false
        /// }
        [HttpPut("{id}")]
        public IActionResult UpdatePhone(int id, Phone phone)
        {
            // Validate the model manually to ensure our custom validations are applied
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(phone);
            
            if (!Validator.TryValidateObject(phone, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? string.Empty, validationResult.ErrorMessage ?? string.Empty);
                }
                return BadRequest(ModelState);
            }
            
            var updatedPhone = _phoneService.UpdatePhone(id, phone);
            if (updatedPhone == null)
                return NotFound();

            return Ok(updatedPhone);
        }

        /// HTTP DELETE: Removes a phone from the collection
        /// Test URL: http://localhost:5218/api/phones/1
        [HttpDelete("{id}")]
        public IActionResult DeletePhone(int id)
        {
            var result = _phoneService.DeletePhone(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}