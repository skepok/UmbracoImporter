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
	public class JsonParser
	{
		public ImportNode Load()
		{
			//var path = @"C:\Data\Projects\UmbracoImporter\src\json-site-spec.json";
			var path = @"D:\MentorDigital-D\Projects\Projects\UmbracoImporter\src\json-site-spec.json";
			
			  ImportNode importNode = new ImportNode();
			using (StreamReader r = new StreamReader(path))
			{
				string json = r.ReadToEnd();

				importNode = JsonConvert.DeserializeObject<ImportNode>(json);
				//dynamic test = JsonConvert.DeserializeObject(json);

				//var dataTypes = test.DataTypes;
				//var ds = JsonConvert.DeserializeObject<DataTypes>(dataTypes.ToString());

				//var documentTypes = test.DocumentTypes;
				//var dt = JsonConvert.DeserializeObject<DocumentTypes>(documentTypes.ToString());
				
				//var content = test.Content;
				//var cc = JsonConvert.DeserializeObject<Content>(content.ToString());
			}

			return importNode;
		}


		public ImportNode Load(string json)
		{	
			ImportNode importNode = new ImportNode();
			importNode = JsonConvert.DeserializeObject<ImportNode>(json);
			return importNode;
		}

	}
}
