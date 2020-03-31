using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet_api_goat.Controllers
{
    public class GoatsController : ApiController
    {
        public HttpResponseMessage PostCommand(string command)
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C " + command;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.Dispose();
            return Request.CreateResponse(HttpStatusCode.OK, output);
        }
    }
}
