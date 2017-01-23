using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/*********************************************************************//**
*	namespace	:	Assets.Scripts.Auth
*
*	describe	:	N/A
*	class name	:	AuthManage
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/26 16:37:04				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/


public class AuthManage {
    Auth _auth;
    Integral _integral;
    Friends _friends;


    public AuthManage(MonoBehaviour order) {
        Debug.Log(" new AuthManage ");
        _auth = new Auth(order);
        _integral = new Integral(order);
        _friends = new Friends(order);
    }
}
