#ifndef __LOBI_ANDROID_CORE__
#define __LOBI_ANDROID_CORE__

#include <jni.h>

class LobiAndroidCore {
public:
    static bool isSignedIn();
    static void prepareExternalId(
        const char* encryptedExternalId,
        const char* iv,
        const char* accountBaseName);
    static void enableStrictExid();
    static bool hasExidUser();
    static void updateExid(
        const char* encryptedExternalId,
        const char* iv, 
        const void (*callback)(JNIEnv* env, jobject obj, jint code));
    static void bindToLobiAccount();
};


#endif // __LOBI_ANDROID_CORE__
