using CampaignApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CampaignApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        public static List<CampaignDto> campaignList = new List<CampaignDto>() {
        new CampaignDto
            {
                Id = 1,
                Name = "Campaign5",
                IsActive = true,
                StartDate = new DateTime(2021, 09, 29),
                EndDate = new DateTime(2021, 12, 31),
            },
        new CampaignDto
            {
                Id = 2,
                Name = "Campaign4",
                IsActive = true,
                StartDate = new DateTime(2021, 10, 29),
                EndDate = new DateTime(2021, 11, 29),
            },
        new CampaignDto
            {
                Id = 3,
                Name = "Campaign2",
                IsActive = false,
                StartDate = new DateTime(2021, 08, 31),
                EndDate = new DateTime(2021, 09, 29),
            },
        new CampaignDto
            {
                Id = 4,
                Name = "Campaign2",
                IsActive = true,
                StartDate = new DateTime(2021, 01, 29),
                EndDate = new DateTime(2021, 12, 29),
            },
        new CampaignDto
            {
                Id = 5,
                Name = "Campaign2",
                IsActive = true,
                StartDate = new DateTime(2021, 01, 29),
                EndDate = new DateTime(2021, 12, 29),
            }
        };

        [Route("passive")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult PassiveCampaign([FromHeader(Name = "x-requestid")] int campaignId)
        {
            var campaign = campaignList.FirstOrDefault(campaign => campaign.Id == campaignId);

            if (campaign is not null)
            {
                campaign.IsActive = false;

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("{campaignId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(CampaignDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<CampaignDto> GetCampaignById(int campaignId)
        {
            var campaign = campaignList.FirstOrDefault(campaign => campaign.Id == campaignId);

            if (campaign is not null)
            {
                return Ok(campaign);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CampaignDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<IEnumerable<CampaignDto>> GetCampaignsAsync()
        {
            if(campaignList.Count > 0)
            {
                return Ok(campaignList);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult<CampaignDto> CreateCampaign([FromBody] CampaignDto campaign)
        {
            try
            {
                var maxId = campaignList.Max(campaign => campaign.Id);
                var newCampaign = new CampaignDto
                {
                    Id = maxId + 1,
                    Name = campaign.Name,
                    IsActive = true,
                    StartDate = campaign.StartDate,
                    EndDate = campaign.EndDate
                };

                campaignList.Add(newCampaign);

                return Ok(newCampaign);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<CampaignDto> DeleteCampaign(int campaignId)
        {
            var campaign = campaignList.FirstOrDefault(campaign => campaign.Id == campaignId);

            if (campaign is not null)
            {
                campaignList.Remove(campaign);

                return Ok(campaign);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("filter")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<CampaignDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<IEnumerable<CampaignDto>> GetCampaignWithFilter(FilterDto filters)
        {
            var resultList = campaignList.Where(campaign => (filters.Name == null || filters.Name.Count == 0 || filters.Name.Contains(campaign.Name))
                                                && (filters.StartDate == null || campaign.StartDate == filters.StartDate)
                                                && (filters.EndDate == null || campaign.EndDate == filters.EndDate)
                                                && filters.IsActive == campaign.IsActive)
                                                .ToList();

            if (resultList.Count > 0)
            {
                return Ok(resultList);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
