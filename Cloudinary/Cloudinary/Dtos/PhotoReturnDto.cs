using System;

namespace Cloudinary.Dtos
{
    public class PhotoReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string Description { get; set; }
        public DateTime AddedTime { get; set; }
        public string PublicId { get; set; }
    }
}