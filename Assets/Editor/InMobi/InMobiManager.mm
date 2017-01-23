//
//  InMobiManager.m
//  InMobiTest
//

#import "InMobiManager.h"
#import "IMSdk.h"



UIViewController *UnityGetGLViewController();
void UnityPause( int shouldPause );
void UnitySendMessage( const char * className, const char * methodName, const char * param );


@implementation InMobiManager

@synthesize adView;

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - NSObject and Class

+ (InMobiManager*)sharedManger
{
	static dispatch_once_t pred;
	static InMobiManager *_sharedInstance = nil;
	
	dispatch_once( &pred, ^{ _sharedInstance = [[self alloc] init]; } );
	return _sharedInstance;
}


+ (NSDictionary*)dictFromJSON:(NSString*)json
{
	if( !json )
		return nil;
	
	NSError *error = nil;
	id obj = [NSJSONSerialization JSONObjectWithData:[json dataUsingEncoding:NSUTF8StringEncoding] options:0 error:&error];
	
	if( error )
		NSLog( @"error parsing JSON: %@", error );
	
	return (NSDictionary*)obj;
}


+ (NSString*)JSONfromObject:(id)object
{
	if( [NSJSONSerialization isValidJSONObject:object] )
	{
		NSError *error = nil;
		NSData *data = [NSJSONSerialization dataWithJSONObject:object options:0 error:&error];
		
		if( error )
			NSLog( @"error serializing object to JSON: %@", error );
		
		return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
	}
	
	return @"{}";
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Private

- (void)adjustAdViewFrameToShowAdView:(IMBanner*)bannerView withPosition:(int)adPosition
{
	// fetch screen dimensions and useful values
	CGRect origFrame = bannerView.frame;
	
	CGFloat screenHeight = [UIScreen mainScreen].bounds.size.height;
	CGFloat screenWidth = [UIScreen mainScreen].bounds.size.width;
	
	if( UIInterfaceOrientationIsLandscape( UnityGetGLViewController().interfaceOrientation ) )
	{
		screenWidth = screenHeight;
		screenHeight = [UIScreen mainScreen].bounds.size.width;
	}
	
	
	switch( (InMobiAdPosition)adPosition )
	{
		case InMobiAdPositionTopLeft:
			origFrame.origin.x = 0;
			origFrame.origin.y = 0;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleRightMargin | UIViewAutoresizingFlexibleBottomMargin );
			break;
		case InMobiAdPositionTopCenter:
			origFrame.origin.x = ( screenWidth / 2 ) - ( origFrame.size.width / 2 );
			origFrame.origin.y = 0;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleRightMargin | UIViewAutoresizingFlexibleBottomMargin );
			break;
		case InMobiAdPositionTopRight:
			origFrame.origin.x = screenWidth - origFrame.size.width;
			origFrame.origin.y = 0;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleBottomMargin );
			break;
		case InMobiAdPositionCentered:
			origFrame.origin.x = ( screenWidth / 2 ) - ( origFrame.size.width / 2 );
			origFrame.origin.y = ( screenHeight / 2 ) - ( origFrame.size.height / 2 );
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleRightMargin | UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleTopMargin | UIViewAutoresizingFlexibleBottomMargin );
			break;
		case InMobiAdPositionBottomLeft:
			origFrame.origin.x = 0;
			origFrame.origin.y = screenHeight - origFrame.size.height;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleRightMargin | UIViewAutoresizingFlexibleTopMargin );
			break;
		case InMobiAdPositionBottomCenter:
			origFrame.origin.x = ( screenWidth / 2 ) - ( origFrame.size.width / 2 );
			origFrame.origin.y = screenHeight - origFrame.size.height;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleRightMargin | UIViewAutoresizingFlexibleTopMargin );
			break;
		case InMobiAdPositionBottomRight:
			origFrame.origin.x = screenWidth - self.adView.frame.size.width;
			origFrame.origin.y = screenHeight - origFrame.size.height;
			self.adView.autoresizingMask = ( UIViewAutoresizingFlexibleLeftMargin | UIViewAutoresizingFlexibleTopMargin );
			break;
	}
	
	bannerView.frame = origFrame;
}

///////////////////////////////////////////////////////////////////////////////////////////////////

