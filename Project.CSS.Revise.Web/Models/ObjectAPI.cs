using System.Xml.Serialization;
using static QRCoder.PayloadGenerator;

namespace Project.CSS.Revise.Web.Models
{
    public class ObjectAPI<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string JobUId { get; set; }
        private T _data;
        public T Data
        {
            get
            {
                if (_data == null)
                {
                    _data = GetObject();
                }
                return _data;
            }
            set { _data = value; }
        }

        private T _errors;
        public T Errors
        {
            get
            {
                if (_data == null)
                {
                    _data = GetObject();
                }
                return _data;
            }
            set { _data = value; }
        }

        protected T GetObject(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        public sms sms { get; set; }
    }

    [XmlRoot(ElementName = "sms")]
    public class sms
    {
        [XmlElement(ElementName = "status")]
        public status status { get; set; }
    }

    public class status
    {
        [XmlElement("detail")]
        public string detail { get; set; }
        [XmlElement("description")]
        public string description { get; set; }
    }

    public class Response
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
