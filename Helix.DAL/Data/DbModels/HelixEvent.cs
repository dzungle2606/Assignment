using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HelixAssignment.DAL
{
    public class HelixEvent
    {
        /// <summary>
        ///     Unique ID of the helix
        /// </summary>
        /// <value>The identifier.</value>
        [Required(ErrorMessage = "Please input the Event ID!")]
        [JsonProperty(PropertyName = "eventId")]
        public long HelixEventId { get; set; } 
        public DateTime Timestamp { get; set; }


        /// This provides a link to the Many-to-Many linking table that links to the Products of this book
        //[Required(ErrorMessage = "Please input the product list for this specific event!")]
        [JsonProperty(PropertyName = "products")]
        public ICollection<HelixEventProduct> ProductsLink { get; set; }

        public HelixEvent()
        {
        }
    }
}
