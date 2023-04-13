// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Authentication.WebAssembly.Msal.Models;

namespace Microsoft.Authentication.WebAssembly.Msal.Extensions.Models;

/// <summary>
/// Authentication provider options for the msal.js authentication provider.
/// </summary>
public class ChromeMsalProviderOptions : MsalProviderOptions 
{
    /// <summary>
    /// Gets or sets the flag that triggers authentication with the Chrome identity authentication flow.
    /// </summary>
    /// <value>Defaults to <c>false</c></value>
    public bool UseChromeWebAuthFlow { get; set; }
}