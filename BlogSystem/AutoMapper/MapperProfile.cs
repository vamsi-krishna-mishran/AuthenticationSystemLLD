using AutoMapper;
using BlogSystem.DTOs;
using BlogSystem.Models;

namespace BlogSystem.AutoMapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<BlogDTO, Blog>().ReverseMap();

            CreateMap<RatingDTO,Rating>().ReverseMap();
        }
    }
}
