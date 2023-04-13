using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Authentication.WebAssembly.Msal;
using Microsoft.Authentication.WebAssembly.Msal.Extensions.Models;
using Microsoft.Authentication.WebAssembly.Msal.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Authentication.WebAssembly.Msal.Extensions;

/// <summary>
/// Contains extension methods to add authentication to Blazor WebAssembly applications using
/// Azure Active Directory or Azure Active Directory B2C.
/// </summary>
public static class MsalWebAssemblyServiceCollectionExtensions
{
    /// <summary>
    /// Adds authentication using msal.js to Blazor applications.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{ChromeMsalProviderOptions}"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IRemoteAuthenticationBuilder<RemoteAuthenticationState, RemoteUserAccount> AddMsalAuthentication(this IServiceCollection services, Action<RemoteAuthenticationOptions<ChromeMsalProviderOptions>> configure)
    {
        return AddMsalAuthentication<RemoteAuthenticationState>(services, configure);
    }

    /// <summary>
    /// Adds authentication using msal.js to Blazor applications.
    /// </summary>
    /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{ChromeMsalProviderOptions}"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, RemoteUserAccount> AddMsalAuthentication<TRemoteAuthenticationState>(
        this IServiceCollection services, Action<RemoteAuthenticationOptions<ChromeMsalProviderOptions>> configure)
        where TRemoteAuthenticationState : RemoteAuthenticationState, new()
    {
        return AddMsalAuthentication<TRemoteAuthenticationState, RemoteUserAccount>(services, configure);
    }

    /// <summary>
    /// Adds authentication using msal.js to Blazor applications.
    /// </summary>
    /// <typeparam name="TRemoteAuthenticationState">The type of the remote authentication state.</typeparam>
    /// <typeparam name="TAccount">The type of the <see cref="RemoteUserAccount"/>.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configure">A callback to configure the <see cref="RemoteAuthenticationOptions{ChromeMsalProviderOptions}"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount> AddMsalAuthentication<TRemoteAuthenticationState, TAccount>(
        this IServiceCollection services, Action<RemoteAuthenticationOptions<ChromeMsalProviderOptions>> configure)
        where TRemoteAuthenticationState : RemoteAuthenticationState, new()
        where TAccount : RemoteUserAccount
    {
        services.AddRemoteAuthentication<TRemoteAuthenticationState, TAccount, ChromeMsalProviderOptions>(configure);

        return new MsalRemoteAuthenticationBuilder<TRemoteAuthenticationState, TAccount>(services);
    }
}

internal sealed class MsalRemoteAuthenticationBuilder<TRemoteAuthenticationState, TRemoteUserAccount> : IRemoteAuthenticationBuilder<TRemoteAuthenticationState, TRemoteUserAccount>
    where TRemoteAuthenticationState : RemoteAuthenticationState, new()
    where TRemoteUserAccount : RemoteUserAccount
{

    public MsalRemoteAuthenticationBuilder(IServiceCollection services) => Services = services;

    public IServiceCollection Services { get; }
}
