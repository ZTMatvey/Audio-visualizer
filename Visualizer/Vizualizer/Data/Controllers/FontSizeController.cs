using System.ComponentModel;
using System.Runtime.CompilerServices;
using Visualizer.Visual;

namespace Visualizer
{
    public class FontSizeController : INotifyPropertyChanged
    {
        private static FontSizeController Instance { get; set; }
        public static FontSizeController GetInstance()
        {
            if (Instance == null)
                Instance = new FontSizeController();
            return Instance;
        }
        private FontSizeController()
        {}
        public int ItemFontSize
        {
            get { return Metadata.SMetadata.ItemsFontSize;}
            set
            {
                if (Metadata.SMetadata.ItemsFontSize != value)
                {
                    Metadata.SMetadata.ItemsFontSize = value;
                    OnItemPropertyChanged();
                }
            }
        }
        public void OnItemPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int FontSize
        {
            get { return Metadata.SMetadata.FontSize; ; }
            set
            {
                if(Metadata.SMetadata.FontSize != value)
                {
                    Metadata.SMetadata.FontSize = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
