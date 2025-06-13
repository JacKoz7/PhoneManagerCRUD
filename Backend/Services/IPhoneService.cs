using Backend.Models;

namespace Backend.Services
{
    /// Interface for phone management operations
    public interface IPhoneService
    {
        /// Get all phones from the collection
        List<Phone> GetAllPhones();
        
        /// Get a specific phone by ID
        Phone? GetPhoneById(int id);
        
        /// Add a new phone to the collection
        Phone AddPhone(Phone phone);

        /// Update an existing phone
        Phone? UpdatePhone(int id, Phone phone);
        
        /// Delete a phone from the collection
        bool DeletePhone(int id);
    }
}