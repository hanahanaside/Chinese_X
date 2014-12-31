#ifndef __LOBI_ANDROID_REC__
#define __LOBI_ANDROID_REC__

#include <jni.h>

class LobiAndroidRec {
public:
    static int NO_ERROR;
    static int ERROR_BAD_ENCODER_CONNECTION;

    static void setRecorderSwitch(bool turnedOn);
    static bool getRecorderSwitch();
    static void startCapturing();
    static void stopCapturing();
    static void resumeCapturing();
    static void pauseCapturing();
    static void setLiveWipeStatus(int status);
    static void setWipeSquareSize(int wipeSize);
    static void setWipePosition(int x, int y);
    static void setMicEnable(bool enabled);
    static void setMicVolume(double volume);
    static void setGameSoundVolume(double volume);
    static void setHideFaceOnPreview(bool hidden);
    static void setPreventSpoiler(bool enabled);
    static void setCapturePerFrame(int frames);
    static void setStickyRecording(bool enabled);
    static bool hasMovie();
    static bool isSupported();
    static int checkError();
    static bool isCapturing();
    static void presentLobiPostWithTitle(
        const char* title,
        const char* postDescription,
        const jlong postScore,
        const char* postCategory);
    static void presentLobiPostWithTitle(
        const char* title,
        const char* postDescription,
        const jlong postScore,
        const char* postCategory,
        const char* postMetadata);
    static void presentLobiPlay();
    static void presentLobiPlay(
        const char* userExid,
        const char* category,
        const bool letsplay,
        const char* metaJson
    );
    static void initOpenSLAudio(int sampleRate);
    static void setSecretMode(bool secretMode);
    static bool removeUnretainedVideo();
    static int  uploadQueueCount();
};


#endif // __LOBI_ANDROID_REC__
