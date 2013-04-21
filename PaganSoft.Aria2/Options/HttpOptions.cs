namespace PaganSoft.Aria2.Options
{
    /// <summary>
    /// Encapsulated HTTP specific options
    /// </summary>
    public struct HttpOptions
    {
        /// <summary>
        /// Send Accept: deflate, gzip request header and inflate response 
        /// if remote server responds with Content-Encoding: gzip 
        /// or Content-Encoding: deflate. 
        /// Default: false
        /// </summary>
        public bool AcceptGzip;

        /// <summary>
        /// Send HTTP authorization header only when it is requested by the server. 
        /// If false is set, then authorization header is always sent to the server. 
        /// There is an exception: if username and password are embedded in URI, 
        /// authorization header is always sent to the server regardless of this option. 
        /// Default: false
        /// </summary>
        public bool AuthChallenge;

        /// <summary>
        /// Send Cache-Control: no-cache and Pragma: no-cache header to avoid cached 
        /// content. 
        /// If false is given, these headers are not sent and you can add 
        /// Cache-Control header with a directive you like 
        /// using <see cref="Options.Header"/> option. 
        /// Default: false
        /// </summary>
        public bool NoCache;

        /// <summary>
        /// Set HTTP password. This affects all URIs.
        /// </summary>
        public string Passwd;

        /// <summary>
        /// Use this proxy server for HTTP. 
        /// To erase previously defined proxy, use "". 
        /// <seealso cref="Options.AllProxy"/> option. 
        /// This affects all URIs. 
        /// The format is [http://][USER:PASSWORD@]HOST[:PORT]
        /// </summary>
        public string Proxy;

        /// <summary>
        /// Set password for <see cref="Proxy"/> option.
        /// </summary>
        public string ProxyPasswd;

        /// <summary>
        /// Set user for <see cref="Proxy"/> option.
        /// </summary>
        public string ProxyUser;

        /// <summary>
        /// Set HTTP user. This affects all URIs.
        /// </summary>
        public string User;
    }
}
