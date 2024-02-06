ConnectionConfigXML connectionConfigXML = new ConnectionConfigXML("D:\\VisualStudioProjects\\LoadingInformation_TestTask\\db_config.xml");
Database db = new Database(connectionConfigXML);

UserObject userObject = new UserObject("Иванов Иван Иванович", "abc@email.com");

db.Insert(userObject);

return 0;