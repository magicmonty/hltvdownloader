namespace Pagansoft.Aria2.Options
{
    /// <summary>
    /// Encapsulates HTTPS specific options
    /// </summary>
    public class HttpsOptions : IHttpsOptions
    {
        /// <summary>
        /// Use this proxy server for HTTPS. 
        /// To erase previously defined proxy, use "". 
        /// <seealso cref="Options.AllProxy"/> option. 
        /// This affects all URIs. 
        /// The is [http://][USER:PASSWORD@]HOST[:PORT]
        /// </summary>
        public string Proxy { get; set; }

        /// <summary>
        /// Set password for <see cref="Proxy"/>> option.
        /// </summary>
        public string ProxyPasswd { get; set; }

        /// <summary>
        /// Set user for <see cref="Proxy"/>> option.
        /// </summary>
        public string ProxyUser { get; set; }
    }
}
