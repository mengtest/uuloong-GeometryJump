//
//  NativeViewController.h
//  GDTMobSample
//
//  Created by GaoChao on 14/11/19.
//  Copyright (c) 2014年 Tencent. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "GDTNativeAd.h"

@interface NativeViewController : UIViewController<GDTNativeAdDelegate>

@property (unsafe_unretained, nonatomic) IBOutlet UITextView *resultTV;

@end
