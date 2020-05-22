using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dotnet_api_goat.Controllers
{
    public class GoatsController : ApiController
    {
        /// <summary>
        /// Returns the contents of a file based on the input.
        /// Try the following file names:
        ///     - UpgradeApplicationHost.js: When running or debugging via IISExpress.
        ///     - dotnet-api-goat.Tests.dll.config: When running using VSTest.
        /// </summary>
        /// <param name="name">The name of the file to read.</param>
        /// <returns>The text contents of the file.</returns>
        /// <cwe_id>CWE-73</cwe_id>
        /// <cwe_description>External Control of File Name or Path</cwe_description>
        /// <cwe_exploit_url>https://localhost:44381/api/goats/file?name=dotnet-api-goat.Tests.dll.config</cwe_exploit_url>
        /// <cwe_status>NOT_IMPLEMENTED</cwe_status>
        public HttpResponseMessage GetFile(string name)
        {
            try
            {
                using (StreamReader r = new StreamReader(name))
                {
                    string content = r.ReadToEnd();
                    return Request.CreateResponse(HttpStatusCode.OK, content);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        /// <summary>
        /// Executes an arbitary OS command.
        /// </summary>
        /// <param name="command">The OS command to execute.</param>
        /// <returns>The standard output of the command.</returns>
        /// <cwe_id>CWE-78</cwe_id>
        /// <cwe_description>OS Command Injection</cwe_description>
        /// <cwe_exploit_url>https://localhost:44381/api/goats/command?command=dir</cwe_exploit_url>
        /// <cwe_status>NOT_IMPLEMENTED</cwe_status>
        public HttpResponseMessage GetCommand(string command)
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

        /// <summary>
        /// Echoes back an HTML response based on the input.
        /// </summary>
        /// <param name="text">The text to include in the HTML.</param>
        /// <returns>A paragraph containing the text.</returns>
        /// <cwe_id>CWE-79</cwe_id>
        /// <cwe_description>Improper Neutralization of Input During Web Page Generation ('Cross-site Scripting')</cwe_description>
        /// <cwe_exploit_url>https://localhost:44381/api/goats/echo?text=hello</cwe_exploit_url>
        /// <cwe_status>NOT_IMPLEMENTED</cwe_status>
        public HttpResponseMessage GetEcho(string text)
        {
            return Request.CreateResponse(HttpStatusCode.OK, text);
        }
    }
}
