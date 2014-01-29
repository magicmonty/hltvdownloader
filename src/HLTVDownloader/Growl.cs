using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NLog.Targets;

namespace PaganSoft.HLTVDownloader
{
    public class Growl
    {
        private bool _isRegistered;
        private const string ApplicationName = "HLTVDownloader";
        private const string DownloadComplete = "Download Complete";

        public Growl()
        {
            _isRegistered = false;
        }

        public void Notify(string message = "Download complete")
        {
            if (!_isRegistered)
                registerToGrowl().ContinueWith(t =>
                {
                    notify(message);
                    _isRegistered = true;
                }, TaskContinuationOptions.NotOnFaulted).Wait(1000);
            else
                notify(message);
        }

        private Task sendToGrowl(IEnumerable<string> commands)
        {
            return Task.Factory.StartNew(() =>
            {
                const int port = 23053;
                var address = IPAddress.Parse("127.0.0.1");
                var endPoint = new IPEndPoint(address, port);

                using (var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    socket.Connect(endPoint);
                    try
                    {
                        foreach (var line in commands)
                            socket.Send(Encoding.ASCII.GetBytes(line + "\r\n"));
                        socket.Send(Encoding.ASCII.GetBytes("\r\n"));
                    }
                    finally
                    {
                        socket.Close();
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        private Task registerToGrowl()
        {
            var commands = new [] {
                "GNTP/1.0 REGISTER NONE",
                "Application-Name: " + ApplicationName,
                "Application-Icon: http://www.userlogos.org/files/logos/disser2/OTR.GIF",
                "Notifications-Count: 1",
                "",
                "Notification-Name: " + DownloadComplete,
                "Notification-Display-Name: Download complete",
                "Notification-Icon: http://www.userlogos.org/files/logos/disser2/OTR.GIF",
                "Notification-Enabled: True",
            };

            return sendToGrowl(commands);
        }

        private Task notify(string message)
        {
            var commands = new [] {
                "GNTP/1.0 NOTIFY NONE",
                "Application-Name: " + ApplicationName,
                "Notification-Name: " + DownloadComplete,
                "Notification-Title: Download complete",
                "Notification-Text: " + message
            };

            return sendToGrowl(commands);
        }
    }
}

