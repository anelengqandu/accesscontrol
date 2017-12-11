using System;


namespace ApiServices.Utils
{
    public class DBResult
    {
        public bool success = true;
        public bool threwError = false;
        public string titleText;
        public string descripText;
        public string status;
        public Object data;
        public DBResult() { }

        public DBResult(bool _success)
        {
            success = _success;
        }

        public DBResult(bool _success, string _titleText, string _descripText)
        {
            success = _success;
            titleText = _titleText;
            descripText = _descripText;
        }

        public DBResult(string _errorTitleText, string _descripText)
        {
            success = false;
            threwError = true;
            titleText = _errorTitleText;
            descripText = _descripText;
        }
        public DBResult(string _errorTitleText, string _descripText, Object _data)
        {
            success = false;
            threwError = true;
            titleText = _errorTitleText;
            descripText = _descripText;
            data = _data;
        }
    }
}