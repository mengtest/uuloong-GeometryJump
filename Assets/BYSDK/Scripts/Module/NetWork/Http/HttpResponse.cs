using System;

[Serializable]
class HttpResponse {
    private int _status;
    private string _data;

    public int status {
        get {
            return _status;
        } set {
            _status = value;
        }
    }

    public string data {
        get {
            return _data;
        } set {
            _data = value;
        }
    }
}

