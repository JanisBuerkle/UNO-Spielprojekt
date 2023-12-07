﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UNO_Spielprojekt.Localization;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.Setting
{
    public partial class SettingsView : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel), typeof(SettingsViewModel), typeof(SettingsView),
            new PropertyMetadata(default(SettingsViewModel)));

        public SettingsViewModel ViewModel
        {
            get => (SettingsViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public static Language language = new Language();
        private readonly MainWindowView _mainWindow;

        public SettingsView()
        {
            InitializeComponent();
            SetLanguage();
        }

        private void SetLanguage()
        {
            if (language.LangString == null)
            {
                CultureInfo culture = System.Threading.Thread.CurrentThread.CurrentUICulture;
                language.LangCulture = culture;
            }
            else
            {
                CultureInfo culture = new CultureInfo(language.LangString);
                language.LangCulture = culture;
            }


            // Header.Text = LocalizationManager.GetLocalizedString("Header");
            // Console.WriteLine($@"Header text: {Header.Text}");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox.SelectedItem is Language selectedLanguage)
            {
                language.LangString = selectedLanguage.CultureName;
                SetLanguage();
                LanguageChangeSnackBar.Title = "Erfolg:";
                LanguageChangeSnackBar.Message = "Du hast erfolgreich die Sprache geändert!";
                LanguageChangeSnackBar.Show();
            }
        }

        private void ScreenModus_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ScreenModus.SelectedItem is WindowMode selectedMode)
            {
                if (selectedMode == WindowMode.FullScreen)
                {
                    Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    Application.Current.MainWindow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1f1f1f"));
                }
                else if (selectedMode == WindowMode.Windowed)
                {
                    Application.Current.MainWindow.WindowStyle = WindowStyle.None;
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                    Application.Current.MainWindow.Background = Brushes.Transparent;
                }
            }
        }
    }
}