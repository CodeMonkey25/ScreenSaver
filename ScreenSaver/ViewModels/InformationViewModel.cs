using ReactiveUI.Fody.Helpers;
using ScreenSaver.Game.Engines;

namespace ScreenSaver.ViewModels
{
    public class InformationViewModel : ViewModelBase
    {
        [Reactive] public Engine Engine { get; set; } = new NullEngine();

        public InformationViewModel() { }
        
        public InformationViewModel(Engine engine) : this()
        {
            Engine = engine;
        }
    }
}