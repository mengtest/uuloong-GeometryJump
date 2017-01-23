//
//  UnityWrapper.c
//  UMessage_Sdk_Demo
//
//  Created by zhu cong on 6/26/15.
//  Copyright (c) 2015 umeng.com. All rights reserved.
//


#import "UMessage.h"
#import "AppDelegateListener.h"

#import "UMPushHandler.h"








static NSString* CreateNSString (const char* string) {
    return [NSString stringWithUTF8String:(string ? string : "")];
}

static char* MakeHeapString(const char* string) {
    if (!string){
        return NULL;
    }
    char* mem = static_cast<char*>(malloc(strlen(string) + 1));
    if (mem) {
        strcpy(mem, string);
    }
    return mem;
}

typedef void (*TagHandler) (const char* response,int remain,const char* error);
typedef void (*AliasHandler) (const char* response,const char* error);

static NSArray* getTagsWithString(NSString *string)
{
    if(string == nil)
    {
        return nil;
    }
    
    NSArray *sepArray = [string componentsSeparatedByString:@","];
    
    __autoreleasing NSMutableArray *array = [[NSMutableArray alloc] initWithArray:sepArray];
    
    for(NSString *str in sepArray)
    {
        if([str length]==0)
        {
            [array removeObject:str];
        }
    }
    
    return array;
}

static NSString* NSSetToNSString(NSSet *set)
{
    if(set==NULL)
    {
        return NULL;
    }
   
    return [[set allObjects] componentsJoinedByString:@","];

}

