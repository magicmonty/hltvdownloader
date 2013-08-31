using Pagansoft.Aria2.Options.Enums;

namespace Pagansoft.Aria2.Options
{
    /// <summary>
    /// Encapsulates FTP specific options
    /// </summary>
    public struct FtpOptions
    {
        /// <summary>
        /// Set FTP password. This affects all URIs. 
        /// If user name is embedded but password is missing in URI, 
        /// aria2 tries to resolve password using .netrc. 
        /// If password is found in .netrc, then use it as password. 
        /// If not, use the password specified in this option. 
        /// Default: ARIA2USER@
        /// </summary>
        public string Passwd;
        /// <summary>
        /// Use the passive mode in FTP. 
        /// If false is given, the active mode will be used. 
        /// Default: true
        /// </summary>
        public bool Pasv;
        /// <summary>
        /// Use this proxy server for FTP. To erase previously defined proxy, use "". 
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
        /// Reuse connection in FTP. 
        /// Default: true
        /// </summary>
        public bool ReuseConnection;
        /// <summary>
        /// Set FTP transfer type. TYPE is either binary or ascii. 
        /// Default: binary
        /// </summary>
        public FtpTransferTypeOption TransferType;
        /// <summary>
        /// Set FTP user. This affects all URIs. 
        /// Default: anonymous
        /// </summary>
        public string User;
    }
}
