using System.Text;
using System.Xml;

public class ConnectionConfigXML : ConnectionConfig
{
    private string _filePath;

    public ConnectionConfigXML(string filePath)
    {
        _filePath = filePath;
    }

    public override string GetConnectionString()
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(_filePath);

        XmlElement? xmlRoot = xmlDocument.DocumentElement;

        StringBuilder builder = new StringBuilder();

        if (xmlRoot != null)
        {
            foreach (XmlNode node in xmlRoot) 
            {
                builder.Append($"{node.Name}={node.InnerText};");
            }
        }

        return builder.ToString();
    }
}