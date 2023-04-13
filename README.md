# Blazor.BrowserExtension.Authentication

A package to add MSAL authentication to Blazor browser extensions built with [mingyaulee/Blazor.BrowserExtension](https://github.com/mingyaulee/Blazor.BrowserExtension).

Written to address https://github.com/dotnet/aspnetcore/issues/46203 and https://github.com/mingyaulee/Blazor.BrowserExtension/issues/52.

> **Warning**<br>
> This package is in pre-release stage. Expect breaking API changes and key features to be missing.

---

## How to use

### Msal.ChromeExample

1. Build and follow the instructions at [mingyaulee/Blazor.BrowserExtension](https://github.com/mingyaulee/Blazor.BrowserExtension) to sideload the extension into your preferred browser.

2. Note of the extension id after the URL scheme in the address bar when the extension is loaded, or listed under the extension in extension settings.

3. [Create an Azure AD B2C tenant and application](https://learn.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-tenant) (Azure AD should also work, I have not tested it with this package).

    Choose "Single Page Application" application type and ensure the redirect URI is of the form `https://[extension id].chromiumapp.org/authentication/login-callback`, where `extension id` is your extension id from step 2.

4. (If Azure AD B2C) [Create a user flow](https://learn.microsoft.com/en-us/azure/active-directory-b2c/tutorial-create-user-flows).

5. Create an `appsettings.json` in `wwwroot` and paste the following contents (contents will vary if Azure AD, see [MSAL.NET documentation](https://github.com/Azure-Samples/ms-identity-blazor-wasm/blob/main/WebApp-OIDC/MyOrg/README.md)):
```
{
  "AzureAd": {
    "Authority": "https://[tenant name].b2clogin.com/[resource name]/[user flow name]",
    "ClientId": "[application client id]",
    "RedirectUri": "https://[extension id].chromiumapp.org/authentication/login-callback",
    "ValidateAuthority": false
  }
}
```

6. Build again and refresh the extension page.

build and follow the instructions at [mingyaulee/Blazor.BrowserExtension](https://github.com/mingyaulee/Blazor.BrowserExtension) to sideload the extension into your preferred browser.

### Notes

- `RemoteAuthenticationUserOptions.AuthenticationType` must be set
- `ChromeMsalProviderOptions.UseChromeWebAuthFlow` must be set to `true`. It does not default to `true` just because you are using the derived class.
- `MsalProviderOptions.LoginMode` must be set to `redirect` (this is likely to change in future versions)