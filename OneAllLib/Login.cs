using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAllLib
{
  public static class Login
  {
    static async Task<string> AuthenticateApplication(string domain)
    {
      var url = "http://localhost:1234/login/";

      var loginForm = @"<html><head>
<script type=""text/javascript"">
 
  /* Replace #your_subdomain# by the subdomain of a Site in your OneAll account */    
  var oneall_subdomain = '"+domain+@"';
 
  /* The library is loaded asynchronously */
  var oa = document.createElement('script');
  oa.type = 'text/javascript'; oa.async = true;
  oa.src = 'https://' + oneall_subdomain + '.api.oneall.com/socialize/library.js';
  var s = document.getElementsByTagName('script')[0];
  s.parentNode.insertBefore(oa, s);
       
</script>
</head><body>
<div id=""oa_social_login_container""></div>
<script type=""text/javascript""> 
  var your_callback_script = '"+url+@"';
  var _oneall = _oneall || [];
  _oneall.push(['social_login', 'set_providers', ['facebook', 'google', 'github', 'reddit']]);
  _oneall.push(['social_login', 'set_callback_uri', your_callback_script]);
  _oneall.push(['social_login', 'do_render_ui', 'oa_social_login_container']);
</script>
</body>
</html>
  ";
      HttpListener listener = new HttpListener();
      listener.Prefixes.Add(url);
      listener.Start();

      // start browser
      var process = System.Diagnostics.Process.Start(url);

      
      
      //serve login form
      var ctx = listener.GetContext();
      ctx.Response.AppendHeader("Access-Control-Allow-Origin", "*");
      ctx.Response.StatusCode = (int)HttpStatusCode.OK;
      ctx.Response.StatusDescription = "OK";
      using (var writer = new StreamWriter(ctx.Response.OutputStream))
      {
        writer.Write(loginForm);
      }
      ctx.Response.OutputStream.Close();

      // read authentication response
      ctx = listener.GetContext();
      //process.Close();

      using (var reader = new StreamReader(ctx.Request.InputStream))
      {
        var str = reader.ReadToEnd();
        return str;
      }


    }
  }
}
