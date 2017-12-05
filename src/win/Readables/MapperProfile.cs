using AutoMapper;

namespace Readables
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UI.Model.UtilityGroup, ViewModel.Outline.OutlineGroup>()
                .ReverseMap();
            CreateMap<UI.Model.UtilityItemLibrary, ViewModel.Outline.OutlineLibraryItem>()
                .ReverseMap();
            CreateMap<UI.Model.UtilityItemSubject, ViewModel.Outline.OutlineSubject>()
                .ReverseMap();
        }
    }
}
