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

					if (item.Items != null && item.Items.Any() && documentTypeId.HasValue)
					{
						EnumerateDocumentTypes(item.Items, documentTypeId.Value, userId);
					}

				}
			}
		}

		private int? CreateDocumentTypeFolder(Item item, int parentId, int userId = 0)
		{
			var result = _contentTypeService.CreateContentTypeContainer(parentId, item.Name, userId);

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
			ContentType contentType = new ContentType(parentId);
			contentType.Name = item.Name;
			contentType.Alias = item.Alias;
			contentType.Icon = "icon-message";

			var textstringDef = new DataTypeDefinition(-1, "Umbraco.Textbox");
			var richtextEditorDef = new DataTypeDefinition(-1, "Umbraco.TinyMCEv3");
			var textboxMultipleDef = new DataTypeDefinition(-1, "Umbraco.TextboxMultiple");

			if (item.Tabs != null && item.Tabs.Any())
			{
				foreach (var tab in item.Tabs)
				{
					if (!string.IsNullOrEmpty(tab.Name))
					{
						contentType.AddPropertyGroup(tab.Name);

						foreach (var property in tab.Properties)
						{
							contentType.AddPropertyType(
								new PropertyType(textstringDef)
								{
									Alias = property.Alias,
									Name = property.Name,
									Description = "",
									Mandatory = property.Mandatory,
									SortOrder = 1
								},
								tab.Name);
						}
					}
				}
			}

			_contentTypeService.Save(contentType);

			if (contentType != null)
			{
				return contentType.Id;
			}
			else
			{
				return parentId;
			}
		}
	}
}
