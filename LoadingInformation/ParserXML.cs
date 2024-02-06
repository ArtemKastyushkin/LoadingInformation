using System.Xml;

public class ParserXML : Parser
{
    private string _filePath;

    private PositionObject parseProduct(XmlNode xmlNode, Database database, long orderId)
    {
        int productQuantity = 0;
        string productName = string.Empty;
        decimal productPrice = 0.0m;

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            switch (childNode.Name)
            {
                case "quantity":
                    productQuantity = Convert.ToInt32(childNode.InnerText);
                    break;
                case "name":
                    productName = childNode.InnerText;
                    break;
                case "price":
                    productPrice = Convert.ToDecimal(childNode.InnerText.Replace(".", ","));
                    break;
                default:
                    break;
            }
        }

        ProductObject product = new ProductObject(productName, productPrice);

        return new PositionObject(database.GetId(product), orderId, productQuantity);
    }

    private int parseUser(XmlNode xmlNode, Database database)
    {
        string userFullname = string.Empty;
        string userEmail = string.Empty;

        foreach (XmlNode childNode in xmlNode.ChildNodes)
        {
            switch (childNode.Name)
            {
                case "fio":
                    userFullname = childNode.InnerText;
                    break;
                case "email":
                    userEmail = childNode.InnerText;
                    break;
                default:
                    break;
            }
        }

        UserObject user = new UserObject(userFullname, userEmail);

        database.Insert(user);

        return database.GetId(user);
    }

    public ParserXML(string filePath)
    {
        _filePath = filePath;
    }

    public override (List<OrderObject>, List<PositionObject>) GetOrdersList(Database database)
    {
        List<OrderObject> ordersList = new List<OrderObject>();
        List<PositionObject> positionsList = new List<PositionObject>();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(_filePath);

        XmlElement? xmlRoot = xmlDocument.DocumentElement;

        if (xmlRoot != null)
        {
            foreach (XmlNode node in xmlRoot)
            {
                long orderId = 0;
                long userId = 0;
                decimal orderSum = 0.0m;
                DateTime orderRegDate = DateTime.MinValue;

                foreach(XmlNode childNode in node.ChildNodes)
                {
                    switch (childNode.Name)
                    {
                        case "no":
                            orderId = Convert.ToInt64(childNode.InnerText);
                            break;
                        case "reg_date":
                            orderRegDate = Convert.ToDateTime(childNode.InnerText);
                            break;
                        case "sum":
                            orderSum = Convert.ToDecimal(childNode.InnerText.Replace(".", ","));
                            break;
                        case "product":
                            positionsList.Add(parseProduct(childNode, database, orderId));
                            break;
                        case "user":
                            userId = parseUser(childNode, database);
                            break;
                        default: 
                            break;
                    }
                }

                ordersList.Add(new OrderObject(orderId, userId, orderSum, orderRegDate));
            }
        }

        return (ordersList, positionsList);
    }
}