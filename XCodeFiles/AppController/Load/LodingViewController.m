//
//  LodingViewController.m
//  Unity-iPhone
//
//  Created by wangYuQi on 16/7/23.
//
//

#import "LodingViewController.h"

#import "BYSLoading.h"

@interface LodingViewController (){
    BYSLoading * _loding;
}

@end

@implementation LodingViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    
    self.view.backgroundColor = [UIColor blackColor];
    
    _loding = [[BYSLoading alloc]init];
    
    [_loding showInView:self.view];
    
    //检测用户的通知
    //[[NSNotificationCenter defaultCenter]addObserver:self selector:@selector(dismissHomeView) name:@"bysDismissHomeView" object:nil];
}

- (void) dealloc {
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

- (void) dismissHomeView{
    [_loding hide];
    [self dismissViewControllerAnimated:YES completion:nil];
}

@end
