using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;
using Umbraco.Core.Services;
using UmbracoImporter.Core.Interfaces;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Importers
{
	public class DocumentTypeImporter : IDocumentTypeImporter
	{
		protected IContentTypeService _contentTypeService;
		public DocumentTypeImporter()
		{
			//_contentTypeService = contentTypeService;
			_contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
		}
		public DocumentTypes Import(DocumentTypes documentTypes)
		{
			foreach (var item in documentTypes.Items)
			{
				if (!string.IsNullOrEmpty(item.Name))
				{
					var res = _contentTypeService.CreateContentTypeContainer(0, item.Name);
				}
			}

			return documentTypes;
		}

		private void CreateDocumentType(string name)
		{
			//create it

			//return id or node?
		}
	}
}
