using Npgsql;

ConnectionConfigXML connectionConfigXML = new ConnectionConfigXML("D:\\VisualStudioProjects\\LoadingInformation_TestTask\\db_config.xml");

Database db = new Database(connectionConfigXML);

return 0;