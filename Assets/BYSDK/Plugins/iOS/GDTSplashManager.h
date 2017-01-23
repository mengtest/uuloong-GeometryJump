//
//  GDTSplashManager.h
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/2.
//
//

#import <Foundation/Foundation.h>
#import "GDTSplashAd.h"

@interface GDTSplashManager : NSObject<GDTSplashAdDelegate>

-(void)loadAndShowADWithAppkey:(NSString *)appKey placementId:(NSString *)placementId;
+ (GDTSplashManager *)sharedManger;

@end
