using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace CoderByteAgeCounting
{

	class Program
    {
        static void Main()
        {
			/* Ok so this isn't really a good solution but it works, the api endpoint should be fixed to have unique key-value pairs. */

			WebRequest request = WebRequest.Create("https://coderbyte.com/api/challenges/json/age-counting");
			WebResponse response = request.GetResponse();
			int count = 0;

			using (Stream stream = response.GetResponseStream())
			{
				var reader = new StreamReader(stream);
				var responseString = reader.ReadToEnd();

				var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
				foreach (Match match in Regex.Matches(data["data"], "(?<=age=)[0-9]+"))
				{
					if (match.Success && int.TryParse(match.Value, out int age) && age > 50)
					{
						count++;
					}
				}
			}
			Console.WriteLine(count);
			response.Close();
		}
    }
}
