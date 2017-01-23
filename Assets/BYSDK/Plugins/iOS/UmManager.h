//
//  UmManager.h
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/20.
//
//

#define  TestAppKey     @"57df56de67e58ec39d000019"

#import <Foundation/Foundation.h>
#import "UMessage.h"

@interface UmManager : NSObject

+ (void)StartWithAppKey:(NSString *)appKey launchOptions:(NSDictionary *)launchOptions;

+ (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo;


@end
