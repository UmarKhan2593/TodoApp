using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Web.Infrastructure.CommonDTOs
{
    public class AddressModel
    {
        public int Id { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Street Address")]
        public string StreetNumber { get; set; }

        [Display(Name = "Suite Number")]
        public string SuitNumber { get; set; }


        [Display(Name = "State")]
        public int? StateId { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        [Display(Name = "Neighborhood")]
        public int? NeighborhoodId { get; set; }
        public bool? IsSuspended { get; set; }
    }
}
