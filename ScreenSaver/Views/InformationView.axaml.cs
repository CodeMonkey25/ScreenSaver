using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ScreenSaver.ViewModels;

namespace ScreenSaver.Views
{
    public partial class InformationView : ReactiveWindow<InformationViewModel>
    {
        public InformationView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}