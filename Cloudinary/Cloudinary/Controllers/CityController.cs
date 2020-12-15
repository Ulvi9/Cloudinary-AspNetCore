using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Cloudinary.DAL;
using Cloudinary.Dtos;
using Cloudinary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cloudinary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CityController(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            // var cities = _context.Cities
            //     .Include(c => c.Photos)
            //     .Select(c=>new CityForListDto
            //     {
            //         Description = c.Description,
            //         Name = c.Name,
            //         PhotoUrl = c.Photos.FirstOrDefault().Url
            //     }).ToList();
            var cities = _context.Cities
                .Include(c => c.Photos).ToList();
            var mapperCities = _mapper.Map<IEnumerable<CityReturnDto>>(cities);
            return Ok(mapperCities);
        }
        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _context.Cities
                .Include(c => c.Photos).FirstOrDefault(c=>c.Id==id);
            if (city == null) return BadRequest("city could not found");
            var mapperCity = _mapper.Map<CityReturnDto>(city);
            return Ok(mapperCity);
        }
        [HttpPost]
        public IActionResult AddCity([FromBody] City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return Ok(city);
        }
    }
}