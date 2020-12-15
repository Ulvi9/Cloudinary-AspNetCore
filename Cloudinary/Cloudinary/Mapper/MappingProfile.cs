using System.Linq;
using AutoMapper;
using Cloudinary.Dtos;
using Cloudinary.Models;

namespace Cloudinary.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityReturnDto>()
                .ForMember(dest => dest.PhotoUrl,
                    opt => 
                        opt.MapFrom(src => src.Photos
                            .FirstOrDefault(c => c.IsMain).Url));
            CreateMap<Photo, PhotoCreationDto>().ReverseMap();

        }
    }
}