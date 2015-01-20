//
//  AppDelegate.m
//  push_mock
//
//  Created by rudo on 2013/06/06.
//  Copyright (c) 2013年 Unicon PTE Ltd. All rights reserved.
//

#import "FelloPushBridge.h"
#import <FelloPush/KonectNotificationsAPI.h>

#if UNITY_VERSION >= 430
// Unity4.3以上ならば下記の対応は不要
#else
#error ※Unity4.3未満では下記の変更をAppController.mmに必ず行って下さい！
#error ※変更を行ったら本警告を削除してからビルドして下さい
#import <FelloPush/KonectNotificationsAPI.h>

// - (void)application:(UIApplication*)application didReceiveRemoteNotification:(NSDictionary*)userInfo
// {
//  UnitySendRemoteNotification(userInfo);
// 上記のコードの後に下記の1行を追加
[KonectNotificationsAPI processNotifications:userInfo];

//- (void)application:(UIApplication*)application didReceiveLocalNotification:(UILocalNotification*)notification
//{
//  UnitySendLocalNotification(notification);
// 上記のコードの後に下記の1行を追加
[KonectNotificationsAPI processLocalNotifications:notification];
#endif

static NSString* _appId = (アプリID);
// 例: static NSString* appId = @"10000";

void _FelloPushBridgePlugin_SetDeviceId(const char* deviceId)
{
    NSString* strDeviceId = [[[NSString alloc] initWithUTF8String:deviceId] autorelease];
    [KonectNotificationsAPI setupNotificationsWithString:strDeviceId];
}

void _FelloPushBridgePlugin_ScheduleLocalNotification(const char* text, int64_t date, const char* label)
{
    NSString* strText = [[[NSString alloc] initWithUTF8String:text] autorelease];
    NSString* strLabel = [[[NSString alloc] initWithUTF8String:label] autorelease];
    [KonectNotificationsAPI scheduleLocalNotification:strText at:[NSDate dateWithTimeIntervalSince1970:date] label:strLabel];
}

void _FelloPushBridgePlugin_CancelLocalNotification(const char* label)
{
    NSString* strLabel = [[[NSString alloc] initWithUTF8String:label] autorelease];
    [KonectNotificationsAPI cancelLocalNotification:strLabel];
}

void _FelloPushBridgePlugin_SetTagString(const char* name, const char* value)
{
    NSString* nameString = [[[NSString alloc] initWithUTF8String:name] autorelease];
    NSString* valueString = [[[NSString alloc] initWithUTF8String:value] autorelease];
    [KonectNotificationsAPI setTag:nameString withStringValue:valueString];
}

void _FelloPushBridgePlugin_SetTagInt(const char* name, int value)
{
    NSString* nameString = [[[NSString alloc] initWithUTF8String:name] autorelease];
    [KonectNotificationsAPI setTag:nameString withIntValue:value];
}

@implementation FelloPushBridgeCallback

+ (void)load
{
    [[NSNotificationCenter defaultCenter] addObserver:[FelloPushBridgeCallback self]
                                             selector:@selector(didFinishLaunchingWithOptions:)
                                                 name:UIApplicationDidFinishLaunchingNotification
                                               object:nil];
    
#if UNITY_VERSION >= 430
    UnityRegisterAppDelegateListener(self);
#endif
}

#if UNITY_VERSION >= 430
+ (void)didReceiveRemoteNotification:(NSNotification*)notification
{
    [KonectNotificationsAPI processNotifications:notification.userInfo];
}

+ (void)didReceiveLocalNotification:(NSNotification*)notification
{
    NSObject* obj = notification.userInfo;
    if ( [obj isKindOfClass:[UILocalNotification self]] )
    {
        [KonectNotificationsAPI processLocalNotifications:(UILocalNotification*)obj];
    }
}
#endif

+ (void)didFinishLaunchingWithOptions:(NSNotification*)notification
{
    NSDictionary *launchOptions = [notification userInfo];
    [KonectNotificationsAPI setRootView:UnityGetGLViewController()];
    [KonectNotificationsAPI initialize:[[[FelloPushBridgeCallback alloc] init] autorelease]
                         launchOptions:launchOptions
                                 appId:_appId];
}

- (void)onLaunchFromNotification:(NSString *)notificationsId message:(NSString *)message extra:(NSDictionary *)extra
{
    NSMutableDictionary* param = [NSMutableDictionary dictionary];
    [param setValue:notificationsId forKey:@"id"];
    [param setValue:message forKey:@"message"];
    if ( extra != nil )
    {
        [param setValue:extra forKey:@"extra"];
    }
    
    NSString* paramJSON = [KonectNotificationsAPI JSONToString:param];
    UnitySendMessage("FelloPush", "onLaunchFromNotification", paramJSON.UTF8String);
}

@end