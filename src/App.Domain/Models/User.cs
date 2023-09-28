using System;
using App.Domain.Models.Core;

namespace App.Domain.Models
{
    /// <summary>
    /// Represents a User entity.
    /// Inherits from EntityBase.
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        /// Overloaded constructor that initializes the User entity with its properties.
        /// </summary>
        public User(string firstname, string lastname, string email, string phoneNumber)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public User() { }
        
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public virtual string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public virtual string Lastname { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        public virtual string PhoneNumber { get; set; }
    }
}
