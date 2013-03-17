using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Reflection;

namespace XPlatUtils.CodeSnippets
{
	static class MainClass
	{
		private const string SnippetDir = "Library/XamarinStudio-4.0/Snippets/";
		private const string SnippetFormat = 
@"<CodeTemplates version=""3.0"">
  <CodeTemplate version=""2.0"">
    <Header>
      <_Group>C#</_Group>
      <Version />
      <MimeType>text/x-csharp</MimeType>
      <Shortcut>{0}</Shortcut>
      <_Description />
      <TemplateType>Unknown</TemplateType>
    </Header>
    <Code><![CDATA[{1}]]></Code>
  </CodeTemplate>
</CodeTemplates>";
		private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>
		{
			{ "Void", "void" },
			{ "Int16", "short" },
			{ "Int32", "int" },
			{ "Int64", "long" },
			{ "String", "string" },
		};

		public static void Main (string[] args)
		{
			var assembly = Assembly.LoadFile ("/Developer/MonoTouch/usr/lib/mono/2.1/monotouch.dll");

			var exportType = assembly.GetType ("MonoTouch.Foundation.ExportAttribute");
			var type = assembly.GetType ("MonoTouch.UIKit.UITableViewSource");

			string snippetDir = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.UserProfile), SnippetDir);

			foreach (var method in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)) {
				var export = method.GetCustomAttributes (exportType, false).FirstOrDefault();
				if (export != null) {
					var selectorProperty = exportType.GetProperty ("Selector");
					string selector = selectorProperty.GetValue (export, null) as string;
					string selectorName = selector.Replace (":", string.Empty);
					string fileName = selectorName + ".template.xml";

					StringBuilder code = new StringBuilder("[Export(\"");
					code.Append (selector);
					code.AppendLine ("\")]");
					code.Append ("public ");
					code.Append (method.ReturnType.Name.FixType ());
					code.Append (' ');
					code.Append (method.Name);
					code.Append (" (");

					bool first = true;
					foreach (var parameter in method.GetParameters ()) {
						if (first) {
							first = false;
						} else {
							code.Append (", ");
						}

						code.Append (parameter.ParameterType.Name.FixType());
						code.Append (' ');
						code.Append (parameter.Name);
					}
					code.AppendLine (")");
					code.AppendLine ("{");
					code.AppendLine ("\t$end$");
					code.Append ("}");

					File.WriteAllText (Path.Combine (snippetDir, fileName), string.Format (SnippetFormat, selectorName, code));

					Console.WriteLine (selectorName);
				}
			}
		}

		private static string FixType(this string type)
		{
			string result;
			if (TypeMapping.TryGetValue (type, out result)) {
				return result;
			}
			return type;
		}
	}
}
