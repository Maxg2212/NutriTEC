namespace NutriTEC_API.Models
{
    //Model that allows to create a JSON to send information to the web app and then it can be read for the web app developers
    public class JSON_Object
    {
        public string status { get; set; } = string.Empty;
        public object result { get; set; }


        public JSON_Object(string status, object result)
        {
            this.status = status;
            this.result = result;
        }
    }
}
