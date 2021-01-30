using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Visualizer.Data.Metadata;

namespace Visualizer.Visual
{
    public static class ApplicationController
    {
        private static bool Serialize { get; set; } = true;
        public static void Setup()
        {
            Application.Current.DispatcherUnhandledException += (s, e) =>
            {
                MessageBox.Show("Все плохо! В связи с неожиданным поведением приложения будет выполнен экстренный выход.\n" +
                    "Также будет удален файл метаданных\n" +
                    "Прошу сообщить об этой ошибке автору: Telegram @matveyztm\n" +
                    "С прикреплением скриншота данной ошибки.\n" +
                    $"{e.Exception.Message}");
                ExitAndDeleteMetadataFile();
            };
            Metadata.GetInstance();
            MetadataSerializator.DesirializeOrSetDefault();
            Metadata.SMetadata.SetupAfterDesirialization();
            ThemesController.SetThemeAndUpdateThemeInMetadataAndUpdateStyles(Metadata.SMetadata.Theme);
        }
        private static void ExitAndDeleteMetadataFile()
        {
            MetadataSerializator.DeleteMetadataFile();
            Serialize = false;
            Application.Current.MainWindow.Close();
        }
        public static void Exit()
        {
            if (Serialize)
                MetadataSerializator.Serialize();
        }
    }
}
