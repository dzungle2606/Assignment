using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HelixAssignment.DAL
{
    public class HelixEventProduct
    {
        public HelixEventProduct()
        {
        }

        public long EventId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Sale_amount { get; set; }

        //-----------------------------
        //Relationships
        // This is the link to the Event side of the relationship
        [IgnoreDataMember]
        public HelixEvent HelixEvent { get; set; }

        // This is the link to the Product side of the relationship
        [IgnoreDataMember]
        public Product Product { get; set; }

        /**************************************************
        The BookAuthor class is the Many-to-Many linking table between the Books and Authors tables
        The Primary Key is made up of the two keys of the Event and Product Tables    
        ***********************************************/

    }
}
