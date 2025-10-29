using Microsoft.JSInterop;
using System.Globalization;

namespace DevBench.Client.Services
{
    public class SettingsService
    {
        private readonly IJSRuntime _jsRuntime;
        public event Action? SettingsChanged;

        public string SelectedCulture { get; private set; } = CultureInfo.CurrentCulture.Name;
        public bool IsDarkMode { get; private set; } = true;

        public SettingsService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<string> GetSavedCulture()
        {
            return await _jsRuntime.InvokeAsync<string>(Constants.LocalStorageGetItem, Constants.LocalStorageKeys.Culture);
        }

        public async Task<string> GetBrowserCulture()
        {
            return await _jsRuntime.InvokeAsync<string>(Constants.JSFunctions.GetLanguage);
        }

        public async Task SetCulture(string culture)
        {
            SelectedCulture = culture;
            var cultureInfo = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            await SavePreference(Constants.LocalStorageKeys.Culture, SelectedCulture);
            NotifySettingsChanged();
        }

        public async Task<string> GetSavedDarkMode()
        {
            return await _jsRuntime.InvokeAsync<string>(Constants.LocalStorageGetItem, Constants.LocalStorageKeys.DarkMode);
        }

        public async Task SetDarkMode(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
            await SavePreference(Constants.LocalStorageKeys.DarkMode, IsDarkMode.ToString().ToLower());
            NotifySettingsChanged();
        }

        public async Task ToggleDarkMode()
        {
            IsDarkMode = !IsDarkMode;
            await SetDarkMode(IsDarkMode);
        }

        private async Task SavePreference(string key, string value)
        {
            await _jsRuntime.InvokeVoidAsync(Constants.LocalStorageSetItem, key, value);
        }

        private void NotifySettingsChanged()
        {
            SettingsChanged?.Invoke();
        }
    }
}
