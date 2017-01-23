//
//  ShareControl.m
//  share
//
//  Created by wangYuQi on 16/6/12.
//  Copyright © 2016年 陕西百益软件有限公司. All rights reserved.
//

#import "ShareControl.h"

//#import "AppDelegate.h"

#import <Social/Social.h>

@implementation ShareControl

void start(void * p, void * url){
    NSArray *activityItems;
    NSString * str = [NSString stringWithUTF8String:p];
    NSURL * _url = [NSURL URLWithString:[NSString stringWithUTF8String:url]];
    UIImage *image = nil;//[UIImage imageNamed:@"ShareLogo.png"];
    if (image != nil) {
        activityItems = @[str, _url, image];
    } else {
        activityItems = @[str, _url];
    }
    UIWindow * window = [UIApplication sharedApplication].keyWindow;
    UIActivityViewController *activityController =
    
    [[UIActivityViewController alloc] initWithActivityItems:activityItems  applicationActivities:nil];
    
    if ([activityController respondsToSelector:@selector(popoverPresentationController)])
    {
        activityController.modalPresentationStyle = UIModalPresentationPopover;
        [activityController setPreferredContentSize:CGSizeMake(248.0,216.0)];
        
        UIPopoverPresentationController *presentationController = activityController.popoverPresentationController;
        presentationController.sourceView = window.rootViewController.view;
        presentationController.permittedArrowDirections = UIPopoverArrowDirectionAny;
        
        CGFloat x = ([UIScreen mainScreen].bounds.size.width - 200)/2;
        CGFloat y = ([UIScreen mainScreen].bounds.size.height - 850)/2;
        
        presentationController.sourceRect = CGRectMake(x, y, 200, 200);
        //[presentationController presentPopoverFromRect:position inView:view permittedArrowDirections:UIPopoverArrowDirectionUp | UIPopoverArrowDirectionDown animated:YES];
        //NSLog(@"%@",presentationController.sourceRect);
    }
    if ([[[UIDevice currentDevice] systemVersion] floatValue] >= 8.0) {
        // iOS 5 code
        [activityController setCompletionWithItemsHandler:^(NSString *activityType, BOOL completed, NSArray *returnedItems, NSError * error){
            if (completed ) {
                NSLog(@"true");
                UnitySendMessage("ShareListener","OnIosShareCallBack","true");
            }else{
                NSLog(@"false");
                UnitySendMessage("ShareListener","OnIosShareCallBack","false");
            }
            
        }];
    }else{
        [activityController setCompletionHandler:^(NSString *activityType,BOOL complete){
            if (complete) {
                NSLog(@"true");
                UnitySendMessage("ShareListener","OnIosShareCallBack","true");
            }else{
                NSLog(@"false");
                UnitySendMessage("ShareListener","OnIosShareCallBack","false");
            }
        }];
    }
    [window.rootViewController presentViewController:activityController  animated:YES completion:nil];
}

void startEmail(void * string) {
    NSString * str = [NSString stringWithUTF8String:string];
    NSLog(@"%@",str);
    NSMutableString * mailUrl = [[NSMutableString alloc]init];
    //添加收件人
    NSArray *toRecipients = [NSArray arrayWithObject: str];
    [mailUrl appendFormat:@"mailto:%@", [toRecipients componentsJoinedByString:@","]];
    //添加抄送
    //NSArray *ccRecipients = [NSArray arrayWithObjects:@"second@example.com", @"third@example.com", nil];
    //[mailUrl appendFormat:@"?cc=%@", [ccRecipients componentsJoinedByString:@","]];
    //添加密送
    //NSArray *bccRecipients = [NSArray arrayWithObjects:@"fourth@example.com", nil];
    //[mailUrl appendFormat:@"&bcc=%@", [bccRecipients componentsJoinedByString:@","]];
    //添加主题
    //[mailUrl appendString:@"&subject=my email"];
    //添加邮件内容
    //[mailUrl appendString:@"&body=<b>email</b> body!"];
    NSString* email = [mailUrl stringByAddingPercentEscapesUsingEncoding: NSUTF8StringEncoding];
    [[UIApplication sharedApplication] openURL: [NSURL URLWithString:email]];
}

void hideSplash()
{
	[[NSNotificationCenter defaultCenter] postNotificationName:@"bysDismissHomeView" object:nil]; 
}

@end
