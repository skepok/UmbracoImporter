using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace UmbracoImporter.Core
{
	public class YamlParser
	{
		public void Load()
		{
			using (StreamReader reader = new StreamReader(@"C:\Data\Projects\UmbracoImporter\src\UmbracoImporter\mentor-site-spec.txt"))
			{
				// Load the stream
				var yaml = new YamlStream();
				yaml.Load(reader);

				// Examine the stream
				var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
				string s = string.Empty;
				foreach (var entry in mapping.Children)
				{
					s += (((YamlScalarNode)entry.Key).Value);
				}

				// List all the items
				var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("items")];
				foreach (YamlMappingNode item in items)
				{
					//output.WriteLine(
					//	"{0}\t{1}",
					//	item.Children[new YamlScalarNode("part_no")],
					//	item.Children[new YamlScalarNode("descrip")]
					//);
				}
			}
		}
	}
}