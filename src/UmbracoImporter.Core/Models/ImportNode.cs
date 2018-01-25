using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoImporter.Core.Models
{
	public class ImportNode
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public IEnumerable<ImportNode> Items;
	}


	public class DocumentTypeImportNode : ImportNode
	{
		public NodePermissions NodePermissions { get; set; }
		public new IEnumerable<DocumentTypeImportNode> Items;
	}

	public class DataTypeImportNode : ImportNode
	{
		public NodeProperties NodeProperties { get; set; }
		public new IEnumerable<DataTypeImportNode> Items;
	}

	public class ContentImportNode : ImportNode
	{
		public NodeProperties NodeProperties { get; set; }
		public new IEnumerable<ContentImportNode> Items;
	}


	public class NodeProperties
	{
		public string Name { get; set; }
		public string Value { get; set; }
	}


	public class NodePermissions
	{
		public bool AllowRoot { get; set; }
		public IEnumerable<ImportNode> Items;
	}

	public class Test
	{
		public Test()
		{
			var node = new DataTypeImportNode();

			//node.Items.First().Items.First().NodeProperties.Value;
		}
	}

}
