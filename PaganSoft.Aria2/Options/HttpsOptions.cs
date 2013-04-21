namespace PaganSoft.Aria2.Options
{
    /// <summary>
    /// Encapsulates HTTPS specific options
    /// </summary>
    public struct HttpsOptions
    {
        /// <summary>
        /// Use this proxy server for HTTPS. 
        /// To erase previously defined proxy, use "". 
        /// <seealso cref="Options.AllProxy"/> option. 
        /// This affects all URIs. 
        /// The is [http://][USER:PASSWORD@]HOST[:PORT]
        /// </summary>
        public string Proxy;

        /// <summary>
        /// Set password for <see cref="Proxy"/>> option.
        /// </summary>
        public string ProxyPasswd;

        /// <summary>
        /// Set user for <see cref="Proxy"/>> option.
        /// </summary>
        public string ProxyUser;
    }
}
