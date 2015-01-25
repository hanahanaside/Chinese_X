#include "LobiInterface.h"
#include "CCDirector.h"

#include "LobiRec/LobiAndroidRec.h"
#include "LobiCore/LobiAndroidCore.h"

bool LobiInterface::isSignedIn()
{
    return LobiAndroidCore::isSignedIn();
}

void LobiInterface::presentProfile()
{
    // unsupported
}

void LobiInterface::presentChat()
{
    // unsupported
}

void LobiInterface::presentLobiPlay()
{
    LobiAndroidRec::presentLobiPlay();
}

void LobiInterface::presentLobiPlay(
    const char* userExid,
    const char* category,
    const bool letsplay,
    const char* metaJson
)
{
    LobiAndroidRec::presentLobiPlay(userExid, category, letsplay, metaJson);
}

void LobiInterface::prepareExternalId(
        char* encryptedExternalId,
        char* iv,
        char* accountBaseName)
{
    LobiAndroidCore::prepareExternalId(encryptedExternalId, iv, accountBaseName);
}

void LobiInterface::enableStrictExid()
{
    LobiAndroidCore::enableStrictExid();
}

void LobiInterface::presentRanking()
{
    // unsupported
}

void LobiInterface::sendRanking()
{
    // unsupported
}

void LobiInterface::recStart()
{
    if (!LobiAndroidRec::isSupported()) {
        CCLOG("LobiRec: not supported");
        return;
    }

    LobiAndroidRec::setMicEnable(true);
    LobiAndroidRec::setCapturePerFrame(3);
    LobiAndroidRec::setMicVolume(1.4f);
    LobiAndroidRec::setGameSoundVolume(0.5f);
    LobiAndroidRec::startCapturing();
}

void LobiInterface::wipePostion(float x, float y)
{
    // unsupported
}

void LobiInterface::recStop()
{
    LobiAndroidRec::stopCapturing();
}

void LobiInterface::recPause()
{
    LobiAndroidRec::pauseCapturing();
}

void LobiInterface::recResume()
{
    LobiAndroidRec::resumeCapturing();
}

void LobiInterface::presentShare()
{
    LobiAndroidRec::presentLobiPostWithTitle(
        "プレイ動画をシェアします！",
        "神懸ったこの華麗なプレイ。やばい。",
        100,  // score
        "",   // category
        ""    // metaData
        );
}


bool LobiInterface::isRecording()
{
    return LobiAndroidRec::isCapturing();
}

bool LobiInterface::isPause()
{
    // unsupported
}

void LobiInterface::initOpenSLAudio()
{
    LobiAndroidRec::initOpenSLAudio(22050);
}

bool LobiInterface::removeUnretainedVideo()
{
    return LobiAndroidRec::removeUnretainedVideo();
}

int LobiInterface::uploadQueueCount()
{
    return LobiAndroidRec::uploadQueueCount();
}
