#include "LobiAndroidCore.h"
#include <cocos2d.h>
#include <platform/android/jni/JniHelper.h>

using namespace cocos2d;

bool LobiAndroidCore::isSignedIn()
{
    bool ret = false;
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(t, "com/kayac/lobi/sdk/LobiCore", "isSignedIn", "()Z")) {
        ret = t.env->CallStaticBooleanMethod(t.classID, t.methodID, NULL);
        t.env->DeleteLocalRef(t.classID);
    }
    return ret;
}

void LobiAndroidCore::prepareExternalId(
        const char* encryptedExternalId,
        const char* iv,
        const char* accountBaseName
) {
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(
            t, "com/kayac/lobi/sdk/LobiCore", "prepareExternalId",
            "("
            "Ljava/lang/String;"
            "Ljava/lang/String;"
            "Ljava/lang/String;"
            ")V"
            )) {

        jstring jexid = t.env->NewStringUTF(encryptedExternalId);
        jstring jiv = t.env->NewStringUTF(iv);
        jstring jaccountBaseName = t.env->NewStringUTF(accountBaseName);

        t.env->CallStaticVoidMethod(
            t.classID, t.methodID, 
            jexid,
            jiv,
            jaccountBaseName,
            0,
            0);
        
        t.env->DeleteLocalRef(jexid);
        t.env->DeleteLocalRef(jiv);
        t.env->DeleteLocalRef(jaccountBaseName);
        
        t.env->DeleteLocalRef(t.classID);
    } else {
        CCLOG("failed to find method named prepareExternalId");
    }
}

void LobiAndroidCore::enableStrictExid()
{
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(t, "com/kayac/lobi/sdk/LobiCore", "enableStrictExid", "()V")) {
        t.env->CallStaticBooleanMethod(t.classID, t.methodID, 0, 0);
        t.env->DeleteLocalRef(t.classID);
    } else {
        CCLOG("failed to find method named enableStrictExid");
    }
}

bool LobiAndroidCore::hasExidUser()
{
    bool ret = false;
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(
            t, "com/kayac/lobi/sdk/LobiCore", "hasExidUser",
            "()Z"
            )) {

        ret = t.env->CallStaticBooleanMethod(t.classID, t.methodID, NULL);
        
        t.env->DeleteLocalRef(t.classID);
    } else {
        CCLOG("failed to find method named hasExidUser");
    }
    return ret;
}

void LobiAndroidCore::updateExid(
        const char* encryptedExternalId,
        const char* iv,
        const void (*callback)(JNIEnv* env, jobject obj, jint code)
) {
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(
            t, "com/kayac/lobi/sdk/LobiCoreAPI", "updateEncryptedExternalId",
            "("
            "Ljava/lang/String;"
            "Ljava/lang/String;"
            ")V"
            )) {
        // register callback
        jclass jni_test_class = t.env->FindClass("com/kayac/lobi/sdk/LobiCoreAPI");
        JNINativeMethod native_method = {
            "updateEncryptedExternalIdNativeCallback",
            "("
            "I"
            "Ljava/lang/String;"
            ")V",
            (void *) callback
        };
        t.env->RegisterNatives(jni_test_class, &native_method, 1);

        // call java method
        jstring jexid = t.env->NewStringUTF(encryptedExternalId);
        jstring jiv = t.env->NewStringUTF(iv);

        t.env->CallStaticVoidMethod(
            t.classID, t.methodID, 
            jexid,
            jiv,
            0,
            0);
        
        t.env->DeleteLocalRef(jexid);
        t.env->DeleteLocalRef(jiv);
        
        t.env->DeleteLocalRef(t.classID);
    } else {
        CCLOG("failed to find method named updateExid");
    }
}

void LobiAndroidCore::bindToLobiAccount()
{
    JniMethodInfo t;
    if (JniHelper::getStaticMethodInfo(t, "com/kayac/lobi/sdk/LobiCore", "bindToLobiAccount", "()V")) {
        t.env->CallStaticVoidMethod(t.classID, t.methodID, 0, 0);
        t.env->DeleteLocalRef(t.classID);
    } else {
        CCLOG("failed to find method named bindToLobiAccount");
    }
}
