using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core
{
	public class JsonImporter
	{
		public void Load()
		{
			using (StreamReader r = new StreamReader(@"C:\Data\Projects\UmbracoImporter\src\UmbracoImporter\json-site-spec.json"))
			{
				string json = r.ReadToEnd();
				ImportNode items = JsonConvert.DeserializeObject<ImportNode>(json);

				dynamic test = JsonConvert.DeserializeObject(json);

				var dd = test.DataTypes;

				var ds = JsonConvert.DeserializeObject<ImportNode>(dd);
			}
		}
	
	}
}
