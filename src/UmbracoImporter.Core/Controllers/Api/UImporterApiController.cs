using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbracoImporter.Core.Importers;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Controllers.Api
{
	public class UImporterApiController : UmbracoAuthorizedApiController
	{
		protected JsonParser jsonParser;
		public UImporterApiController()
		{
			jsonParser = new JsonParser();
		}
		[HttpGet]
		public IEnumerable<string> GetAllProducts()
		{
			return new[] { "Table", "Chair", "Desk", "Computer", "Beer fridge" };
		}

		[HttpPost]
		public string ImportJson(string json)
		{
			ImportNode root = jsonParser.Load(json);

			//DocumentTypeImporter documentTypeImporter = new DocumentTypeImporter();
			//documentTypeImporter.Import(root.DocumentTypes);
			var parsed = JsonConvert.SerializeObject(root);

			return parsed;
		}
	}
}
