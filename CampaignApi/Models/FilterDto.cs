using System;
using System.Collections.Generic;

namespace CampaignApi.Models
{
    public class FilterDto
    {
        public List<string> Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
