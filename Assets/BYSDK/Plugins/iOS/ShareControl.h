//
//  ShareControl.h
//  share
//
//  Created by wangYuQi on 16/6/12.
//  Copyright © 2016年 陕西百益软件有限公司. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <UIKit/UIKit.h>

@interface ShareControl : NSObject

/******************************************
* 调用iOS系统分享						  *
* text: string 分享的内容				  *
* url:	下载地址						  *
*******************************************/
void start(void * text, void * url);

/******************************************
* 调用邮件发送功能						  *
* Email: string 邮箱地址				  *
*******************************************/
void startEmail(void * emailstr);

/******************************************
* 进入第一个场景后，发送消息关闭遮罩层	  *
*******************************************/
void hideSplash();
@end
