using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Avv.Apps.Commons
{
    public class XmlToJson
    {
        private static void AddIndent(StringBuilder sb, int level)
        {
            sb.Append("".PadLeft(level * 2));
        }

        private static string Indent(int level)
        {
            return "".PadLeft(level * 2);
        }

        public static string Parse(string xml)
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            ToJson(XDocument.Load(new StringReader(xml)).Root, sb, 1, true);
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static void ToJson(XElement elem, StringBuilder sb, int level, bool isWriteName)
        {
            if (isWriteName)
            {
                ToJson(elem.Name, sb, level, false);
            }
            else
            {
                sb.Append(Indent(level));
            }

            if (!elem.HasAttributes && !elem.HasElements)
            {
                ToValue(elem.Value, sb);
                return;
            }

            sb.AppendLine("{");

            if (elem.HasAttributes)
            {
                foreach (XAttribute attr in elem.Attributes())
                {
                    ToJson(attr, sb, level + 1);
                    sb.AppendLine(",");
                }
                sb.Remove(sb.Length - 2, 1);
            }

            if (elem.HasElements)
            {
                IEnumerable<XName> names = 
                    elem.Elements().Select(e => e.Name).Distinct();

                foreach (XName en in names)
                {
                    var ds = elem.Elements(en);

                    if (1 < ds.Count())
                    {
                        ToJson(ds.First().Name, sb, level + 1, false);
                        sb.Append("[ ");
                        foreach (var d in ds)
                        {
                            ToJson(d, sb, level + 1, false);
                        }
                        sb.AppendLine("]");
                    }
                    else
                    {
                        ToJson(ds.First(), sb, level + 1, true);
                    }
                    sb.AppendLine(",");
                }
            }

            sb.Append(Indent(level)).AppendLine("}");
        }

        public static void ToJson(XAttribute elem, StringBuilder sb, int level)
        {
            ToJson(elem.Name, sb, level, true);
            ToValue(elem.Value, sb);
        }

        private static void ToJson(XName name, StringBuilder sb, int level, bool isAttr)
        {
            sb.Append(Indent(level));
            sb.Append("\"");
            sb.Append(isAttr ? "@" : "");
            sb.Append(name.LocalName);
            sb.Append("\": ");
        }

        private static void ToValue(object value, StringBuilder sb)
        {
            if (value != null)
            {
                sb.Append("\"");
                sb.Append(value.ToString());
                sb.Append("\"");
            }
            else
            {
                sb.Append("null");
            }
        }




        //public static void ToJson(XElement elem, StringBuilder sb, int level, bool writeName)
        //{
        //    AddIndent(sb, level);

        //    if (writeName)
        //    {
        //        ToName(elem.Name.LocalName, sb, level);
        //    }

        //    if (!elem.HasAttributes && !elem.HasElements)
        //    {
        //        ToValue(elem.Value, sb, level);
        //        return;
        //    }

        //    sb.AppendLine("{");

        //    if (elem.HasAttributes)
        //    {
        //        foreach (var attr in elem.Attributes())
        //        {
        //            AddIndent(sb, level);
        //            ToName("@" + attr.Name.LocalName, sb, level);
        //            ToValue(attr.Value, sb, level);
        //            sb.AppendLine(",");
        //        }
        //    }

        //    if (elem.HasElements)
        //    {
        //        XElement prev = null;
        //        foreach (var chld in elem.Descendants())
        //        {
        //            bool writeNameByChild = 1 < elem.Descendants(chld.Name).Count();
        //            ToJson(chld, sb, level + 1, (prev == null || chld.Name != prev.Name));

        //            AddIndent(sb, level);
        //            sb.AppendLine(",");
        //            prev = chld;
        //        }
        //    }
        //    else
        //    {
        //        ToValue(elem.Value, sb, level);
        //    }

        //    AddIndent(sb, level);
        //    sb.AppendLine("}");
        //}

        //private static void ToName(string name, StringBuilder sb, int level)
        //{
        //    sb.Append("\"");
        //    sb.Append(name);
        //    sb.Append("\": ");
        //}

        //private static void ToValue(object value, StringBuilder sb, int level)
        //{
        //    if (value != null)
        //    {
        //        sb.Append(value.ToString());
        //    }
        //    else
        //    {
        //        sb.Append("null");
        //    }
        //}

        //private static void ParseElement(XElement elem, StringBuilder sb, int level, bool writeName)
        //{
        //    if (writeName)
        //    {
        //        ParseName(elem.Name.LocalName, sb, level);
        //    }

        //    if (!elem.HasAttributes && !elem.HasElements)
        //    {
        //        ParseValue(elem.Value, sb, level);
        //        return;
        //    }

        //    sb.AppendLine("{");

        //    if (elem.HasAttributes)
        //    {
        //        foreach (var attr in elem.Attributes())
        //        {
        //            ParseName("@" + attr.Name.LocalName, sb, level);
        //            ParseValue(attr.Value, sb, level);
        //            sb.AppendLine(",");
        //        }
        //    }

        //    if (elem.HasElements)
        //    {
        //        XElement prev = null;
        //        foreach (var chld in elem.Descendants())
        //        {
        //            bool writeNameByChild = 1 < elem.Descendants(chld.Name).Count();
        //            ParseElement(chld, sb, level + 1, (prev == null || chld.Name != prev.Name));
        //            sb.AppendLine(",");
        //            prev = chld;
        //        }
        //    }
        //    else
        //    {
        //        ParseValue(elem.Value, sb, level);
        //    }

        //    sb.AppendLine("}");

        //}

        //private static void ParseName(string name, StringBuilder sb, int level)
        //{
        //    sb.Append("\"");
        //    sb.Append(name);
        //    sb.Append("\": ");
        //}

        //private static void ParseValue(object value, StringBuilder sb, int level)
        //{
        //    if (value != null)
        //    {
        //        sb.Append(value.ToString());
        //    }
        //    else
        //    {
        //        sb.Append("null");
        //    }
        //}

    }
}
