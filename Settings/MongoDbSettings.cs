namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        // same as our Settings file
        public string Host { get; set; }
        public int Port { get; set; }

        // Here we can grab our connection string as long as our props are filled out
        public string ConnectionString 
        { 
            get
            {
                return $"mongodb://{Host}:{Port}";
            } 
        
        }
    }
}