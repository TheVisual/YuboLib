﻿using System.Collections.Generic;
using System.Threading.Tasks;
using YuboLib.Extras;
using YuboLib.REST;
using YuboLib;

namespace SnapchatLib.REST.Endpoints;

public interface ILoginEndpoint
{
    Task<string> Login(string password);
}

internal class LoginEndpoint : EndpointAccessor, ILoginEndpoint
{
    internal static readonly EndpointInfo LoginEdnpoint = new()
    {
        Url = "/login",
        BaseEndpoint = RequestConfigurator.ApiBaseEndpoint,
    };

    public LoginEndpoint(YuboClient client, IYuboHttpClient httpClient, YuboLockedConfig config, IClientLogger logger, IUtilities utilities, IRequestConfigurator configurator) : base(client, httpClient, config, logger, utilities, configurator)
    {
    }

    public async Task<string> Login(string password)
    {
        var parameters = new Dictionary<string, string>
        {
            {"username", Config.Username },
            {"type", "username" },
            {"password", password },
        };
        var response = await Send(LoginEdnpoint, parameters);
        return m_Utilities.JsonDeserializeObject<string>(await response.Content.ReadAsStringAsync());
    }
}