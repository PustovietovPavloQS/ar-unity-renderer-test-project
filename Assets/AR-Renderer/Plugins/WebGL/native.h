struct Vector3 {
	float x,y,z;
};

struct Quaternion {
	float x,y,z,w;
};

typedef void (*callback_vtt)(struct Vector3, struct Quaternion, struct Vector3, float);
typedef void (*callback_vft)(struct Vector3, struct Quaternion, struct Vector3, float, const char *a);