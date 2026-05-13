using AutoMapper;
using PMS.Application.DTO.Board;
using PMS.Application.DTO.Column;
using PMS.Application.DTO.Issue;
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
                .ForMember(des => des.Boards, opt => opt.MapFrom(src => src.Boards))
                .ReverseMap();

            CreateMap<CreateProjectDto, Project>();

            CreateMap<Issue, IssueDto>()
                .ForMember(des => des.Column, opt => opt.MapFrom(src => src.Column))
                .ForMember(des => des.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
            CreateMap<CreateIssueDto, Issue>();

            CreateMap<Board, BoardDto>()
                .ForMember(des => des.Columns, opt => opt.MapFrom(src => src.Columns))
                .ReverseMap();

            CreateMap<Column, ColumnDto>()
                .ForMember(des => des.Board, opt => opt.MapFrom(src => src.Board))
                .ReverseMap();
             

        }


    }
}
