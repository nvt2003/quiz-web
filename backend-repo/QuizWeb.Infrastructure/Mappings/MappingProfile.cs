using AutoMapper;
using QuizWeb.Application.DTOs.Participants;
using QuizWeb.Application.DTOs.Quizzes;
using QuizWeb.Application.DTOs.Tests;
using QuizWeb.Application.DTOs.Topics;
using QuizWeb.Infrastructure.Persistence.Models;

namespace QuizWeb.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Topic, TopicDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateTopicRequest, Topic>();
            CreateMap<UpdateTopicRequest, Topic>();
            CreateMap<Quiz, QuizVM>();
            CreateMap<Topic, TopicVM>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Quizzes, opt => opt.MapFrom(src => src.Quizzes));
            CreateMap<Participant, ParticipantVM>(); 
            CreateMap<Test, TestVM>()
            .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Topic.Title))
            .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants));
        }
    }

}
