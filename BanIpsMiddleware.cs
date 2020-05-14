using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UI.middleware
{
    public class BanIpsMiddleware
    {
        private class RootJson
        {
            public RootJson()
            {
                Ips = new List<IpObject>();
            }
            public List<IpObject> Ips { get; set; }
        }
        private class IpObject
        {
            public string Ip { get; set; }
            public string Detail { get; set; }
        }
        private RequestDelegate _next;
        public BanIpsMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            var fileroot = Path.Combine(Directory.GetCurrentDirectory(), "banIps.json");
            var JSON = System.IO.File.ReadAllText(fileroot);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<RootJson>(JSON);
            var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString();
            var finalResult = result == null ? false : result.Ips.Any(x => x.Ip == remoteIpAddress);
            if (finalResult)
            {
                await context.Response.WriteAsync($"you are ban by this site for {result.Ips.Find(x=>x.Ip==remoteIpAddress).Detail}");
            }
            else
            {
                await _next(context);
            }
            
        }
    }
}
