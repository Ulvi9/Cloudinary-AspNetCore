using System;
using Microsoft.AspNetCore.Http;

namespace Cloudinary.Dtos
{
    public class PhotoCreationDto
    {
        public PhotoCreationDto()
        {
            AddedTime=DateTime.Now;
        }
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime AddedTime { get; set; }
        public string PublicId { get; set; }
    }
}