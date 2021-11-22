namespace Catalog.Api.Settings
{
    public class MongoDbSettings
    {
        // same as our Settings file
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        // Here we can grab our connection string as long as our props are filled out
        public string ConnectionString_DEV 
        { 
            get
            {
                return $"mongodb://{Host}:{Port}";
            } 
        
        }

        public string ConnectionString 
        { 
            get
            {
                return $"mongodb://{User}:{Password}@{Host}:{Port}";
            } 
        
        }
    }
}