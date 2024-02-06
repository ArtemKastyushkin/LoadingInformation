ConnectionConfigXML connectionConfigXML = new ConnectionConfigXML("D:\\VisualStudioProjects\\LoadingInformation_TestTask\\db_config.xml");
Database database = new Database(connectionConfigXML);

ParserXML parserXML = new ParserXML("D:\\VisualStudioProjects\\LoadingInformation_TestTask\\data_for_loading.xml");

database.InsertOrdersList(parserXML.GetOrdersList(database));

return 0;