static NSString* NSDictToNSString(NSDictionary *dict)
{
    if(dict==NULL)
    {
        return NULL;
    }
    
    NSError *error;
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict
                                                       options:NSJSONWritingPrettyPrinted // Pass 0 if you don't care about the readability of the generated string
                                                         error:&error];
    
    if (! jsonData) {
        NSLog(@"Got an error: %@", error);
        return NULL;
    } else {
        
        __autoreleasing NSString *jsonString = [[NSString alloc]initWithData:jsonData encoding:NSUTF8StringEncoding];
        
        return jsonString;
    }
    
}
UMPushHandler* handler;
extern "C"{
    
    void UMessageStartWithAppkeyForUnity(NSString *string,NSDictionary* launchOptions)
    {
//        //添加如下
//        //并将"your app key"替换成您在umeng后台获得的appkey
//        [UMessage startWithAppkey:string launchOptions:launchOptions];
//        
//#if __IPHONE_OS_VERSION_MAX_ALLOWED >= __IPHONE_8_0
//        if([[[UIDevice currentDevice] systemVersion] compare:@"8.0" options:NSNumericSearch] != NSOrderedAscending)
//        {
//            //register remoteNotification types
//            UIMutableUserNotificationAction *action1 = [[UIMutableUserNotificationAction alloc] init];
//            action1.identifier = @"action1_identifier";
//            action1.title=@"Accept";
//            action1.activationMode = UIUserNotificationActivationModeForeground;//当点击的时候启动程序
//            
//            UIMutableUserNotificationAction *action2 = [[UIMutableUserNotificationAction alloc] init];  //第二按钮
//            action2.identifier = @"action2_identifier";
//            action2.title=@"Reject";
//            action2.activationMode = UIUserNotificationActivationModeBackground;//当点击的时候不启动程序，在后台处理
//            action2.authenticationRequired = YES;//需要解锁才能处理，如果action.activationMode = UIUserNotificationActivationModeForeground;则这个属性被忽略；
//            action2.destructive = YES;
//            
//            UIMutableUserNotificationCategory *categorys = [[UIMutableUserNotificationCategory alloc] init];
//            categorys.identifier = @"category1";//这组动作的唯一标示
//            [categorys setActions:@[action1,action2] forContext:(UIUserNotificationActionContextDefault)];
//            
//            UIUserNotificationSettings *userSettings = [UIUserNotificationSettings settingsForTypes:UIUserNotificationTypeBadge|UIUserNotificationTypeSound|UIUserNotificationTypeAlert
//                                                                                         categories:[NSSet setWithObject:categorys]];
//            [UMessage registerRemoteNotificationAndUserNotificationSettings:userSettings];
//            
//        } else{
//            //register remoteNotification types
//            [UMessage registerForRemoteNotificationTypes:UIRemoteNotificationTypeBadge
//             |UIRemoteNotificationTypeSound
//             |UIRemoteNotificationTypeAlert];
//        }
//#else
//        
//        //register remoteNotification types
//        [UMessage registerForRemoteNotificationTypes:UIRemoteNotificationTypeBadge
//         |UIRemoteNotificationTypeSound
//         |UIRemoteNotificationTypeAlert];
//        
//        
//#endif
//        handler = [[UMPushHandler alloc] init];
//        UnityRegisterAppDelegateListener(handler);
        
        //设置 AppKey 及 LaunchOptions
        [UMessage startWithAppkey:@"57df56de67e58ec39d000019" launchOptions:launchOptions];
        
        //1.3.0版本开始简化初始化过程。如不需要交互式的通知，下面用下面一句话注册通知即可。
        [UMessage registerForRemoteNotifications];
        
        /**  如果你期望使用交互式(只有iOS 8.0及以上有)的通知，请参考下面注释部分的初始化代码
         //register remoteNotification types （iOS 8.0及其以上版本）
         UIMutableUserNotificationAction *action1 = [[UIMutableUserNotificationAction alloc] init];
         action1.identifier = @"action1_identifier";
         action1.title=@"Accept";
         action1.activationMode = UIUserNotificationActivationModeForeground;//当点击的时候启动程序
         
         UIMutableUserNotificationAction *action2 = [[UIMutableUserNotificationAction alloc] init];  //第二按钮
         action2.identifier = @"action2_identifier";
         action2.title=@"Reject";
         action2.activationMode = UIUserNotificationActivationModeBackground;//当点击的时候不启动程序，在后台处理
         action2.authenticationRequired = YES;//需要解锁才能处理，如果action.activationMode = UIUserNotificationActivationModeForeground;则这个属性被忽略；
         action2.destructive = YES;
         
         UIMutableUserNotificationCategory *actionCategory = [[UIMutableUserNotificationCategory alloc] init];
         actionCategory.identifier = @"category1";//这组动作的唯一标示
         [actionCategory setActions:@[action1,action2] forContext:(UIUserNotificationActionContextDefault)];
         
         NSSet *categories = [NSSet setWithObject:actionCategory];
         
         //如果默认使用角标，文字和声音全部打开，请用下面的方法
         [UMessage registerForRemoteNotifications:categories];
         
         //如果对角标，文字和声音的取舍，请用下面的方法
         //UIRemoteNotificationType types7 = UIRemoteNotificationTypeBadge|UIRemoteNotificationTypeAlert|UIRemoteNotificationTypeSound;
         //UIUserNotificationType types8 = UIUserNotificationTypeAlert|UIUserNotificationTypeSound|UIUserNotificationTypeBadge;
         //[UMessage registerForRemoteNotifications:categories withTypesForIos7:types7 withTypesForIos8:types8];
         */
        
        //for log
        [UMessage setLogEnabled:YES];
        
        
    }
    
    void _setLogEnabled(bool value)
    {
        [UMessage setLogEnabled:value];
    }
    
    void _setBadgeClear(bool value)
    {
        [UMessage setBadgeClear:value];
    }
    
    void _setAutoAlert(bool value)
    {
        [UMessage setAutoAlert:value];
    }
    
    void _setChannel(const char* channel)
    {
        [UMessage setChannel:CreateNSString(channel)];
    }
    
    
    void _unregisterForRemoteNotifications()
    {
        [UMessage unregisterForRemoteNotifications];
    }
    
   
    
    static NSString* NSError2NSString(NSError *error)
    {
        NSArray *infoList = @[@"kUMessageErrorUnknown,未知错误",
                              @"kUMessageErrorResponseErr,响应出错",
                              @"kUMessageErrorOperateErr,操作失败",
                              @"kUMessageErrorParamErr,参数非法",
                              @"kUMessageErrorDependsErr,条件不足(如:还未获取device_token，添加tag是不成功的)",
                              @"kUMessageErrorServerSetErr,服务器限定操作"];
        
        
        NSString* returnVal = error.localizedDescription;
        
        if ([error.domain isEqualToString: kUMessageErrorDomain] ) {
            
            returnVal = [NSString stringWithFormat:@"%@ %@",returnVal,infoList[error.code]];
        }
        return returnVal;
    }
    
    void _addTags(const char* tag,TagHandler handler)
    {
        
        [UMessage addTag:getTagsWithString(CreateNSString(tag))
                response:^(id responseObject, NSInteger remain, NSError *error) {
                    
                    handler(MakeHeapString([NSDictToNSString(responseObject) UTF8String]),(int)remain,[NSError2NSString(error) UTF8String]);
                    
                    
                }];
        
        
    }
    
    void _removeTags(const char* tag,TagHandler handler)
    {
        
        [UMessage removeTag:getTagsWithString(CreateNSString(tag))
                response:^(id responseObject, NSInteger remain, NSError *error) {
                    handler(MakeHeapString([NSDictToNSString(responseObject) UTF8String]),(int)remain,[NSError2NSString(error) UTF8String]);
                    
                    
                }];
        
        
    }
    void _getTags(TagHandler handler)
    {
        
        [UMessage getTags:
                ^(NSSet* responseObject, NSInteger remain, NSError *error) {
                    handler(MakeHeapString([NSSetToNSString(responseObject) UTF8String]),(int)remain,[NSError2NSString(error) UTF8String]);
                    
                    
                }];
        
    }
    
    void _removeAllTags(TagHandler handler)
    {
        
        [UMessage removeAllTags:
         ^(id responseObject, NSInteger remain, NSError *error) {
             handler(MakeHeapString([NSDictToNSString(responseObject) UTF8String]),(int)remain,[NSError2NSString(error) UTF8String]);
             
             
         }];
    }
    
    void _addAlias(const char* name,const char* type,AliasHandler handler)
    {
        
        [UMessage addAlias:CreateNSString(name) type:CreateNSString(type) response:
         ^(id responseObject, NSError *error) {
             handler(MakeHeapString([NSDictToNSString(responseObject) UTF8String]),[NSError2NSString(error) UTF8String]);
             
             
         }];
    }
    
    
    void _removeAlias(const char* name,const char* type,AliasHandler handler)
    {
        
        [UMessage removeAlias:CreateNSString(name) type:CreateNSString(type) response:
         ^(id responseObject, NSError *error) {
             handler(MakeHeapString([NSDictToNSString(responseObject) UTF8String]),[NSError2NSString(error) UTF8String]);
             
             
         }];
    }
}
