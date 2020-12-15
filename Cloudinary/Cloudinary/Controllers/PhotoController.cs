using System.Linq;
using AutoMapper;
using Cloudinary.DAL;
using Cloudinary.Dtos;
using Cloudinary.Helpers;
using Cloudinary.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cloudinary.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class PhotoController : ControllerBase
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private IOptions<CloudinarySettings> _cloudinaryConfig;
            private CloudinaryDotNet.Cloudinary _cloudinary;
            public PhotoController(DataContext context,
                IMapper mapper,
                IOptions<CloudinarySettings> cloudinaryConfig)
            {
                _context = context;
                _mapper = mapper;
                _cloudinaryConfig = cloudinaryConfig;
                Account account=new Account(_cloudinaryConfig.Value.CloudName,
                    _cloudinaryConfig.Value.ApiKey,
                    _cloudinaryConfig.Value.ApiSecret);
                _cloudinary=new CloudinaryDotNet.Cloudinary(account);
            }

            [HttpPost("{cityId}")]
            public IActionResult AddPhoto(int cityId,[FromForm] PhotoCreationDto photoCreationDto)
            {
                var city = _context.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null) return BadRequest("Could not find the city");
                
                var file = photoCreationDto.File;
                var uploadResult=new ImageUploadResult();
                if (file.Length>0)
                {
                    using (var stream=file.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams
                        {

                            File = new FileDescription(file.Name, stream)
                        };
                        uploadResult = _cloudinary.Upload(uploadParams);
                    } 
                }

                photoCreationDto.Url = uploadResult.Url.ToString();
                photoCreationDto.PublicId = uploadResult.PublicId;
                
                var photo = _mapper.Map<Photo>(photoCreationDto);
                photo.Description = photoCreationDto.Description;
                photo.City = city;
                if (!city.Photos.Any(p=>p.IsMain))
                {
                    photo.IsMain = true;
                }

                city.Photos.Add(photo);
                 _context.SaveChanges();
                // var photoReturn = _mapper.Map<PhotoReturnDto>(photo);
                return Ok("lorem");
            }
           [HttpGet("{cityId}")]
            public IActionResult GetPhotos(int cityId)
            {
                var photo = _context.Photos
                    .Where(c => c.CityId == cityId)
                    .Select(x => x.Url);
                return Ok(photo);

            }
        }
    
}