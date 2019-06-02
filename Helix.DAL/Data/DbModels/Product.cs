using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelixAssignment.DAL
{
    public class Product
    {
        /// <summary>
        ///     Unique ID of the entity
        /// </summary>
        /// <value>The identifier.</value>
        [Required(ErrorMessage = "Please input the product ID!")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Please input the product name!")]
        public string Name { get; set; }

        /// This points to, via the linking table, all the events the product has been sold 
        public ICollection<HelixEventProduct> HelixEventsLink { get; set; }
    }
}