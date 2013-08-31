using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    public struct FileResponse
    {
        [XmlRpcMember("index")]
        public int
            Index;
        [XmlRpcMember("path")]
        public string
            Path;
        [XmlRpcMember("length")]
        public string
            Length;
        [XmlRpcMember("completedLength")]
        public string
            CompletedLength;
        [XmlRpcMember("selected")]
        public string
            Selected;
        [XmlRpcMember("uris")]
        public UriResponse[]
            Uris;
    }
}
