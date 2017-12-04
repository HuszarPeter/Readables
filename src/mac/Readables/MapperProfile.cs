using System;
using AutoMapper;
using Readables.ViewControllers.Outline.Model;

namespace Readables
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UI.Model.ViewMode, nint>().ConvertUsing(input => (input == UI.Model.ViewMode.List) ? 0 : 1);
            CreateMap<nint, UI.Model.ViewMode>().ConvertUsing(input => (input == 0) ? UI.Model.ViewMode.List : UI.Model.ViewMode.Cover);

            CreateMap<UI.Model.UtilityGroup, OutlineGroup>()
            .ReverseMap();
            CreateMap<UI.Model.UtilityItemSubject, OutlineItemSubject>()
            .ReverseMap();
            CreateMap<UI.Model.UtilityItemLibrary, OutlineItemLibrary>()
            .ReverseMap();
        }
    }
}
