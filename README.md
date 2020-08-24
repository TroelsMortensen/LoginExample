# LoginExample
This is the simplest way I've been able to do a login system for a Blazor app.

I have created a custom AuthenticationStateProvider, found in /LoginFunctionality/. 
It currently has a hardcoded list of three users. The logged in user is stored in the sessionStorage, using Java Script Interop.
There are methods to log in and out.
The stored user is used whenever authentication is required, and the user is fetched from the sessionStorage. 

Because of the Blazor lifecycle (or something), server prerendering must be disabled, find this setting in /Pages/_Host.cshtml.

The App.razor must be modified.

Minimal security has been considered, this is mainly for toy examples.
