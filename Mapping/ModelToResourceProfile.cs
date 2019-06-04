using AutoMapper;
using MMM_Bracket.API.Domain.Models;
using MMM_Bracket.API.Resources;

namespace MMM_Bracket.API.Mapping
{
  public class ModelToResourceProfile : Profile
  {
    public ModelToResourceProfile()
    {
      CreateMap<Category, CategoryResource>();
      CreateMap<Animal, AnimalResource>();
      CreateMap<Participant, ParticipantResource>();
      CreateMap<User, UserResource>();
      CreateMap<Battle, BattleResource>();
    }
  }
}