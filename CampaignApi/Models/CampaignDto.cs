using System;
using System.ComponentModel.DataAnnotations;

namespace CampaignApi.Models
{
    public class CampaignDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
