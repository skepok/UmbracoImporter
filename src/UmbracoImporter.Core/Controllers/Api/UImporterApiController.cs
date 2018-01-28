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
		protected DocumentTypeImporter _documentTypeImporter;
		public UImporterApiController(DocumentTypeImporter documentTypeImporter)
		{
			jsonParser = new JsonParser();
			_documentTypeImporter = documentTypeImporter;
		}

		[HttpGet]
		public ImportNode MockImportJson()
		{
			ImportNode root = jsonParser.Load();

			_documentTypeImporter.Import(root.DocumentTypes);
			return root;
		}

		[HttpPost]
		public ImportNode ImportJson(string json)
		{
			ImportNode root = jsonParser.Load(json);
			_documentTypeImporter.Import(root.DocumentTypes);
			
			//var parsed = JsonConvert.SerializeObject(root);
			//return new[] { parsed };
			return root;
		}
	}
}
