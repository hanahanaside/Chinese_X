#import <UIKit/UIKit.h>

#ifdef __cplusplus
extern "C" {
#endif
    void LobiRec_set_unity_pause_func(void(*unityPause)(bool));
    void LobiRec_set_unity_gl_view(UIView*(*unityGetGLView)(void));
    UIView* LobiRec_get_unity_gl_view();    
#ifdef __cplusplus
}
#endif
