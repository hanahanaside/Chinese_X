#import <UIKit/UIKit.h>
#import <FelloPush/IKonectNotificationsCallback.h>

#if UNITY_VERSION >= 430
#import "AppDelegateListener.h"
#endif

extern UIViewController* UnityGetGLViewController();
extern "C" {
    void _FelloPushBridgePlugin_SetDeviceId(const char* deviceId);
    void _FelloPushBridgePlugin_OnInterstitial(const char* tag);
    void _FelloPushBridgePlugin_CancelInterstitial();
    void _FelloPushBridgePlugin_ScheduleLocalNotification(const char* text, int64_t date, const char* label);
    void _FelloPushBridgePlugin_CancelLocalNotification(const char* label);
    void _FelloPushBridgePlugin_SetAdEnabled(BOOL enabled);
    void _FelloPushBridgePlugin_SetTagString(const char* name, const char* value);
    void _FelloPushBridgePlugin_SetTagInt(const char* name, int value);
}

#if UNITY_VERSION >= 430
@interface FelloPushBridgeCallback : NSObject<IKonectNotificationsCallback, AppDelegateListener>
#else
@interface FelloPushBridgeCallback : NSObject<IKonectNotificationsCallback>
#endif

@end