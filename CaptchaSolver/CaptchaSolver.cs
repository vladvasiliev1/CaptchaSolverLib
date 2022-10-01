using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Threading;

namespace CaptchaSolver
{
    //Recaptcha v2 callback
    public class CaptchaSolver
    {
        string apiKey;

        public CaptchaSolver(string apiKey)        
        {
            this.apiKey = apiKey;
        }

        public string Solve(string siteKey,string url)
        {
            try
            {
                HttpRequest httpRequest = new HttpRequest();

                string response = httpRequest.Get($"http://rucaptcha.com/in.php?key={apiKey}&method=userrecaptcha&googlekey={siteKey}&pageurl={url}").ToString();

                int id;

                if (response.ToLower().Contains("ok"))
                {
                    id = int.Parse(response.Split('|')[1]);

                    m1:
                    Thread.Sleep(20000);

                    response = httpRequest.Get($"http://rucaptcha.com/res.php?key={apiKey}&action=get&id={id}").ToString();

                    if (response.ToLower().Contains("ok"))
                    {
                        return response.Split('|')[1];
                    }
                    else
                    {
                        Thread.Sleep(10000);

                        goto m1;
                    }
                }

                

                
            }
            catch
            {

            }

            return null;
        }

        

    }
}
