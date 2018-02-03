using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;
using UmbracoImporter.Core.Interfaces;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Importers
{
	public class DataTypeImporter : IDataTypeImporter
	{
		protected IContentTypeService _contentTypeService;
		protected IContentService _contentService;
		protected IDataTypeService _dataTypeService;
		public DataTypeImporter()
		{
			_contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
			_contentService = ApplicationContext.Current.Services.ContentService;
			_dataTypeService = ApplicationContext.Current.Services.DataTypeService;

		}

		public DataTypes Import(DataTypes dataTypes)
		{
			var user = UmbracoContext.Current.Security.CurrentUser;

			EnumerateDataTypes(dataTypes.Items, -1, user.Id);

			return dataTypes;
		}


		private void EnumerateDataTypes(List<Item> items, int parentId, int userId = 0)
		{
			foreach (var item in items)
			{
				int? dataTypeId = CreateDataType(item, parentId, userId);

				if (item.Items != null && item.Items.Any() && dataTypeId.HasValue)
				{
					EnumerateDataTypes(item.Items, dataTypeId.Value, userId);
				}
			}
		}

		private int? CreateDataType(Item item, int parentId, int userId = 0)
		{
			var dataType = new DataTypeDefinition(parentId, "Umbraco.ContentPicker2");

			dataType.Name = item.Name;
			//need to create content in order to select start node

			var ttt = _dataTypeService.GetAllDataTypeDefinitions();

			_dataTypeService.Save(dataType);
			
			return dataType.Id;
		}
	}
}
