namespace BlazorAuthClientApp.wwwroot.js
{
    
    window.sessionStorage = {
        setItem: function (key, value) {
            window.sessionStorage.setItem(key, value);
        },
        getItem: function (key) {
            return window.sessionStorage.getItem(key);
        },
        removeItem: function (key) {
            window.sessionStorage.removeItem(key);
        }
    };
}
