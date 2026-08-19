using AutoMapper;
using BulletinBoard_.Application.Dtos;
using BulletinBoard_.Application.Services.Interfaces;
using CoreBoard_.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAdService _adService;

        public AdsController(IAdService adService, IMapper mapper)
        {
            _adService = adService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] AdFilterDto filter)
        {
            var ads = await _adService.GetAdsAsync(filter);

            return Ok(_mapper.Map<IEnumerable<AdDto>>(ads));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _adService.DeleteAdAsync(id);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AdUpdateDto dto)
        {
            var ad = _mapper.Map<Ad>(dto);
            ad.Id = id;
            var updatedAd = await _adService.UpdateAdAsync(ad);

            return Ok(_mapper.Map<AdDto>(updatedAd));
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] AdCreateDto dto)
        {
            var ad = _mapper.Map<Ad>(dto);
            var createdAd = await _adService.CreateAdAsync(ad);

            return Ok(_mapper.Map<AdDto>(createdAd));
        }
    }
}
