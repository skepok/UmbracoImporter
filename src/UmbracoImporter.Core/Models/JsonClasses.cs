using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoImporter.Core.Models
{
	class JsonClasses
	{
	}


	public class Meta
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Version { get; set; }
	}

	public class Environment
	{
		public string Name { get; set; }
		public string Host { get; set; }
		public string ConnectionString { get; set; }
	}

	public class Config
	{
		public string Mentor { get; set; }
		public Meta Meta { get; set; }
		public List<Environment> Environments { get; set; }
	}

	public class NodeType
	{
		public string Name { get; set; }
		public string Ref { get; set; }
	}

	public class StartNode
	{
		public string Ref { get; set; }
	}

	public class Item
	{
		public string Name { get; set; }
		public NodeType NodeType { get; set; }
		public StartNode StartNode { get; set; }
		public List<Item> Items { get; set; }
	}

	public class DataTypes
	{
		public List<Item> Items { get; set; }
	}

	public class Permissions
	{
		public bool AllowRoot { get; set; }
	}


	public class Property
	{
		public string Name { get; set; }
		public NodeType NodeType { get; set; }
		public bool Mandatory { get; set; }
	}

	public class Tab
	{
		public string Name { get; set; }
		public List<Property> Properties { get; set; }
	}
	
	public class DocumentTypes
	{
		public List<Item> Items { get; set; }
	}
	
	public class Value
	{
		public string Ref { get; set; }
	}

	public class Content
	{
		public List<Item> Items { get; set; }
	}

	public class ImportNode
	{
		public Config Config { get; set; }
		public DataTypes DataTypes { get; set; }
		public DocumentTypes DocumentTypes { get; set; }
		public Content Content { get; set; }
	}

}
