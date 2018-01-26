using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbracoImporter.Core.Models;

namespace UmbracoImporter.Core.Importers
{
	public class DocumentTypeImporter
	{
		public void Import(DocumentTypes documentTypes)
		{
			foreach(var item in documentTypes.Items)
			{
				if (string.IsNullOrEmpty(item.Name))
				{

				}
			}
		}

		private void CreateDocumentType(string name)
		{
			//create it

			//return id or node?
		}
	}
}
