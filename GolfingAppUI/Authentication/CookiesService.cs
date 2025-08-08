using Microsoft.JSInterop;

namespace GolfingAppUI.Authentication;
/// <summary>
/// Handles cookie management on the client side: SetCookie, GetCookie and DeleteCookie
/// </summary>
public class CookieService{

    private readonly IJSRuntime _jSRuntime; 
    public CookieService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    public async Task SetCookieAsync(string name, string value, int expireDays)
    {
       
        await _jSRuntime.InvokeVoidAsync("setCookie", name, value, expireDays);
    }

    public async Task<string> GetCookieAsync(string name)
    {
        return await _jSRuntime.InvokeAsync<string>("getCookie", name);
    }

    public async Task DeleteCookieAsync(string name)
    {
        await _jSRuntime.InvokeVoidAsync("deleteCookie", name);
    }
   
}