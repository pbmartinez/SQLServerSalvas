using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace Core.Application.Services
{
    public class BackUpService : IBackUpService
    {

        public BackUpService()
        {

        }

        public void FullBackUpAsync(Salvas salva)
        {
            var server = new Server(salva.Conexion.Servidor);
            var baseDatos = server.Databases[$"{salva.Conexion.BaseDatos}"];


            var backup = new Backup
            {
                Action = BackupActionType.Database,
                Database = baseDatos.Name,
                BackupSetName = $"{salva.Nombre}",
                Initialize = true,
                CompressionOption = BackupCompressionOptions.On
            };
            backup.Devices.AddDevice($@"{salva.Conexion.Path}{salva.FullName}", DeviceType.File);


            //backup.Complete += Backup_Complete;
            backup.SqlBackupAsync(server);

            if (server.ConnectionContext.IsOpen)
                server.ConnectionContext.Disconnect();
        }

        //private void Backup_Complete(object sender, Microsoft.SqlServer.Management.Common.ServerMessageEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
