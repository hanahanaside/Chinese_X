#import "iPhone_target_Prefix.pch"
#import "UnityAppController.h"

// Lobi用にヘッダをインポートする
#if UNITY_VERSION < 450
#include "iPhone_View.h"
#endif

#define LOBI_CHAT
#define LOBI_REC

#import <LobiCore/LobiCore.h>
#include "../Libraries/LobiCoreBridge.h"

#ifdef LOBI_CHAT
#import <LobiChat/LobiChat.h>
#include "../Libraries/LobiChatBridge.h"
#endif

#ifdef LOBI_REC
//#include "../Libraries/LobiRecBridge.h"
#endif

void UnityPause(bool pause);

#ifdef LOBI_CHAT
@interface LobiUnityAppController : UnityAppController < LobiChatAppLinkDelegate >
#else
@interface LobiUnityAppController : UnityAppController
#endif

+(void)load;

@end

@implementation LobiUnityAppController

+(void)load
{
    extern const char* AppControllerClassName;
    AppControllerClassName = "LobiUnityAppController";
}

- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
    BOOL ret = [super application:application didFinishLaunchingWithOptions:launchOptions];
    
    // UnityのViewControllerを取得する
    LobiCore_set_root_view_controller_func(UnityGetGLViewController);
    
    #ifdef LOBI_REC
    // UnityPause関数を取得する
//    LobiRec_set_unity_pause_func(UnityPause);
    
    // GLViewを設定する
//    LobiRec_set_unity_gl_view(UnityGetGLView);
    #endif
    
    #ifdef LOBI_CHAT
    // AppLink からデータの取得を行う
    [LobiChat setAppLinkDelegate:self];
    #endif
    
    // 初期化を行う
    [LobiCore setupClientId:@""
            accountBaseName:@""];
    
#ifdef LOBI_CHAT
    // app link 起動時に - (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation が呼ばれるように
    return YES;    
#else
    return ret;
#endif
}

- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation
{
    BOOL ret = [super application:application
                          openURL:url
                sourceApplication:sourceApplication
                       annotation:annotation];
    
    if ([LobiCore handleOpenURL:url]) {
        return YES;
    }
    
    return ret;
}

#ifdef LOBI_CHAT
#pragma mark - LobiChatAppLinkDelegate

- (void)onAppLink:(NSString *)value
{
    LobiChat_set_app_link_listener_message(value);
}
#endif

@end