- (void)inMobiInit:accountID requestParams:(NSDictionary *)requestParams
{
    [IMSdk initWithAccountID:accountID];
    
    NSNotificationCenter *nc = [NSNotificationCenter defaultCenter];
    [nc addObserver:self
           selector:@selector(interfaceOrientationChanged:)
               name:UIDeviceOrientationDidChangeNotification
             object:nil];
    
    if ([requestParams count] != 0) {
        
        NSInteger age = [[requestParams objectForKey:@"age"] integerValue];
        if (age) {
            [IMSdk setAge:age];
        }
        
        NSString *gender = [requestParams objectForKey:@"gender"];
        if (gender) {
            if ([gender isEqualToString:@"GENDER_FEMALE"]) {
                [IMSdk setGender:kIMSDKGenderFemale];
            } else if ([gender isEqualToString:@"GENDER_MALE"]) {
                [IMSdk setGender:kIMSDKGenderMale];
            }
        }
        
        NSString *education = [requestParams objectForKey:@"education"];
        if (education) {
            if ([education isEqualToString:@"EDUCATION_HIGHSCHOOLORLESS"]) {
                [IMSdk setEducation:kIMSDKEducationHighSchoolOrLess];
            } else if ([education isEqualToString:@"EDUCATION_COLLEGEORGRADUATE"]) {
                [IMSdk setEducation:kIMSDKEducationCollegeOrGraduate];
            } else if ([education isEqualToString:@"EDUCATION_POSTGRADUATEORABOVE"]) {
                [IMSdk setEducation:kIMSDKEducationPostGraduateOrAbove];
            }
        }
        
        NSString *ethnicity = [requestParams objectForKey:@"ethnicity"];
        if (ethnicity) {
            if ([ethnicity isEqualToString:@"ETHNICITY_ASIAN"]) {
                [IMSdk setEthnicity:kIMSDKEthnicityAsian];
            } else if ([ethnicity isEqualToString:@"ETHNICITY_HISPANIC"]) {
                [IMSdk setEthnicity:kIMSDKEthnicityHispanic];
            } else if ([ethnicity isEqualToString:@"ETHNICITY_AFRICAN_AMERICAN"]) {
                [IMSdk setEthnicity:kIMSDKEthnicityAfricanAmerican];
            } else if ([ethnicity isEqualToString:@"ETHNICITY_CAUCASIAN"]) {
                [IMSdk setEthnicity:kIMSDKEthnicityCaucasian];
            } else if ([ethnicity isEqualToString:@"ETHNICITY_OTHER"]) {
                [IMSdk setEthnicity:kIMSDKEthnicityOther];
            }
        }
        
        NSString *dob = [requestParams objectForKey:@"dob"];
        if (dob) {
            NSDateFormatter *dateFormat = [[NSDateFormatter alloc] init];
            [dateFormat setDateFormat:@"dd-mm-yyyy"];
            NSDate *date = [dateFormat dateFromString:dob];
            NSCalendar* calendar = [NSCalendar currentCalendar];
            NSDateComponents* components = [calendar components:NSYearCalendarUnit|NSMonthCalendarUnit|NSDayCalendarUnit fromDate:date]; // Get necessary date components

            [IMSdk setYearOfBirth:[components year] ];
        }
        
        unsigned int income = [[requestParams objectForKey:@"income"] unsignedIntValue];
        if (income) {
            [IMSdk setIncome:income];
        }
        
        NSString *language = [requestParams objectForKey:@"language"];
        if (language) {
            [IMSdk setLanguage:language];
        }
        
        NSString *postalCode = [requestParams objectForKey:@"postalCode"];
        if (postalCode) {
            [IMSdk setPostalCode:postalCode];
        }
        
        NSString *areaCode = [requestParams objectForKey:@"areaCode"];
        if (areaCode) {
            [IMSdk setAreaCode:areaCode];
        }
        
        NSString *interests = [requestParams objectForKey:@"interests"];
        if (interests) {
            [IMSdk setInterests:interests];
        }
        
        NSString *city = [requestParams objectForKey:@"city"];
        NSString *state = [requestParams objectForKey:@"state"];
        NSString *country = [requestParams objectForKey:@"country"];
        if (city && state && country) {
            [IMSdk setLocationWithCity:city state:state country:country];
        }
        
        NSString *sessionId = [requestParams objectForKey:@"sessionId"];
        if (sessionId) {
            [IMSdk addId:sessionId forType:kIMSDKIdTypeSession];
        }
        NSString *loginId = [requestParams objectForKey:@"loginId"];
        if (loginId) {
            [IMSdk addId:loginId forType:kIMSDKIdTypeLogin];
        }
        
        NSString *ageGroup = [requestParams objectForKey:@"ageGroup"];
        if (ageGroup) {
            if ([ageGroup isEqualToString:@"Below18"]) {
                [IMSdk setAgeGroup:kIMSDKAgeGroupBelow18];
            }
            else if ([ageGroup isEqualToString:@"Between18And20"]) {
                [IMSdk setAgeGroup:kIMSDKAgeGroupBetween18And20];
            }
            else if ([ageGroup isEqualToString:@"Between21And24"]){
                [IMSdk setAgeGroup:kIMSDKAgeGroupBetween21And24];
            }
            else if ([ageGroup isEqualToString:@"Between25And34"]){
                [IMSdk setAgeGroup:kIMSDKAgeGroupBetween25And34];
            }
            else if ([ageGroup isEqualToString:@"Between35To54"]){
                [IMSdk setAgeGroup:kIMSDKAgeGroupBetween35And54];
            }
            else if ([ageGroup isEqualToString:@"Above55"]){
                [IMSdk setAgeGroup:kIMSDKAgeGroupAbove55];
            }
        }
        
        NSString *houseHoldIncome = [requestParams objectForKey:@"houseHoldIncome"];
        if (houseHoldIncome) {
            if ([houseHoldIncome isEqualToString:@"Below5kUSD"]) {
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBelow5kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between5kAnd10kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetweek5kAnd10kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between10kAnd15kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween10kAnd15kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between15kAnd20kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween15kAnd20kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between20kAnd25kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween20kAnd25kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between25kAnd50kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween25kAnd50kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between50kAnd75kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween50kAnd75kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between75kAnd100kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween75kAnd100kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Between100kAnd150kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeBetween100kAnd150kUSD];
            }
            else if ([houseHoldIncome isEqualToString:@"Above150kUSD"]){
                [IMSdk setHouseholdIncome:kIMSDKHouseholdIncomeAbove150kUSD];
            }
        }
        
        float latitude = [[requestParams objectForKey:@"latitude"]doubleValue];
        float longitude = [[requestParams objectForKey:@"longitude"]doubleValue];
        
        CLLocation *location = [[CLLocation alloc]initWithLatitude:latitude longitude:longitude];
        [IMSdk setLocation:location];
        
    }
    self.bannerDict = [[NSMutableDictionary alloc] init];
    self.interstitialDict = [[NSMutableDictionary alloc] init];
}

