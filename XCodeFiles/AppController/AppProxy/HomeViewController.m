//
//  HomeViewController.m
//  Unity-iPhone
//
//  Created by Forever.H on 15/12/23.
//
//

#import "HomeViewController.h"

#import "BYSLoading.h"

#import "UnitySubAppDelegate.h"

#import "MyDataManager.h"


@interface HomeViewController (){
    BYSLoading * _loding;
    int _num;
    NSTimer * _timer;
}
@end

@implementation HomeViewController

//- (void)viewDidLoad {
//    [super viewDidLoad];
//    // Do any additional setup after loading the view from its nib.
//}
//
//- (void)didReceiveMemoryWarning {
//    [super didReceiveMemoryWarning];
//    // Dispose of any resources that can be recreated.
//}
//
///*
//#pragma mark - Navigation
//
//// In a storyboard-based application, you will often want to do a little preparation before navigation
//- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
//    // Get the new view controller using [segue destinationViewController].
//    // Pass the selected object to the new view controller.
//}
//*/
////
//- (IBAction)jumpToEnterUnityVC:(id)sender {
//    EnterUnityViewController *enterVC = [[EnterUnityViewController alloc]init];
//    [self presentViewController:enterVC animated:nil completion:nil];
//}

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view from its nib.
    
    self.view.backgroundColor = [UIColor blackColor];
    
    _num = 0;
    
    _loding = [[BYSLoading alloc]init];
    
    [_loding showInView:self.view];
   
    [self start];
    
    CGFloat width = [UIScreen mainScreen].bounds.size.width;
    CGFloat height = [UIScreen mainScreen].bounds.size.height;
    
    UIImageView * imageView = [[UIImageView alloc]initWithImage:[UIImage imageNamed:@"loadingTitle"]];
    imageView.frame = CGRectMake(0, 0, width/1334*163, height/755*32);
    imageView.center = CGPointMake(width/2.0 + width/1334*13.5, height*0.4685);
    [self.view addSubview:imageView];
}


- (void)start{
   _timer = [NSTimer scheduledTimerWithTimeInterval:1.0f
                                                       target:self
                                                     selector:@selector(play)
                                                     userInfo:nil
                                                      repeats:YES];
    [_timer fire];
}

- (void)play{
    if (_num == 1) {
        [_timer invalidate];
        [MyDataManager sharedManager].isInMyHomeView = true;
        
        if([MyDataManager sharedManager].isRestartInUnity)
        {
            //判断是否是第一次启动unity
            
            [MyDataManager sharedManager].myWindow.rootViewController = [MyDataManager sharedManager].unityViewController; //将存放在单例中的unityViewController变成当前的rootVC
            [[MyDataManager sharedManager].myWindow bringSubviewToFront: [MyDataManager sharedManager].unityViewController.view];//将unityVC放到window的最上面
            [[[UnityGetMainWindow() rootViewController] view]setHidden:NO]; //让unity的界面显示出来
            [UnityGetMainWindow() makeKeyAndVisible];
            UnityPause(false);
        }
        else
        {
            //如果是第一次启动unity，需要添加返回键
            
            [MyDataManager sharedManager].isRestartInUnity = true;
            [[MyDataManager sharedManager].myAppDelegate startUnity:UIApplication.sharedApplication];//启动unity
        }
    }else {
        _num++;
    }
    
    
}

@end
