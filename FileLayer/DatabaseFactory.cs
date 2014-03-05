using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLayer
{
    public enum DatabaseType
    {
        EncryptedBinary,
        XML,
        SQLServer
    }
    public class DatabaseFactory
    {
        public static IDatabase Create(DatabaseType type)
        {
            switch (type)
            {
                case DatabaseType.EncryptedBinary:
                    return new BinaryFile();
                    
                case DatabaseType.XML:
                    return new XMLFile();
                   
                default:
                    return null;
            }
        }
    }
}
