using Backend.Models;

namespace Backend.Services
{

    /// Implementation of phone management operations
    public class PhoneService : IPhoneService
    {
        // In-memory collection of phones
        private static List<Phone> _phones = new List<Phone>
        {
            new Phone { 
                Id = 1, 
                Name = "iPhone 13", 
                Manufacturer = "Apple", 
                PhoneNumber = "123456789", 
                Price = 1500, 
                InStock = true 
            },
            new Phone { 
                Id = 2, 
                Name = "Galaxy S21", 
                Manufacturer = "Samsung", 
                PhoneNumber = "987654321", 
                Price = 1700, 
                InStock = true 
            },
            new Phone { 
                Id = 3, 
                Name = "Pixel 6", 
                Manufacturer = "Google", 
                PhoneNumber = "456789123", 
                Price = 990, 
                InStock = false 
            }
        };

        /// Get all phones from the collection
        public List<Phone> GetAllPhones()
        {
            return _phones;
        }

        /// Get a specific phone by ID
        public Phone? GetPhoneById(int id)
        {
            return _phones.FirstOrDefault(p => p.Id == id);
        }

        /// Add a new phone to the collection
        public Phone AddPhone(Phone phone)
        {
            // Generate a new ID (in a real app, this would be handled by the database)
            phone.Id = _phones.Count > 0 ? _phones.Max(p => p.Id) + 1 : 1;
            _phones.Add(phone);
            return phone;
        }

        /// Update an existing phone
        public Phone? UpdatePhone(int id, Phone phone)
        {
            var existingPhone = _phones.FirstOrDefault(p => p.Id == id);
            if (existingPhone == null)
                return null;

            existingPhone.Name = phone.Name;
            existingPhone.Manufacturer = phone.Manufacturer;
            existingPhone.PhoneNumber = phone.PhoneNumber;
            existingPhone.Price = phone.Price;
            existingPhone.InStock = phone.InStock;

            return existingPhone;
        }

        /// Delete a phone from the collection
        public bool DeletePhone(int id)
        {
            var phone = _phones.FirstOrDefault(p => p.Id == id);
            if (phone == null)
                return false;

            _phones.Remove(phone);
            return true;
        }
    }
}