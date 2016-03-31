using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PaganSoft.HLTVDownloader
{
    public class Growl
    {
        private static Growl _instance = null;

        private bool _isRegistered;
        private const string ApplicationName = "HLTVDownloader";
        private const string DownloadComplete = "Download Complete";

        private Growl()
        {
            _isRegistered = false;
        }

        public static Task Notify(string message = "Download complete")
        {
            if (_instance == null)
                _instance = new Growl();

            return _instance.NotifyInternal(message);
        }

        private async Task NotifyInternal(string message)
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
                Console.WriteLine($"Error notifying via Growl: {e.Message}");
            }
        }

        private static async Task SendToGrowl(IEnumerable<string> commands, IEnumerable<KeyValuePair<string, byte[]>> binaryResources = null)
        {
            const int port = 23053;
            var address = IPAddress.Parse("127.0.0.1");
            var endPoint = new IPEndPoint(address, port);

            using (var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    await Task.Factory.FromAsync(socket.BeginConnect(endPoint, null, socket), socket.EndConnect);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error connecting to Growl: {e.Message}");
                    return;
                }

                try
                {
                    foreach (var line in commands)
                        await Send(socket, Encoding.ASCII.GetBytes(line + "\r\n"));

                    if (binaryResources != null)
                    {
                        foreach (var kvp in binaryResources)
                        {
                            if (kvp.Value.Length == 0)
                                continue;

                            await Send(socket, Encoding.ASCII.GetBytes($"Identifier: {kvp.Key}\r\n"));
                            await Send(socket, Encoding.ASCII.GetBytes($"Length: {kvp.Value.Length}\r\n"));
                            await Send(socket, Encoding.ASCII.GetBytes("\r\n"));
                            await Send(socket, kvp.Value);
                            await Send(socket, Encoding.ASCII.GetBytes("\r\n"));
                        }
                    }

                    await Send(socket, Encoding.ASCII.GetBytes("\r\n"));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error sending message to Growl: {e.Message}");
                }
                finally
                {
                    socket.Close();
                }
            }
        }

        private static async Task<int> Send(Socket socket, byte[] data)
        {
            if (data == null || data.Length == 0 || socket == null)
                return 0;

            SocketError errorCode;
            return await Task.Factory.FromAsync<int>(
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, out errorCode, null, socket),
                socket.EndSend);
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

