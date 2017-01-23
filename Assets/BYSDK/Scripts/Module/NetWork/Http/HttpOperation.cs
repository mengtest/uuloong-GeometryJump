using System;
using UnityEngine;
using System.Collections;
using System.Text;
using Pathfinding.Serialization.JsonFx;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;

class HttpOperation {
    public enum HttpStatus {
        SUCCESS = 1
    }

    /// <summary>
    ///  Get方法
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="OnSuccess">成功回调接口</param>
    /// <param name="OnError">失败回调接口</param>
    public static void GetByHttp(MonoBehaviour sender,
                                 string url,
                                 Action<string> OnSuccess,
                                 Action<string> OnError) {
        Debug.Log("URL: " + url);
        HttpRequest<string> request = new HttpRequest<string>(url);
        request.OnSuccessCallBack += OnSuccess;
        request.OnErrorCallBack += OnError;
        sender.StartCoroutine(HttpOperation.Get<string>(request));
    }

    ///// <summary>
    /////  http post 方法
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="url">目标网地址</param>
    ///// <param name="jsonData">上传的数据</param>
    ///// <param name="OnSuccess">成功回调</param>
    ///// <param name="OnError">失败回调</param>
    //public static void PostByHttp<T>(MonoBehaviour sender,
    //                                 string url,
    //                                 List<string> data,
    //                                 Action<string> OnSuccess,
    //                                 Action<string> OnError,
    //                                 string json = "") {
    //    Debug.Log("URL: " + url);
    //    HttpRequest<string> request = new HttpRequest<string>(url);
    //    request.OnSuccessCallBack += OnSuccess;
    //    request.OnErrorCallBack += OnError;
    //    request.Body = json;
    //    sender.StartCoroutine(HttpOperation.Post<T>(request, data));
    //}

    /// <summary>
    /// http post 方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="url">url地址</param>
    /// <param name="data"></param>
    /// <param name="OnSuccess">成功回调</param>
    /// <param name="OnError">失败回调</param>
    public static void PostByHttp<T>(MonoBehaviour sender,
                                 string url,
                                 List<string> data,
                                 Action<string> OnSuccess,
                                 Action<string> OnError)
    {
        Debug.Log("URL: " + url);
        HttpRequest<string> request = new HttpRequest<string>(url);
        request.OnSuccessCallBack += OnSuccess;
        request.OnErrorCallBack += OnError;
        sender.StartCoroutine(HttpOperation.Post<T>(request, data));
    }

    /// <summary>
    /// http post 方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="url">url地址</param>
    /// <param name="json">json数据</param>
    /// <param name="OnSuccess">成功回调</param>
    /// <param name="OnError">失败回调</param>
    public static void PostByHttp<T>(MonoBehaviour sender,
                             string url,
                             string json,
                             Action<string> OnSuccess,
                             Action<string> OnError)
    {
        Debug.Log("URL: " + url);
        HttpRequest<string> request = new HttpRequest<string>(url);
        request.OnSuccessCallBack += OnSuccess;
        request.OnErrorCallBack += OnError;
        request.Body = json;
        sender.StartCoroutine(HttpOperation.Post<T>(request, null));
    }

    private static IEnumerator Post<T>(HttpRequest<string> HttpRequest, List<string> dataList = null) {
        WWWForm form = new WWWForm();
        HttpRequest.AddHeader("content-encoding", "gzip");

        if(dataList != null) {
            for( int i = 0; i < dataList.Count; i++ ) {
                form.AddField(i.ToString(), dataList[i]);
            }
        } else {
            form.AddField("data", HttpRequest.Body);
        }
        WWW www = new WWW(HttpRequest.Url,form);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
            if (HttpRequest.OnErrorCallBack != null) {
                HttpRequest.OnErrorCallBack(www.error);
            }
            www.Dispose();
        } else {
            Debug.Log(www.text);
            HttpResponse data = JsonReader.Deserialize<HttpResponse>(www.text);
            www.Dispose();
            if (data.status == (int)HttpStatus.SUCCESS) {
                if (HttpRequest.OnSuccessCallBack != null) {
                    HttpRequest.OnSuccessCallBack(data.data.ToString());
                }
            } else {
                if (HttpRequest.OnErrorCallBack != null) {
                    HttpRequest.OnErrorCallBack(data.data.ToString());
                }
            }
        }
    }

    private static IEnumerator Get<T>(HttpRequest<T> HttpRequest) {
        StringBuilder sb = new StringBuilder();
        sb.Append(HttpRequest.Url);
        sb.Append("\n");
        sb.Append(HttpRequest.Body);
        Debug.Log(sb.ToString());

        string body = HttpRequest.Body;
        byte[] bs = body == "" ?  UTF8Encoding.UTF8.GetBytes(body): null;
        WWW www = new WWW(HttpRequest.Url, bs, HttpRequest.Headers);
        yield return www;
        if (www.error != null) {
            Debug.Log(www.error);
            if (HttpRequest.OnErrorCallBack != null) {
                HttpRequest.OnErrorCallBack(www.error);
            }
            www.Dispose();
        } else {
            Debug.Log(www.text);
            HttpResponse data = JsonReader.Deserialize<HttpResponse>(www.text);
            www.Dispose();
            if (data.status == (int)HttpStatus.SUCCESS) {
                if (HttpRequest.OnSuccessCallBack != null) {
                    Debug.Log("data: " + data.data.ToString());
                    HttpRequest.OnSuccessCallBack(data.data.ToString());
                }
            } else {
                if (HttpRequest.OnErrorCallBack != null) {
                    HttpRequest.OnErrorCallBack(data.data.ToString());
                }
            }
        }
    }

    internal static T GetByHttp<T>(MonoBehaviour order, T sendPointByRuleURL, Action<T> festivalSuccess, Action<T> festivalFailed) {
        throw new NotImplementedException();
    }
}


