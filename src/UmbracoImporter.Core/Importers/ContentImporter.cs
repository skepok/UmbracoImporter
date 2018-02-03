using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Services;
using Umbraco.Web;
using UmbracoImporter.Core.Interfaces;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Importers
{
	public class ContentImporter : IContentImporter
	{
		protected IContentTypeService _contentTypeService;
		protected IContentService _contentService;
		public ContentImporter()
		{
			_contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
			_contentService = ApplicationContext.Current.Services.ContentService;

		}

		public Content Import(Content content)
		{
			var user = UmbracoContext.Current.Security.CurrentUser;

			EnumerateContent(content.Items, -1, user.Id);

			return content;
		}


		private void EnumerateContent(List<Item> items, int parentId, int userId = 0)
		{
			foreach (var item in items)
			{

			}
		}

		private int? CreateContent(Item item, int parentId, int userId = 0)
		{
			var contentTypes = _contentTypeService.GetAllContentTypeAliases();
			var contentType = contentTypes.FirstOrDefault(x => item.NodeType.Ref.Replace(" ", "").ToLower().Contains(x.ToLower()));
			var content = _contentService.CreateContent(item.Name, parentId, contentType, userId);
			return content.Id;
		}
	}
}
