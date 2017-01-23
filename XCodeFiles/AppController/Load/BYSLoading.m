//
//  BYSLoading.m
//  OrangeCard
//
//  Created by hanJianXin on 15/9/30.
//  Copyright (c) 2015年 陕西百益软件有限公司. All rights reserved.
//

#import "BYSLoading.h"

@implementation BYSLoading

- (id)initWithFrame:(CGRect)frame{
    self = [super initWithFrame:frame];
    if (self) {
        
        NSArray *gifArray = [NSArray arrayWithObjects:
                             [UIImage imageNamed:@"icon_load1.png"],
                             [UIImage imageNamed:@"icon_load2.png"],
                             [UIImage imageNamed:@"icon_load3.png"],
                             [UIImage imageNamed:@"icon_load4.png"],
                             [UIImage imageNamed:@"icon_load5.png"],
                             [UIImage imageNamed:@"icon_load6.png"],
                             [UIImage imageNamed:@"icon_load7.png"],
                             [UIImage imageNamed:@"icon_load8.png"],
                             [UIImage imageNamed:@"icon_load9.png"],
                             [UIImage imageNamed:@"icon_load10.png"],
                             [UIImage imageNamed:@"icon_load11.png"],
                             [UIImage imageNamed:@"icon_load12.png"],nil];
        self.animationImages = gifArray; //动画图片数组
        self.animationDuration = 1; //执行一次完整动画所需的时长
        self.animationRepeatCount = 0;  //动画重复次数,无限重复
        UIImage *image = [gifArray objectAtIndex:0];
        NSInteger width = MIN([UIScreen mainScreen].bounds.size.width, [UIScreen mainScreen].bounds.size.height)/755*78;
        self.frame = CGRectMake(self.frame.origin.x, self.frame.origin.y, width, width);
    }
    return self;
}

- (void)showInView:(UIView *)view{
      self.center = CGPointMake([UIScreen mainScreen].bounds.size.width/2.0, [UIScreen mainScreen].bounds.size.height*0.375);
    [view addSubview:self];
    [self startAnimating];
}

- (void)hide{
    [self stopAnimating];
    [self removeFromSuperview];
}


/*
// Only override drawRect: if you perform custom drawing.
// An empty implementation adversely affects performance during animation.
- (void)drawRect:(CGRect)rect {
    // Drawing code
}
*/

@end
