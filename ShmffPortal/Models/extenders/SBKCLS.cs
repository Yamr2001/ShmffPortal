using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShmffPortal.Models.extenders
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {

        private EnvelopeBody bodyField;

        /// <remarks/>
        public EnvelopeBody Body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public partial class EnvelopeBody
    {

        private InqSbkResponse inqSbkResponseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
        public InqSbkResponse InqSbkResponse
        {
            get
            {
                return this.inqSbkResponseField;
            }
            set
            {
                this.inqSbkResponseField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class InqSbkResponse
    {

        private byte statusCodeField;

        private string[] sbkResultField;

        /// <remarks/>
        public byte StatusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SbkResult")]
        public string[] SbkResult
        {
            get
            {
                return this.sbkResultField;
            }
            set
            {
                this.sbkResultField = value;
            }
        }
    }


}