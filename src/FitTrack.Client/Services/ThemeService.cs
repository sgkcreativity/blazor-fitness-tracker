using Microsoft.JSInterop;

namespace FitTrack.Client.Services;

public class ThemeService
{
    private readonly IJSRuntime _js;
    public ThemeService(IJSRuntime js) => _js = js;

    public Task<string> GetAsync() => _js.InvokeAsync<string>("fittrack.getTheme").AsTask();
    public Task SetAsync(string theme) => _js.InvokeVoidAsync("fittrack.setTheme", theme).AsTask();

    public async Task ToggleAsync()
    {
        var cur = await GetAsync();
        await SetAsync(cur == "dark" ? "light" : "dark");
    }
}