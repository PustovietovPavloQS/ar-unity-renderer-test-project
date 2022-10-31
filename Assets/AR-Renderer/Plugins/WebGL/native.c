// #define EMSCRIPTEN_KEEPALIVE

#include <stdint.h>
#include "emscripten.h"
#include "native.h"

callback_vtt cb_vtt;
callback_vft cb_vft;


void set_callbacks(
	callback_vtt p_tt,
	callback_vft p_ft
)
{
	cb_vtt = p_tt;
	cb_vft = p_ft;
}



void EMSCRIPTEN_KEEPALIVE call_cb_vtt(struct Vector3 pos,struct Quaternion rot, struct Vector3 sc, float cFov) {
	cb_vtt(pos, rot, sc, cFov);			//Target transform
}

void EMSCRIPTEN_KEEPALIVE call_cb_vft(struct Vector3 pos,struct Quaternion rot, struct Vector3 sc, float cFov, const char *fm) {
	cb_vft(pos, rot, sc, cFov, fm);		//Face transform
}