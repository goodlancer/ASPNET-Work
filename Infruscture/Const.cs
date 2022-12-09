using System.Collections.Generic;

namespace Infrastructure
{
    public class Const
    {
        public const string DEFAULT_ADMIN_ROLE_ID = "C646D104-C2DE-4246-AF21-BCD9FBC3AB4D";
        public const string DEFAULT_SUPER_ADMIN_ROLE_ID = "E398ABF6-C61B-42DE-8E68-35B42E924D98";
        public const string DEFAULT_USER_ROLE_ID = "02899B9D-5769-42A7-A406-251341588222";
        public const string DEFAULT_CLIENT_ROLE_ID = "3de6ac94-e507-4c56-8580-c567de78f85d";

        public const string EMAIL_TEMPLATE_INVITATION_SEND = "account_creation_invitation";
        public const string EMAIL_TEMPLATE_ACCOUNT_CREATION = "account_creation";
        public const string EMAIL_TEMPLATE_ACCOUNT_CONFIRMATION = "account_confirmation";
        public const string EMAIL_TEMPLATE_ACCOUNT_CLIENT_CONFIRMATION = "account_client_creation_invitation";
        public const string EMAIL_TEMPLATE_PASSWORD_FORGOT = "forgot_password";

        public const string PROPERTY_LIST_PROVIDER_AGENT_PRO = "agent_pro";
        public const string PROPERTY_LIST_PROVIDER_PRYCD = "prycd";
        public const string PROPERTY_SEQUENCE_NUMBER = "property_sequence_number";

        public const string PROPERTY_LIST_IMPORT_FILE_TYPE_CSV = "csv";

        public static List<string> DEFAULT_COLUMNS = new List<string> { "APN","APNFormatted","LandUse","LandUse","Longitude","Longitude","LotArea","ACRES","LotAreaUnits","acres","LotNumber",
        "LegalLot","MailAddress","MailingStreetAddress","MCity","MailCity","MState","MailState","MUnitNumber","MailUnitNumber","MZip","MailZZIP9","MZip4","MailZZIP9","Owner1FName",
        "Owner1FirstName","Owner1LName","OwnerLastname1","Owner2FName","Owner2FirstName","Owner2LName","OwnerLastname2","OwnerNameFormatted","OwnerMailingName","OwnerNames",
        "OwnersAll","PropertyAddress","SitusStreetAddress","PState","SitusState","PStreetName","SitusStreetName","PStreetSuffix","SitusMode","PZip","SitusZipCode","PZip4",
        "SitusZip9","LegalDescription", "Latitude" };

    }
}
