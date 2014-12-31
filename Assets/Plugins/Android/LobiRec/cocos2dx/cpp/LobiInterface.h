//
//  LobiInterface.h
//  RecTest
//
//  Created by takahashi-kohei on 2014/01/28.
//
//
 
#ifndef __cocos2dx__KLMCaptureInterface__
#define __cocos2dx__KLMCaptureInterface__
 
namespace LobiInterface
{
    bool isSignedIn();
    void presentProfile();
    void presentChat();
    void presentLobiPlay();
    void presentLobiPlay(
        const char* userExid,
        const char* category,
        const bool letsplay,
        const char* metaJson
    );
    
    void prepareExternalId(
        char* encryptedExternalId,
        char* iv,
        char* accountBaseName);
    void enableStrictExid();
     
    void presentRanking();
    void sendRanking();
     
    void recStart();
    void recStop();
    bool isRecording();
    void recPause();
    void recResume();
    bool isPause();
 
    void presentShare();
    void wipePostion(float x, float y);
    
    void initOpenSLAudio();

    bool removeUnretainedVideo();
    int  uploadQueueCount();
}
 
#endif
