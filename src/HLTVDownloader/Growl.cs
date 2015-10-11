using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

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

        public async void Notify(string message = "Download complete")
        {
            try
            {
                if (!_isRegistered) 
                {
                    await RegisterToGrowl();
                    _isRegistered = true;
                }
                
                await SendNotification(message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error notifying via Growl: {0}", e.Message);
            }
        }

        private static Task SendToGrowl(IEnumerable<string> commands, IEnumerable<KeyValuePair<string, byte[]>> binaryResources = null)
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
                        
                        if (binaryResources != null)
                        {
                            foreach (var kvp in binaryResources)
                            {
                                if (kvp.Value.Length == 0)
                                    continue;

                                socket.Send(Encoding.ASCII.GetBytes(string.Format("Identifier: {0}\r\n", kvp.Key)));
                                socket.Send(Encoding.ASCII.GetBytes(string.Format("Length: {0}\r\n", kvp.Value.Length)));
                                socket.Send(Encoding.ASCII.GetBytes("\r\n"));
                                socket.Send(kvp.Value);
                                socket.Send(Encoding.ASCII.GetBytes("\r\n"));
                            }
                        }
                        socket.Send(Encoding.ASCII.GetBytes("\r\n"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error sending message to Growl: {0}", e.Message);
                    }
                    finally
                    {
                        socket.Close();
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        private static Task RegisterToGrowl()
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

            return SendToGrowl(commands);
        }

        private static Task SendNotification(string message)
        {
            var commands = new [] {
                "GNTP/1.0 NOTIFY NONE",
                "Application-Name: " + ApplicationName,
                "Notification-Name: " + DownloadComplete,
                "Notification-Title: Download complete",
                "Notification-Text: " + message
            };

            return SendToGrowl(commands);
        }
    }
}

