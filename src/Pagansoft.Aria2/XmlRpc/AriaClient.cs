using System;
using System.IO;
using System.Net;
using System.Xml;
using XmlRpcLight;
using XmlRpcLight.DataTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pagansoft.Aria2.XmlRpc
{
    public class AriaClient : IAria2c
    {
        private readonly XmlRpcSerializer _serializer;

        public AriaClient()
        {
            _serializer = new XmlRpcSerializer();
        }

        public Task<string> AddUri(string[] uris)
        {
            return InvokeMethod<string>("aria2.addUri", new object[] { uris });
        }

        public Task<string> AddUri(string[] uris, XmlRpcStruct options)
        {
            return InvokeMethod<string>("aria2.addUri", new object[] { uris, options });
        }

        public Task<string> AddUri(string[] uris, IDictionary<string, string> options, int position)
        {
            return InvokeMethod<string>("aria2.addUri", new object[] { uris, MakeXmlRpcStruct(options), position });
        }

        public Task<string> AddTorrent(byte[] torrent)
        {
            return InvokeMethod<string>("aria2.addTorrent", new object[] { torrent });
        }

        public Task<string> AddTorrent(byte[] torrent, string[] uris)
        {
            return InvokeMethod<string>("aria2.addTorrent", new object[] { torrent, uris });
        }

        public Task<string> AddTorrent(byte[] torrent, string[] uris, IDictionary<string, string> options)
        {
            return InvokeMethod<string>("aria2.addTorrent", new object[] { torrent, uris, MakeXmlRpcStruct(options) });
        }

        public Task<string> AddTorrent(byte[] torrent, string[] uris, IDictionary<string, string> options, int position)
        {
            return InvokeMethod<string>("aria2.addTorrent", new object[] {
                torrent,
                uris,
                MakeXmlRpcStruct(options),
                position
            });
        }

        public Task<string> AddMetalink(byte[] metalink)
        {
            return InvokeMethod<string>("aria2.addMetalink", new object[] { metalink });
        }

        public Task<string> AddMetalink(byte[] metalink, IDictionary<string, string> options)
        {
            return InvokeMethod<string>("aria2.addMetalink", new object[] { metalink, MakeXmlRpcStruct(options) });
        }

        public Task<string> AddMetalink(byte[] metalink, IDictionary<string, string> options, int position)
        {
            return InvokeMethod<string>("aria2.addMetalink", new object[] {
                metalink,
                MakeXmlRpcStruct(options),
                position
            });
        }

        public Task<string> Remove(string gid)
        {
            return InvokeMethod<string>("aria2.remove", new object[] { gid });
        }

        public Task<string> ForceRemove(string gid)
        {
            return InvokeMethod<string>("aria2.forceRemove", new object[] { gid });
        }

        public Task<string> Pause(string gid)
        {
            return InvokeMethod<string>("aria2.pause", new object[] { gid });
        }

        public Task<string> PauseAll()
        {
            return InvokeMethod<string>("aria2.pauseAll", new object[0]);
        }

        public Task<string> ForcePause(string gid)
        {
            return InvokeMethod<string>("aria2.forcePause", new object[] { gid });
        }

        public Task<string> ForcePauseAll()
        {
            return InvokeMethod<string>("aria2.forcePauseAll", new object[0]);
        }

        public Task<string> Unpause(string gid)
        {
            return InvokeMethod<string>("aria2.unpause", new object[] { gid });
        }

        public Task<string> UnpauseAll()
        {
            return InvokeMethod<string>("aria2.unpauseAll", new object[0]);
        }

        public Task<StatusResponse> TellStatus(string gid)
        {
            return InvokeMethod<StatusResponse>("aria2.tellStatus", new object[] { gid });
        }

        public Task<StatusResponse> TellStatus(string gid, string[] keys)
        {
            return InvokeMethod<StatusResponse>("aria2.tellStatus", new object[] { gid, keys });
        }

        public Task<UriResponse[]> GetUris(string gid)
        {
            return InvokeMethod<UriResponse[]>("aria2.getUris", new object[] { gid });
        }

        public Task<FileResponse[]> GetFiles(string gid)
        {
            return InvokeMethod<FileResponse[]>("aria2.getFiles", new object[] { gid });
        }

        public Task<PeerResponse[]> GetPeers(string gid)
        {
            return InvokeMethod<PeerResponse[]>("aria2.getPeers", new object[] { gid });
        }

        public Task<ServersResponse[]> GetServers(string gid)
        {
            return InvokeMethod<ServersResponse[]>("aria2.getServers", new object[] { gid });
        }

        public Task<StatusResponse[]> TellActive()
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellActive", new object[0]);
        }

        public Task<StatusResponse[]> TellActive(string[] keys)
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellActive", new object[] { keys });
        }

        public Task<StatusResponse[]> TellWaiting(int offset, int num)
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellWaiting", new object[] { offset, num });
        }

        public Task<StatusResponse[]> TellWaiting(int offset, int num, string[] keys)
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellWaiting", new object[] { offset, num, keys });
        }

        public Task<StatusResponse[]> TellStopped(int offset, int num)
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellStopped", new object[] { offset, num });
        }

        public Task<StatusResponse[]> TellStopped(int offset, int num, string[] keys)
        {
            return InvokeMethod<StatusResponse[]>("aria2.tellStopped", new object[] { offset, num, keys });
        }

        public Task<int> ChangePosition(string gid, int pos, string how)
        {
            return InvokeMethod<int>("aria2.changePosition", new object[] { gid, pos, how });
        }

        public Task<int[]> ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris)
        {
            return InvokeMethod<int[]>("aria2.changeUri", new object[] { gid, fileIndex, delUris, addUris });
        }

        public Task<int[]> ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris, int position)
        {
            return InvokeMethod<int[]>("aria2.changeUri", new object[] { gid, fileIndex, delUris, addUris, position });
        }

        public async Task<IDictionary<string, string>> GetOption(string gid)
        {
            var result = await InvokeMethod<XmlRpcStruct>("aria2.getOption", new object[] { gid });

            var ret = new Dictionary<string, string>();
            foreach (var key in result.Keys)
                ret.Add(key.ToString(), result[key].ToString());

            return ret;
        }

        public Task<string> ChangeOption(string gid, IDictionary<string, string> options)
        {
            return InvokeMethod<string>("aria2.changeOption", new object[] { gid, MakeXmlRpcStruct(options) });
        }

        public Task<Options> GetGlobalOption()
        {
            return InvokeMethod<Options>("aria2.getGlobalOption", new object[0]);
        }

        public Task<string> ChangeGlobalOption(Options options)
        {
            return InvokeMethod<string>("aria2.changeGlobalOption", new object[] { options });
        }

        public Task<GlobalStatResponse> GetGlobalStat()
        {
            return InvokeMethod<GlobalStatResponse>("aria2.getGlobalStat", new object[0]);
        }

        public Task<string> PurgeDownloadResult()
        {
            return InvokeMethod<string>("aria2.purgeDownloadResult", new object[0]);
        }

        public Task<string> RemoveDownloadResult(string gid)
        {
            return InvokeMethod<string>("aria2.removeDownloadResult", new object[] { gid });
        }

        public Task<SessionInfoResponse> GetSessionInfo()
        {
            return InvokeMethod<SessionInfoResponse>("aria2.getSessionInfo", new object[0]);
        }

        public Task<string> Shutdown()
        {
            return InvokeMethod<string>("aria2.shutdown", new object[0]);
        }

        public Task<string> ForceShutdown()
        {
            return InvokeMethod<string>("aria2.forceShutdown", new object[0]);
        }

        public Task<VersionResponse> GetVersion()
        {
            return InvokeMethod<VersionResponse>("aria2.getVersion", new object[0]);
        }

        #region Helpers

        private async Task<T> InvokeMethod<T>(string methodName, object[] args)
        {
            var request = new XmlRpcRequest(methodName, args);
            var http = CreateWebRequest();

            await SendRequest(request, http);
            return await GetResponse<T>(http);
        }

        private static HttpWebRequest CreateWebRequest()
        {
            var http = WebRequest.CreateHttp("http://localhost:6800/rpc");
            http.KeepAlive = false;
            http.Method = "POST";
            http.ContentType = "text/xml";
            http.AllowAutoRedirect = false;
            return http;
        }

        private Task SendRequest(XmlRpcRequest request, WebRequest http)
        {
            return Task.Run(() =>
            {
                using (var serialized = new MemoryStream())
                {
                    _serializer.SerializeRequest(serialized, request);
                    http.ContentLength = serialized.Length;
                    var outStream = http.GetRequestStream();
                    serialized.Seek(0, SeekOrigin.Begin);
                    serialized.CopyTo(outStream);
                    outStream.Close();
                }
            });
        }

        private async Task<TResult> GetResponse<TResult>(WebRequest http)
        {
            var response = await http.GetResponseAsync();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var xdoc = new XmlDocument();
                xdoc.LoadXml(reader.ReadToEnd());

                var xmlRpcResponse = _serializer.DeserializeResponse(xdoc, typeof(TResult));
                return (TResult)xmlRpcResponse.retVal;
            }
        }

        static XmlRpcStruct MakeXmlRpcStruct(IDictionary<string, string> options)
        {
            var o = new XmlRpcStruct();
            foreach (var key in options.Keys)
                o.Add(key, options[key]);
            return o;
        }

        #endregion
    }
}

