using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelixAssignment.ViewModel
{
    public class HelixEventViewModel
    {
        /// <summary>
        ///     Unique ID of the helix
        /// </summary>
        /// <value>The identifier.</value>
        [Required(ErrorMessage = "Please input the Event ID!")]
        public long Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Please input the product list for this specific event!")]
        public virtual ICollection<ProductViewModel> Products { get; set; }

        public HelixEventViewModel()
        {
        }
    }

    public class ProductViewModel
    {
        /// <summary>
        ///     Unique ID of the entity
        /// </summary>
        /// <value>The identifier.</value>
        [Required(ErrorMessage = "Please input the product ID!")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please input the product name!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please input the quantity!")]
        [Range(0, 999)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please input the sales amount!")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "You didn't enter a proper sales amount!")]
        public decimal? Sale_amount { get; set; }
    }
}
