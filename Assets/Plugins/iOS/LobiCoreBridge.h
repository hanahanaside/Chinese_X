#import <UIKit/UIKit.h>

#ifdef __cplusplus
extern "C" {
#endif
    void LobiCore_set_root_view_controller_func(UIViewController*(*getViewController)());
    UIViewController* LobiCore_get_root_view_controller();
#ifdef __cplusplus
}
#endif
