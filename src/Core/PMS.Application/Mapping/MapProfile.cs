using AutoMapper;
using PMS.Application.DTO.Project;
using PMS.Domain.Entities;

namespace PMS.Application.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(des => des.LastUpdatedDate, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(des => des.Manager, opt => opt.MapFrom(src => src.Manager))
                .ReverseMap();

            CreateMap<CreateProjectDto, Project>();



        }


    }
}
