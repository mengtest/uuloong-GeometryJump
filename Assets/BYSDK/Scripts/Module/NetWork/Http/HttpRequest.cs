using System;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class HttpRequest<T> {
    private string _url;
    private Dictionary<string, string> _headers;
    private string _body;
    private Action<string>  _OnSuccessCallBack;
    private Action<string> _OnErrorCallBack;
    string GameResourcesID = "Q1N5M41ADHMJSHAKORY2U7Q1RCEXCGA2";
    public HttpRequest(string url) {
        _headers = new Dictionary<string, string>();
        _headers.Add("Content-Type", "application/json");

        string language = "en";
        SystemLanguage curLan = Application.systemLanguage;
        if (curLan == SystemLanguage.Chinese ||
                curLan == SystemLanguage.ChineseSimplified||
                curLan == SystemLanguage.ChineseTraditional) {
            language = "zh-CN";
        }

        _headers.Add("Accept-Language", language);
        //  _headers.Add("X-BYGame-Application-Token", "Q1N5M41ADHMJSHAKORY2U7Q1RCEXCGA2");
        _url = url;
    }

    public HttpRequest() {
        _headers = new Dictionary<string, string>();
        _headers.Add("Content-Type", "application/json");

        string language = "en";
        SystemLanguage curLan = Application.systemLanguage;
        if (curLan == SystemLanguage.Chinese ||
                curLan == SystemLanguage.ChineseSimplified ||
                curLan == SystemLanguage.ChineseTraditional) {
            language = "zh-CN";
        }
        _headers.Add("Accept-Language", language);
        _headers.Add("X-BYGame-Application-Token", GameResourcesID);
    }
    public void AddHeader(string key, string value) {
        _headers.Add(key, value);
    }

    public string Url {
        get {
            return _url;
        } set {
            _url = value;
        }
    }

    public Dictionary<string, string> Headers {
        get {
            return _headers;
        }
    }


    public string Body {
        get {
            return _body;
        } set {
            _body = value;
        }
    }

    public Action<string> OnSuccessCallBack {
        get {
            return _OnSuccessCallBack;
        } set {
            _OnSuccessCallBack = value;
        }
    }

    public Action<string> OnErrorCallBack {
        get {
            return _OnErrorCallBack;
        } set {
            _OnErrorCallBack = value;
        }
    }

}