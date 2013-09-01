using Pagansoft.Aria2.Options.Enums;

namespace Pagansoft.Aria2.Options
{
    public class MetaLinkOptions : IMetaLinkOptions
    {
        /// <summary>
        /// Specify base URI to resolve relative URI in metalink:url 
        /// and metalink:metaurl element in a metalink file stored in local disk. 
        /// If URI points to a directory, URI must end with /.
        /// </summary>
        public string BaseUri { get; set; }

        /// <summary>
        /// If true is given and several protocols are available for a mirror in a 
        /// metalink file, aria2 uses one of them. 
        /// Use <see cref="PreferredProtocol"/> option to specify the preference of 
        /// protocol. 
        /// Default: true
        /// </summary>
        public bool EnableUniqueProtocol { get; set; }

        /// <summary>
        /// The language of the file to download.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The location of the preferred server. 
        /// A comma-delimited list of locations is acceptable, 
        /// for example, jp,us.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The operating system of the file to download.
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Specify preferred protocol. 
        /// The possible values are Http, Https, Ftp and None. 
        /// Specify none to disable this feature. 
        /// Default: None
        /// </summary>
        public ProtocolOption PreferredProtocol { get; set; }

        /// <summary>
        /// The version of the file to download.
        /// </summary>
        public string Version { get; set; }
    }
}
