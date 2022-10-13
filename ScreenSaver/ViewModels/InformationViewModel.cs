using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game;

namespace ScreenSaver.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        [Reactive] public Engine Engine { get; set; }

        public InformationViewModel(Engine engine)
        {
            Engine = engine;
        }
    }
}