- (void)interfaceOrientationChanged:(NSNotification *)notification {
    
    orientationChange = [UIApplication sharedApplication].statusBarOrientation;
    
    if (((requestOrientation == UIInterfaceOrientationLandscapeLeft) || (requestOrientation == UIInterfaceOrientationLandscapeRight)) && ((orientationChange == UIInterfaceOrientationLandscapeLeft) || (orientationChange == UIInterfaceOrientationLandscapeRight))){
        if (self.interstitialVisible) {
            self.interstitialVisible.delegate = nil;
            self.interstitialVisible= nil;
        }
    }
}

///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Actions (Interstitials)

-(void) loadInterstitial:(NSString *)interstitialKey placementId:(long long)placementId {
    IMInterstitial *interstitial = [[IMInterstitial alloc] initWithPlacementId:placementId];
    if( interstitial != nil)
    {
        interstitial.delegate=self;
        interstitial.extras=@{ @"tp": @"p_unity",
                                @"mk-carrier" : @"3.0.119.0"};
        [interstitial load];
        [self.interstitialDict setObject:interstitial forKey:interstitialKey];
        NSLog(@"Interstitial Object Created");
    }

}

-(bool) isInterstitialReady:(NSString*)interstitialKey
{
    IMInterstitial* interstitial = [self.interstitialDict objectForKey:interstitialKey];
    return interstitial.isReady;
}


