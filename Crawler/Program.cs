using System;
//
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//git add -u

namespace Crawler
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            //var websiteUr1 = args[0];
            //string websiteUr1 = "https://pja.edu.pl/";

            if (args.Length < 1)
            {
                throw new ArgumentNullException();
            }
            var websiteUr = args[0];
            var httpClient = new HttpClient();

            // uri
            // https://docs.microsoft.com/pl-pl/dotnet/api/system.uri?view=net-6.0

            Uri uriTestbox;
            var iswebsiteUr = Uri.TryCreate(websiteUr, UriKind.Absolute, out uriTestbox);
            if (!iswebsiteUr)
            {
                throw new ArgumentException();
            }
            var source = await httpClient.GetAsync(websiteUr);
            if (!source.IsSuccessStatusCode)
            {
                throw new Exception("## Download site error !/ Blad w czasie pobierania strony ##");
            }
            httpClient.Dispose();
            var page = await source.Content.ReadAsStringAsync();

            //var list = new List<string>();
            //var dictionary = new Dictionary<string, string>();

            //var response = await httpClient.GetAsync(websiteUr1);
            //var content = await response.Content.ReadAsStringAsync();


            // var regex = new Regex(@"[await-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.=]+");


            var regex = new Regex(@"[a-zA-Z0-9_]+@([a-zA-Z]*\.)*[a-zA-Z]*");
            var matchCollection = regex.Matches(page);


            var set = new HashSet<string>();
            if (matchCollection.Count < 1)
            {
                Console.WriteLine(" ## No mails found!/Nie znaleziono adresów strony ##");
            }

            foreach (var item in matchCollection)

            {

                Console.WriteLine(item);
                if (!set.Contains(item.ToString()))
                {
                    Console.WriteLine(item);
                    set.Add(item.ToString());
                }
            }
        }
    }
}