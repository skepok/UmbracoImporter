using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco.cms.businesslogic.web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using UmbracoImporter.Core.Interfaces;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Importers
{
	public class DocumentTypeImporter : IDocumentTypeImporter
	{
		protected IContentTypeService _contentTypeService;
		protected IContentService _contentService;
		public DocumentTypeImporter()
		{
			_contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
			_contentService = ApplicationContext.Current.Services.ContentService;
		}

		public DocumentTypes Import(DocumentTypes documentTypes)
		{
			var user = UmbracoContext.Current.Security.CurrentUser;

			EnumerateDocumentTypes(documentTypes.Items, -1, user.Id);

			return documentTypes;
		}


		private void EnumerateDocumentTypes(List<Item> items, int parentId, int userId = 0)
		{
			foreach (var item in items)
			{
				if (!string.IsNullOrEmpty(item.Name))
				{
					int? documentTypeId = null;
					if (item.NodeType.Name == "Folder")
					{
						documentTypeId = CreateDocumentTypeFolder(item, parentId, userId);				
					}
					else
					{
						documentTypeId = CreateDocumentType(item, parentId, userId);
					}

					if (item.Items.Any() && documentTypeId.HasValue)
					{
						EnumerateDocumentTypes(item.Items, documentTypeId.Value, userId);
					}

				}
			}
		}

		private int? CreateDocumentTypeFolder(Item item, int parentId, int userId = 0)
		{
			var result = _contentTypeService.CreateContentTypeContainer(parentId, item.Alias, userId);

			if (result.Success)
			{
				return result.Result.Entity.Id;
			}
			else
			{
				return null;
			}		
		}

		private int? CreateDocumentType(Item item, int parentId, int userId = 0)
		{
			var result = _contentService.CreateContent(item.Name, parentId, item.Alias, userId);
			
			if (result != null)
			{
				return result.Id;
			}
			else
			{
				return null;
			}
		}
	}
}