- (void)presentInterstitial:(NSString*)interstitialKey
{
    IMInterstitial* interstitial = [self.interstitialDict objectForKey:interstitialKey];
    if( interstitial && interstitial.isReady )
    {
        NSLog(@"Interstitial will be shown");
        [interstitial showFromViewController:UnityGetGLViewController()];
        self.interstitialVisible=interstitial;
    }
    else
    {
        NSLog( @"no interstitial has been loaded" );
        self.interstitialVisible=nil;
    }
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - Banners

//create banner object respective to the bannerKey provided and add it to bannerDict for later reference.

-(void) createBanner:(NSDictionary *)bannerParams width:(int)width height:(int)height placementID:(long long)placementId{
    NSString *bannerkey = [bannerParams objectForKey:@"bannerKey"];
    if ([self.bannerDict objectForKey:bannerkey] == NULL) {
        IMBanner *banner = [[IMBanner alloc] initWithFrame:CGRectMake(0, 0, width, height) placementId:placementId ];
        if (banner != NULL) {
            NSMutableDictionary *bannerObject = [[NSMutableDictionary alloc] init];
            [bannerObject setObject:banner forKey:@"banner"];
            
            NSString *refreshInterval = [bannerParams objectForKey:@"refreshInterval"];
            if(refreshInterval!=NULL){
                [bannerObject setObject:refreshInterval forKey:@"refreshInterval"];
            }
            
            NSString *position = [bannerParams objectForKey:@"position"];
            if (position != NULL) {
                [bannerObject setObject:position forKey:@"position"];
            }

            NSString *keywords = [bannerParams objectForKey:@"keywords"];
            if (keywords != NULL) {
                [bannerObject setObject:keywords forKey:@"keywords"];
            }
            
            [self.bannerDict setObject:bannerObject forKey:bannerkey];
        }
    } else {
        
        NSLog(@"Banner Object already present with key %@",bannerkey);
        
    }
}
//load banner respective to bannerKey and if no banner object is found relative to bannerKey then log message.
- (void)loadBanner:(NSString*)bannerKey
{
    NSDictionary *bannerObject = [self.bannerDict objectForKey:bannerKey];
    if (bannerObject != NULL)
    {
        NSLog(@"Banner object is present in dictionary with key bannerKey");
        int positon;
        adView = (IMBanner*)[bannerObject objectForKey:@"banner"];
        adView.delegate = self;
        adView.extras = @{ @"tp": @"p_unity" ,
                           @"key" : @"value"};
        adView.keywords = [bannerObject objectForKey:@"keywords"];
        //adView.refTagKey = [bannerObject objectForKey:@"refTagKey"];
        //adView.refTagValue = [bannerObject objectForKey:@"refTagValue"];
        adView.refreshInterval = [[bannerObject objectForKey:@"refreshInterval"]integerValue];
        positon = [[bannerObject objectForKey:@"position"] intValue];
        NSLog(@"%d",positon);

        [UnityGetGLViewController().view addSubview:adView];
        [adView load];
        [self adjustAdViewFrameToShowAdView:adView withPosition:positon];
    }
    else
    {
        NSLog(@"No Banner has been created with bannerKey : %@",bannerKey);
    }
}


- (void)destroyBanner:(NSString*)bannerKey
{
    NSDictionary* bannerObject=[self.bannerDict objectForKey:bannerKey];
    if(bannerObject != nil)
    {
        IMBanner* bannerView = (IMBanner*)[bannerObject objectForKey:@"banner"];
        bannerView.delegate = nil;
        [bannerView removeFromSuperview];
        bannerView=nil;
        [self.bannerDict removeObjectForKey:bannerKey];
    }
    else
        NSLog(@"No object present in dictionary with key bannerKey");
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - IMInterstitialDelegate

- (void)interstitialDidFinishLoading:(IMInterstitial *)interstitial
{
    NSLog( @"[InMobi] interstitialDidReceiveAd" );
    UnitySendMessage( "InMobiManager", "interstitialDidReceiveAd", "" );
}

- (void)interstitial:(IMInterstitial *)interstitial didFailToPresentWithError:(IMRequestStatus *)error {
    NSLog( @"[InMobi] didFailToPresentWithError: %@", error );
    UnitySendMessage( "InMobiManager", "interstitialDidFailToPresentScreenWithError", error.localizedDescription.UTF8String );
}

- (void)interstitial:(IMInterstitial *)interstitial didFailToLoadWithError:(IMRequestStatus *)error {
    NSLog( @"[InMobi] didFailToLoadWithError: %@", error );
    UnitySendMessage( "InMobiManager", "interstitialDidFailToReceiveAdWithError", error.localizedDescription.UTF8String );
}

- (void)interstitialWillPresent:(IMInterstitial *)ad
{
	NSLog( @"[InMobi] interstitialWillPresentScreen" );
	UnityPause( 1 );
	UnitySendMessage( "InMobiManager", "interstitialWillPresentScreen", "" );
}

- (void)interstitialDidPresent:(IMInterstitial *)ad
{
    NSLog( @"[InMobi] interstitialDidPresentScreen" );
    UnityPause( 1 );
    UnitySendMessage( "InMobiManager", "interstitialDidPresentScreen", "" );
}

- (void)interstitialWillDismiss:(IMInterstitial *)ad
{
	NSLog( @"[InMobi] interstitialWillDismissScreen" );
	UnityPause( 0 );
	UnitySendMessage( "InMobiManager", "interstitialWillDismissScreen", "" );
}


- (void)interstitialDidDismiss:(IMInterstitial *)ad
{
	NSLog( @"[InMobi] interstitialDidDismissScreen" );
    self.interstitialVisible=nil;
	UnitySendMessage( "InMobiManager", "interstitialDidDismissScreen", "" );
}

- (void)userWillLeaveApplicationFromInterstitial:(IMInterstitial *)interstitial
{
    NSLog( @"[InMobi] userWillLeaveApplicationFromInterstitial" );
    UnitySendMessage( "InMobiManager", "interstitialWillLeaveApplication", "" );
}

- (void)interstitialDidInteract:(IMInterstitial *)ad withParams:(NSDictionary *)dictionary
{
	NSLog( @"[InMobi] interstitialDidInteract" );
	UnitySendMessage( "InMobiManager", "interstitialDidInteract", [InMobiManager JSONfromObject:dictionary].UTF8String );
}

- (void) interstitial:(IMInterstitial *)interstitial rewardActionCompletedWithRewards:(NSDictionary *)rewards
{
     NSLog(@"[InMobi] interstitial rewardActionCompletedWithRewards: %@", [rewards description]);
    UnitySendMessage( "InMobiManager", "interstitialRewardActionCompletedWithRewards", [InMobiManager JSONfromObject:rewards].UTF8String );
}


///////////////////////////////////////////////////////////////////////////////////////////////////
#pragma mark - IMBannerDelegate

- (void) bannerDidFinishLoading:(IMBanner *)banner
{
    NSLog( @"[InMobi] bannerDidReceiveAd" );
    UnitySendMessage( "InMobiManager", "bannerDidReceiveAd", "" );
}

- (void) banner:(IMBanner *)banner didFailToLoadWithError:(IMRequestStatus *)error {
    NSLog( @"[InMobi] didFailToReceiveAdWithError: %@", error );
    UnitySendMessage( "InMobiManager", "bannerDidFailToReceiveAdWithError", error.localizedDescription.UTF8String);
}

- (void)banner:(IMBanner *)banner didInteractWithParams:(NSDictionary *)params {
    NSLog( @"[InMobi] bannerDidInteract" );
    UnitySendMessage( "InMobiManager", "bannerDidInteract", [InMobiManager JSONfromObject:params].UTF8String );
}

- (void)bannerWillPresentScreen:(IMBanner *)banner
{
	NSLog( @"[InMobi] bannerWillPresentScreen" );
	UnityPause( 1 );
	UnitySendMessage( "InMobiManager", "bannerWillPresentScreen", "" );
}

- (void)bannerDidPresentScreen:(IMBanner *)banner
{
    NSLog( @"[InMobi] bannerDidPresentScreen" );
    UnityPause( 1 );
    UnitySendMessage( "InMobiManager", "bannerDidPresentScreen", "" );
}

- (void)bannerWillDismissScreen:(IMBanner *)banner
{
	NSLog( @"[InMobi] bannerWillDismissScreen" );
	UnityPause( 0 );
	UnitySendMessage( "InMobiManager", "bannerWillDismissScreen", "" );
}


- (void)bannerDidDismissScreen:(IMBanner *)banner
{
	NSLog( @"[InMobi] bannerDidDismissScreen" );
	UnitySendMessage( "InMobiManager", "bannerDidDismissScreen", "" );
}

- (void) userWillLeaveApplicationFromBanner:(IMBanner *)banner {
    NSLog( @"[InMobi] userWillLeaveApplicationFromBanner" );
    UnitySendMessage( "InMobiManager", "bannerWillLeaveApplication", "" );
}


- (void)banner:(IMBanner *)banner rewardActionCompletedWithRewards:(NSDictionary *)rewards {
    NSLog( @"[InMobi] banner rewardActionCompletedWithRewards" );
    UnitySendMessage( "InMobiManager", "bannerRewardActionCompletedWithRewards", [InMobiManager JSONfromObject:rewards].UTF8String );
}

/*
- (void) dealloc {
    self.interstitialVisible.delegate = nil;
    self.interstitialVisible= nil;
    self.adView.delegate = nil;
    self.adView = nil;
    [[NSNotificationCenter defaultCenter] removeObserver:self name:UIDeviceOrientationDidChangeNotification object:nil];
    [self.interstitialDict removeAllObjects];
    [self.bannerDict removeAllObjects];
    self.nativeAd = nil;
    self.nativeAd.delegate = nil;
}*/

@end