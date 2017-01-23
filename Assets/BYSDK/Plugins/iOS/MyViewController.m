//
//  MyViewController.m
//  Unity-iPhone
//
//  Created by hanJianXin on 16/9/1.
//
//

#import "MyViewController.h"

@interface MyViewController ()

/**
 *  webview
 */
@property (nonatomic, strong)UIWebView *webView;

/**
 *  加载网址页面
 */
- (void)loadWebViewWithString: (NSString *)urlStr;

@end

@implementation MyViewController

/**
 *  视图加载
 */
- (void)viewDidLoad {
    [super viewDidLoad];
    
    //背景色
    self.view.backgroundColor = [UIColor grayColor];
    //标题显示当前网址
    self.title = _url;
    
    //添加导航栏返回按钮
    UIButton *returnBtn = [UIButton buttonWithType:UIButtonTypeRoundedRect];
    [returnBtn setTitle:@"返回" forState:UIControlStateNormal];
    [returnBtn sizeToFit];
    [returnBtn addTarget:self action:@selector(CloseWebView) forControlEvents:UIControlEventTouchUpInside];
    UIBarButtonItem *returnItem = [[UIBarButtonItem alloc]initWithCustomView:returnBtn];
    self.navigationItem.leftBarButtonItem = returnItem;
    
    //屏幕尺寸
    CGFloat ApplicationW = [[UIScreen mainScreen] bounds].size.width;
    CGFloat ApplicationH = [[UIScreen mainScreen] bounds].size.height;
    
    // webview
    _webView = [[UIWebView alloc] init];
    //frame
    [_webView setFrame:CGRectMake(2, 2, ApplicationW, ApplicationH - 2)];
    //自适应
    _webView.autoresizingMask = UIViewAutoresizingFlexibleHeight |UIViewAutoresizingFlexibleWidth;
    _webView.scalesPageToFit = YES;
    //设置webview的代理
    _webView.delegate = self;
    //添加到页面
    [self.view addSubview:_webView];
    //加载网页
    [self loadWebViewWithString:_url];
}

/**
 *  加载网址页面
 */
- (void)loadWebViewWithString:(NSString*)urlStr {
    // 网址字符串转URL
    NSURL *url = [NSURL URLWithString:urlStr];
    // url请求
    NSURLRequest *request = [NSURLRequest requestWithURL:url];
    // 加载url请求
    [_webView loadRequest:request];
}

/**
 *  关闭webview页面
 */
- (void)CloseWebView {
    [self.navigationController.view removeFromSuperview];
    [self.navigationController dismissViewControllerAnimated:YES completion:nil];
}

#pragma mark webview委托方法
/**
 *  开始加载
 */
- (void)webViewDidStartLoad:(UIWebView *)webView {
    NSLog(@"开始加载...");
}
/**
 *  加载完成
 */
- (void)webViewDidFinishLoad:(UIWebView *)webView {
    NSLog(@"加载完成...");
}
/**
 *  加载出错
 */
- (void)webView:(UIWebView *)webView didFailLoadWithError:(NSError *)error {
    NSLog(@"加载出错:%@",[error localizedDescription]);
}

@end
